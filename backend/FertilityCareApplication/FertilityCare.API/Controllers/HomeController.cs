using Microsoft.AspNetCore.Mvc;

namespace FertilityCare.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
