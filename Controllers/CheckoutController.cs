using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using MyShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CartHelper helper;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
            helper = new CartHelper();
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            List<ProductModel> collection = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            ViewBag.Total = helper.GetProductsTotalPrice(collection);
            List<CartViewModel> products = helper.GroupedProducts(collection);

            return View(products);
        }
    }
}
