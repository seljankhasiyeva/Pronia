using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.Utilities.Extensions;
using Pronia1.Utilities.Enums;

namespace Pronia1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;


        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult Test()
        {
            string result=Guid.NewGuid().ToString();    
            return Content(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slide)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(!slide.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }

            if (slide.Photo.ValidateSize(FileSize.KB, 20))
            {
                ModelState.AddModelError("Photo", "File size is incorrect");
                return View();
            }

            slide.Image = await slide.Photo.CreateFile(_env.WebRootPath,"assets","images","website-images");
            await _context.Sliders.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
