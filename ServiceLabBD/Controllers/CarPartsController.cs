using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceLabBD;

namespace ServiceLabBD.Controllers
{
    public class CarPartsController : Controller
    {
        private readonly ServiceContext _context;

        public CarPartsController(ServiceContext context)
        {
            _context = context;
        }

        // GET: CarParts
        public async Task<IActionResult> Index()
        {
            var serviceContext = _context.CarParts.Include(c => c.Produser).Include(c => c.Stocks);
            return View(await serviceContext.ToListAsync());
        }

        // GET: CarParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts
                .Include(c => c.Produser)
                .Include(c => c.Stocks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // GET: CarParts/Create
        public IActionResult Create()
        {
            ViewData["ProduserId"] = new SelectList(_context.Produsers, "Id", "Adress");
            ViewData["StocksId"] = new SelectList(_context.Stocks, "Id", "Id");
            return View();
        }

        // POST: CarParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProduserId,Description,Price,Quantity,StocksId")] CarPart carPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProduserId"] = new SelectList(_context.Produsers, "Id", "Adress", carPart.ProduserId);
            ViewData["StocksId"] = new SelectList(_context.Stocks, "Id", "Id", carPart.StocksId);
            return View(carPart);
        }

        // GET: CarParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts.FindAsync(id);
            if (carPart == null)
            {
                return NotFound();
            }
            ViewData["ProduserId"] = new SelectList(_context.Produsers, "Id", "Adress", carPart.ProduserId);
            ViewData["StocksId"] = new SelectList(_context.Stocks, "Id", "Id", carPart.StocksId);
            return View(carPart);
        }

        // POST: CarParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProduserId,Description,Price,Quantity,StocksId")] CarPart carPart)
        {
            if (id != carPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPartExists(carPart.Id))
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
            ViewData["ProduserId"] = new SelectList(_context.Produsers, "Id", "Adress", carPart.ProduserId);
            ViewData["StocksId"] = new SelectList(_context.Stocks, "Id", "Id", carPart.StocksId);
            return View(carPart);
        }

        // GET: CarParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarParts == null)
            {
                return NotFound();
            }

            var carPart = await _context.CarParts
                .Include(c => c.Produser)
                .Include(c => c.Stocks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPart == null)
            {
                return NotFound();
            }

            return View(carPart);
        }

        // POST: CarParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarParts == null)
            {
                return Problem("Entity set 'ServiceContext.CarParts'  is null.");
            }
            var carPart = await _context.CarParts.FindAsync(id);
            if (carPart != null)
            {
                _context.CarParts.Remove(carPart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPartExists(int id)
        {
          return _context.CarParts.Any(e => e.Id == id);
        }
    }
}
