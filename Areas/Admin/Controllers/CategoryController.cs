using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Models;
using Pronia.DAL;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]

   
    public class CategoryController : Controller
    {
        public readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //List<Category> categories=await _context.Categories.Include(c=>c.Products).ToListAsync();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            bool result=await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if(result)
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                return View();
            }
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            return Json(category);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id==null || id<1)
            {
                return BadRequest();
            }
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id==id);
            if(category is null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Category exists = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (exists is null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "The category with this name already exists");
                return View();
            }
            exists.Name=category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete()
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            _context.Categories.Remove();
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
