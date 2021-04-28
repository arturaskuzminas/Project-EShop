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
                results = results.Where(s => s.Title.StartsWith(id));
                return View(results);
            }
        }
    }
}
