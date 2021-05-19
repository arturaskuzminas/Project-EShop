using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.ViewComponents
{
    public class CartNav : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartNav(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
