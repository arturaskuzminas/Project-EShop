using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [AllowAnonymous]
    public class StoreProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StoreProduct
        public async Task<IActionResult> Product(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // GET: StoreProducts
        [HttpGet]
        public async Task<IActionResult> ProductsAsync(string id)
        {
            IEnumerable<ProductModel> results = await _context.Products.ToListAsync();

            if (string.IsNullOrEmpty(id) || id.Length < 2)
            {
                ViewBag.searchStr = "incorrect";
                return View();
            }
            else {
                results = results.Where(s => s.Title.ToLowerInvariant().StartsWith(id.ToLowerInvariant()));
                return View(results);
            }
        }

        // GET: CategoryProducts
        [HttpGet]
        public async Task<IActionResult> CategoryProducts(string id)
        {
            IEnumerable<ProductModel> products = await _context.Products.ToListAsync();
            int categoryID = _context.Categories.Where(n => n.Title == id).First().ID;

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.searchStr = "incorrect";
                return View();
            }
            else
            {
                products = products.Where(s => s.CategoryID == categoryID);
                ViewBag.CategoryName = id;
                return View(products);
            }
        }
    }
}
