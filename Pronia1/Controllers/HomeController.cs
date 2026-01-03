using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.ViewModels;

namespace Pronia1.Controllers
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
            

            //_context.Sliders.AddRange(slides);
            //_context.SaveChanges();

            

            //_context.Blogs.AddRange(blogs);
            //_context.SaveChanges();

           /* List<Product> products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Red Rose",
                    Price = 19.99m,
                },
                new Product
                {
                    Id = 2,
                    Name = "White Tulip",
                    Price = 19.99m,
                },
                new Product
                {
                    Id = 3,
                    Name = "Yellow Sunflower",
                    Price = 19.99m,
                }
            };*/

            HomeVM homeVM = new HomeVM
            {
                Slides = _context.Sliders.OrderBy(s=>s.Order).ToList(),
                Products= _context.Products.Include(p=>p.ProductImages).ToList(),
                Blogs = _context.Blogs.ToList()
            };
            return View(homeVM);
        }
    }
}
