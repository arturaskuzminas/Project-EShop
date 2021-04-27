using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
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
        public async Task<IActionResult> Products()
        {
            return View(await _context.Products.ToListAsync());
        }
    }
}
