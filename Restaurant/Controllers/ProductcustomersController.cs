using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ProductcustomersController : Controller
    {
        private readonly ModelContext _context;

        public ProductcustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Productcustomers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Productcustomers.Include(p => p.Customer).Include(p => p.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Productcustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Productcustomers == null)
            {
                return NotFound();
            }

            var productcustomer = await _context.Productcustomers
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productcustomer == null)
            {
                return NotFound();
            }

            return View(productcustomer);
        }

        // GET: Productcustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Productcustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,Quantity,DateFrom")] Productcustomer productcustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productcustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productcustomer.ProductId);
            return View(productcustomer);
        }

        // GET: Productcustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Productcustomers == null)
            {
                return NotFound();
            }

            var productcustomer = await _context.Productcustomers.FindAsync(id);
            if (productcustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productcustomer.ProductId);
            return View(productcustomer);
        }

        // POST: Productcustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ProductId,CustomerId,Quantity,DateFrom")] Productcustomer productcustomer)
        {
            if (id != productcustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productcustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductcustomerExists(productcustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productcustomer.ProductId);
            return View(productcustomer);
        }

        // GET: Productcustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Productcustomers == null)
            {
                return NotFound();
            }

            var productcustomer = await _context.Productcustomers
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productcustomer == null)
            {
                return NotFound();
            }

            return View(productcustomer);
        }

        // POST: Productcustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Productcustomers == null)
            {
                return Problem("Entity set 'ModelContext.Productcustomers'  is null.");
            }
            var productcustomer = await _context.Productcustomers.FindAsync(id);
            if (productcustomer != null)
            {
                _context.Productcustomers.Remove(productcustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductcustomerExists(decimal id)
        {
          return (_context.Productcustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
