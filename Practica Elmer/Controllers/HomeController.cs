using Microsoft.AspNetCore.Mvc;
using Practica_Elmer.Models;
using System.Diagnostics;

namespace Practica_Elmer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult Historico()
        {
            return View();
        }
        public IActionResult EntradasProductos()
        {
            return View();
        }

        public IActionResult Venta()
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