using Microsoft.AspNetCore.Mvc;

namespace Petrobowl.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
