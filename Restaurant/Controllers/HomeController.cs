using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using System.Diagnostics;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  ModelContext context;
        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var categories = context.Categories.ToList();


            return View(categories);
        }


        public IActionResult GetProductByCategory(int id)
        {
            var product = context.Products.Where(x => x.Categoryid == id).ToList();

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "home");
        }
    }
}