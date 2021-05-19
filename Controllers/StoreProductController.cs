using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class StoreProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: StoreProduct
        public async Task<IActionResult> Product(int? id)
        {
            ViewBag.id = id;
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

        [AllowAnonymous]
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
            else
            {
                results = results.Where(s => s.Title.ToLowerInvariant().StartsWith(id.ToLowerInvariant()));
                return View(results);
            }
        }

        [AllowAnonymous]
        // GET: CategoryProducts
        [HttpGet]
        public async Task<IActionResult> CategoryProducts(string id)
        {
            IEnumerable<ProductModel> products = await _context.Products.ToListAsync();
            int categoryID = _context.Categories.Where(n => n.ID == int.Parse(id)).First().ID;

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.searchStr = "incorrect";
                return View();
            }
            else
            {
                products = products.Where(pr => pr.CategoryID == categoryID);
                ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                return View(products);
            }
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult AddToCart(int id)
        {
            HttpContext.Session.SetInt32("inc", (int)HttpContext.Session.GetInt32("inc") + 1);
            return RedirectToAction("Product", "StoreProduct", new { id });
        }
    }
}
