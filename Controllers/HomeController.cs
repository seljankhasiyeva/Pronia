using Microsoft.AspNetCore.Mvc;
using Pronia.DAL;
using Pronia.ViewModels;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            
        {
            HomeVM vm = new HomeVM
            {
                Sliders = _context.Sliders.Where(s => s.IsDeleted).ToList(),
                Products = _context.Products.Take(8).ToList(),
                Blogs = _context.Blogs.OrderByDescending(b => b.CreatedDate).Take(3).ToList()
            };

            return View(vm);
        }
    }
}
