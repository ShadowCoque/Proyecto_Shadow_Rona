using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using petrotest.Models;
using System.Diagnostics;

namespace petrotest.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Administrador");
                }
                else if (User.IsInRole("Juez"))
                {
                    return RedirectToAction("Index", "Juez");
                }
                else if (User.IsInRole("Competidor"))
                {
                    return RedirectToAction("Index", "Competidor");
                }
                else if (User.IsInRole("Veedor"))
                {
                    return RedirectToAction("Index", "Veedor");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public IActionResult Juez()
        {

            return View();
        }

        public IActionResult Competidor()
        {

            return View();
        }
        public IActionResult Veedor()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}