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
                _context.Add(customer);
                await _context.SaveChangesAsync();

                Userlogin userlogin = new Userlogin();
                userlogin.Username = email;
                userlogin.Password = password;
                userlogin.RoleId = 2;
                userlogin.CustomerId = customer.Id;

                _context.Add(userlogin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Home");
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
    }
}
