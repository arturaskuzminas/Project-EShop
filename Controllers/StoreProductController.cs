using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using MyShop.ViewComponents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class StoreProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartHelper helper;

        public StoreProductController(ApplicationDbContext context)
        {
            _context = context;
            helper = new CartHelper();
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
            List<ProductModel> list = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            list.Add(_context.Products.Where(p => p.ID.Equals(id)).First());
            list.Sort(helper.GetComparerByTitle());
            Extensions.SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", list);

            HttpContext.Session.SetInt32("inc", list.Count());
            return RedirectToAction("Product", "StoreProduct", new { id });
        }
    }
}