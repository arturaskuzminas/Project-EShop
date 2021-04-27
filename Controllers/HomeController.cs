using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Data;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductModel> products = await _context.Products.ToListAsync();
            List<ProductModel> collection = new List<ProductModel>();

            collection.Add(products.Where(p => p.Title.Contains("Night")).First());
            collection.Add(products.Where(p => p.Title.Contains("Ivory")).First());
            collection.Add(products.Where(p => p.Title.Contains("Lewis")).First());

            collection.Add(products.Where(p => p.Title.Contains("Nimbo")).First());
            collection.Add(products.Where(p => p.Title.Contains("Mangana")).First());
            collection.Add(products.Where(p => p.Title.Contains("Pheonix")).First());

            return View(collection);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
