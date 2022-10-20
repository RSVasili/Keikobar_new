using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Keikobar.Data;
using Keikobar.Models;
using Keikobar.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Keikobar.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _dbContext.Products.Include(u =>u.Category).Include(u => u.ApplicationType);

            // foreach (var obj in objList)
            // {
            //     obj.Category = _dbContext.Categories.FirstOrDefault(u => u.Id == obj.CategoryId);
            //     obj.ApplicationType = _dbContext.ApplicationTypes.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            // }
            
            return View(objList);
        }
        
        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            // IEnumerable<SelectListItem> CategoryDropDown = _dbContext.Categories.Select(i => new SelectListItem
            // {
            //     Text = i.Name,
            //     Value = i.Id.ToString()
            // });
            //
            // ViewBag.CategoryDropDown = CategoryDropDown; 
            // ViewData["CategoryDropDown"] = CategoryDropDown; 

            // Product product = new Product();

            ProductVM productVm = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _dbContext.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ApplicationTypeSelectList  = _dbContext.ApplicationTypes.Select(i => new SelectListItem
                {
                Text = i.Name,
                Value = i.Id.ToString()
            })
            };

            if (id == null)
            {
                // this is for create
                return View(productVm);
            }
            else
            {
                // this is for update
                productVm.Product = _dbContext.Products.Find(id);

                if (productVm.Product == null)
                {
                    return NotFound();
                }

                return View(productVm);
            }
        }
        
        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVm)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVm.Product.Id == Guid.Empty)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVm.Product.Image = fileName + extension;

                    _dbContext.Products.Add(productVm.Product);
                }
                else
                {
                    //Updating
                    var objFromDb = _dbContext.Products.AsNoTracking().FirstOrDefault(u => u.Id == productVm.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVm.Product.Image = fileName + extension;

                    }
                    else
                    {
                        productVm.Product.Image = objFromDb.Image;
                    }

                    _dbContext.Products.Update(productVm.Product);
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            productVm.CategorySelectList = _dbContext.Categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            productVm.ApplicationTypeSelectList = _dbContext.ApplicationTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(productVm);
        }
        
        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            Product product = _dbContext.Products.Include(u => u.Category).Include(u => u.ApplicationType).FirstOrDefault(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        
        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _dbContext.Products.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _dbContext.Products.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}