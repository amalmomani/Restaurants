using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext _context)
        {
            this._context = _context;
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = 20;
            // var Fname= "Amal";
            ViewBag.Fname =  HttpContext.Session.GetString("Fname");

            ViewBag.CustomerCount = _context.Customers.Count();

            ViewData["Customers"] = _context.Customers.Count();

            ViewBag.ProductsCount= _context.Products.Count();

            ViewBag.PC = _context.Productcustomers.Sum(x=>x.Quantity);

            ViewBag.TotalSales = _context.Productcustomers.Sum(x=>x.Quantity * (x.Product.Price) );



            

            return View();
        }
    }
}
