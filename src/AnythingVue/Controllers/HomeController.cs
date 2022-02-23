using Microsoft.AspNetCore.Mvc;

namespace Anything.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}