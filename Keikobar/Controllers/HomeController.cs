using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Keikobar.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Keikobar.Models;
using Keikobar.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Keikobar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                Products = _dbContext.Products.Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _dbContext.Categories
            };
            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}