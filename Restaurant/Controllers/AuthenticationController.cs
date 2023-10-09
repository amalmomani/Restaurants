using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AuthenticationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register2()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,Imagepath,ImageFile")] Customer customer, string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (customer.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + customer.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await customer.ImageFile.CopyToAsync(fileStream);
                    }

                    customer.Imagepath = fileName;
                }

                var user = _context.Userlogins.Where(x => x.Username == email).FirstOrDefault();
                if (user == null)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    Userlogin userlogin = new Userlogin();
                    userlogin.Username = email;
                    userlogin.Password = password;
                    userlogin.RoleId = 2;
                    userlogin.CustomerId = customer.Id;

                    _context.Add(userlogin);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Email is already used, please try another one."; 
                }
            }
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2([Bind("Id,Fname,Lname,Imagepath,ImageFile")] Customer customer, string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (customer.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + customer.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await customer.ImageFile.CopyToAsync(fileStream);
                    }

                    customer.Imagepath = fileName;
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();

                Userlogin userlogin = new Userlogin();
                userlogin.Username = email;
                userlogin.Password = password;
                userlogin.RoleId = 2;
                userlogin.CustomerId = customer.Id;

                _context.Add(userlogin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }



        [HttpPost]

        public async Task<IActionResult> Login([Bind("Id,Username,Password")] Userlogin userlogin)
        {
            var auth = _context.Userlogins.Where(x => x.Username == userlogin.Username && x.Password == userlogin.Password).FirstOrDefault();

            if(auth != null)
            {
                var user = _context.Customers.Where(x=>x.Id== auth.CustomerId).FirstOrDefault();

                switch (auth.RoleId)
                {
                    case 1:                        
                        HttpContext.Session.SetString("Fname", user.Fname );
                        HttpContext.Session.SetString("Lname", user.Lname);
                        HttpContext.Session.SetString("Username", auth.Username);
                        HttpContext.Session.SetInt32("CustomerId", (int)auth.CustomerId );
                        return RedirectToAction("Index", "Admin");
                    case 2:
                        // var Fname= "Amal";

                        HttpContext.Session.SetString("Fname", user.Fname);
                        HttpContext.Session.SetString("Lname", user.Lname);
                        HttpContext.Session.SetString("Username", auth.Username);
                        HttpContext.Session.SetString("Image", user.Imagepath);
                        HttpContext.Session.SetInt32("CustomerId", (int)auth.CustomerId);

                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Error = "Wrong credentials";
            }

            return View();
        }


    }
}
