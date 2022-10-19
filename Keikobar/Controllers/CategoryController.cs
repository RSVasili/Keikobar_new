using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keikobar.Data;
using Keikobar.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keikobar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext context)
        {
            _dbContext = context;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _dbContext.Categories;
            return View(objList);
        }
        
        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
        
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken] // Атрибут защиты  в который добавляется токен от взлома
        public IActionResult Create(Category obj)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}