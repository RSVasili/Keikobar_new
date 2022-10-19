using System;
using System.Collections.Generic;
using Keikobar.Data;
using Keikobar.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keikobar.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ApplicationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _dbContext.ApplicationTypes;
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            _dbContext.ApplicationTypes.Add(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _dbContext.ApplicationTypes.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ApplicationTypes.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _dbContext.ApplicationTypes.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        
        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _dbContext.ApplicationTypes.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.ApplicationTypes.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}