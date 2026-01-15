using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia1.DAL;
using Pronia1.Models;
using Pronia1.Utilities.Extensions;
using Pronia1.Utilities.Enums;
using Pronia1.Areas.Admin.ViewModels;

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
        public async Task<IActionResult> Create(CreateSliderVM createSliderVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(!createSliderVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }

            if (createSliderVM.Photo.ValidateSize(FileSize.KB, 20))
            {
                ModelState.AddModelError("Photo", "File size is incorrect");
                return View();
            }

            Slider slider = new Slider()
            {
                Title = createSliderVM.Title,
                Description = createSliderVM.Description,
                Discount = createSliderVM.Discount,
                Order = createSliderVM.Order,
                Image = await createSliderVM.Photo.CreateFile(_env.WebRootPath, "assets", "images", "website-images")
            };

            //createSliderVM.Image = await createSliderVM.Photo.CreateFile(_env.WebRootPath,"assets","images","website-images");
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            //System.IO.File.Delete(Path.Combine(_env.WebRootPath,"assets","images","website-images",slider.Image));
            slider.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Sliders.Remove(slider);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            UpdateSliderVM updateSliderVM = new UpdateSliderVM()
            {
                Title = slider.Title,
                Description = slider.Description,
                Discount = slider.Discount,
                Order = slider.Order,
                Image = slider.Image
            };
            return View(updateSliderVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, UpdateSliderVM updateSliderVM)
        {
            if(!ModelState.IsValid)
            {
                return View(updateSliderVM);
            }
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);

            if(updateSliderVM.Photo != null)
            {
                if(!updateSliderVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(updateSliderVM.Photo), "File type must be image");
                    return View();
                }
                if (updateSliderVM.Photo.ValidateSize(FileSize.KB, 20))
                {
                    ModelState.AddModelError(nameof(updateSliderVM.Photo), "File size is incorrect");
                    return View();
                }
                string fileName=await updateSliderVM.Photo.CreateFile(_env.WebRootPath, "assets", "images", "website-images");
                slider.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                slider.Image = fileName;
            }
            slider.Title = updateSliderVM.Title;
            slider.Description = updateSliderVM.Description;
            slider.Discount = updateSliderVM.Discount;
            slider.Order = updateSliderVM.Order;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
