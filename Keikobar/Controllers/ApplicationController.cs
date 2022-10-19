using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keikobar.Data;
using Keikobar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}