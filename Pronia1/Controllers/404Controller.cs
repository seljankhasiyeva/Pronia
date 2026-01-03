using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class _404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
