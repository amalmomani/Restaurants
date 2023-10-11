using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var products = _context.Products.Include(p=>p.Category).ToList();

            var productCustomer = _context.Productcustomers.ToList();

            var customers= _context.Customers.ToList();

            var category = _context.Categories.ToList();
            var model = Tuple.Create<IEnumerable<Product>,
                IEnumerable<Productcustomer>, IEnumerable<Customer>>
                (products, productCustomer, customers);

            return View(model);
        }


        public IActionResult Join()
        {
            var products = _context.Products.ToList();
            var category= _context.Categories.ToList();
            var customers = _context.Customers.ToList();
            var productCustomers= _context.Productcustomers.ToList();


            var model = from c in customers
                        join pc in productCustomers on c.Id equals pc.CustomerId
                        join p in products on pc.ProductId equals p.Id
                        join cat in category on p.Categoryid equals cat.Id
                        select new JoinTable { category = cat, customer = c, product = p, productcustomer = pc };



            return View(model);
        }


        //get
        public IActionResult Search()
        {
            var model = _context.Productcustomers.Include(p => p.Product).Include(c => c.Customer).ToList();

            return View(model);
        }

        //post

        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var model = _context.Productcustomers.Include(p => p.Product).Include(c => c.Customer).ToList();

            if(startDate == null && endDate == null)
            {
                return View(model);
            }
            else if(startDate !=null && endDate == null)
            {
                var result = model.Where(x => x.DateFrom.Value.Date >= startDate);
                return View(result);
            }
            else if(startDate ==null && endDate != null)
            {
                var result = model.Where(x => x.DateFrom.Value.Date <= endDate);
                return View(result);
            }
            else
            {
                var result = model.Where(x => x.DateFrom.Value.Date >= startDate && x.DateFrom.Value.Date <= endDate);
                return View(result);

            }
        }

    }
}
