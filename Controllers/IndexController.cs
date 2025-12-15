using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
       
    }
}
