using Microsoft.AspNetCore.Mvc;

namespace petrotest.Controllers
{
    public class VeedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
