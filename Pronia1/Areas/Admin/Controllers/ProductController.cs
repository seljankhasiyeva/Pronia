using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia1.Areas.Admin.ViewModels;
using Pronia1.DAL;
using Pronia1.Models;

namespace Pronia1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProductVM> productVMs= await _context.Products.Include(p=>p.Category).Include(p=>p.ProductImages.Where(pi=>pi.IsPrimary==true))
                .Select(p=>new GetProductVM  
                {
                    Id=p.Id,
                    Name =p.Name,
                    Price=p.Price,
                    CategoryName=p.Category.Name,
                    Image = p.ProductImages[0].Image
                })
                .ToListAsync();
            return View(productVMs);
        }

        public async Task<IActionResult> Create()
        {
            CreateProductVM createProductVM = new CreateProductVM()
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(createProductVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM createProductVM)
        {
            createProductVM.Categories = await _context.Categories.ToListAsync();
            if(!ModelState.IsValid)
            {
                return View(createProductVM);
            }
            bool result=await _context.Categories.AnyAsync(c=>c.Id==createProductVM.CategoryId);
            if(!result)
            {
                ModelState.AddModelError(nameof(createProductVM.CategoryId),"Category not found");
                return View(createProductVM);
            }
            Product product = new Product()
            {
                Name = createProductVM.Name,
                Price = createProductVM.Price,
                Description = createProductVM.Description,
                CategoryId = createProductVM.CategoryId.Value,
                SKU = createProductVM.SKU
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
