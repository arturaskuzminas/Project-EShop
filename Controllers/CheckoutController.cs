using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using MyShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CartHelper helper;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
            helper = new CartHelper();
        }

        public IActionResult Index()
        {
            List<ProductModel> collection = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            ViewBag.Total = helper.GetProductsTotalPrice(collection);
            List<CartViewModel> products = helper.GroupedProducts(collection);



            return View(products);
        }

        public IActionResult RemoveItem(string id)
        {
            List<ProductModel> collection = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            collection.RemoveAll(p => p.Title.Equals(id));
            Extensions.SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", collection);
            HttpContext.Session.SetInt32("inc", collection.Count);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CheckoutFinal()
        {
            var userId = HttpContext.User.Identity.Name;
            List<ProductModel> list = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            List<CartViewModel> groups = helper.GroupedProducts(list);
            var totalPrice = helper.GetProductsTotalPrice(list);
            Order order = new Order(1, totalPrice, userId);

            _context.Add(order);
            _context.SaveChanges();
            var orderID = _context.Orders.ToList();

            foreach (var item in groups)
            {
                OrderDetail detail = new OrderDetail(item.ItemName, item.ItemCount, 
                                                     item.ItemTotalPrice, item.pictureLink, item.ItemPrice,
                                                     orderID.Last().OrderID);
                _context.OrderDetails.Add(detail);
            }
            _context.SaveChanges();
            ViewBag.Message = "Užsakymas atliktas sėkmingai !";
            return View(_context.Orders);
        }
    }
}
