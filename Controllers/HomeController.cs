using Microsoft.AspNetCore.Mvc;
using Pronia.Models;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Slider> slides = new List<Slider>
            {
                new Slider
                {
                    Id = 1,
                    Title = "Summer Sale",
                    Discount = "50% OFF",
                    Description = "Get ready for summer with our exclusive sale!",
                    Image = "1-1-524x617.png",
                    Order = 2
                },

                new Slider
                {
                    Id = 2,
                    Title = "New Arrivals",
                    Discount = "30% OFF",
                    Description = "Check out the latest additions to our collection.",
                    Image = "1-2-524x617.png",
                    Order = 3
                },

                new Slider {
                    Id = 3,
                    Title = "Limited Time Offer",
                    Discount = "20% OFF",
                    Description = "Hurry up! This offer won't last long.",
                    Image = "1-5-270x300.jpg",
                    Order = 1

                }

            };
            return View();
        }
    }
}
