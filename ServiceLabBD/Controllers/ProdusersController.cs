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
    public class ProdusersController : Controller
    {
        private readonly ServiceContext _context;

        public ProdusersController(ServiceContext context)
        {
            _context = context;
        }

        // GET: Produsers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Produsers.ToListAsync());
        }

        // GET: Produsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produsers == null)
            {
                return NotFound();
            }

            var produser = await _context.Produsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produser == null)
            {
                return NotFound();
            }

            return View(produser);
        }

        // GET: Produsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Adress")] Produser produser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produser);
        }

        // GET: Produsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produsers == null)
            {
                return NotFound();
            }

            var produser = await _context.Produsers.FindAsync(id);
            if (produser == null)
            {
                return NotFound();
            }
            return View(produser);
        }

        // POST: Produsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Adress")] Produser produser)
        {
            if (id != produser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduserExists(produser.Id))
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
            return View(produser);
        }

        // GET: Produsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produsers == null)
            {
                return NotFound();
            }

            var produser = await _context.Produsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produser == null)
            {
                return NotFound();
            }

            return View(produser);
        }

        // POST: Produsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produsers == null)
            {
                return Problem("Entity set 'ServiceContext.Produsers'  is null.");
            }
            var produser = await _context.Produsers.FindAsync(id);
            if (produser != null)
            {
                _context.Produsers.Remove(produser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduserExists(int id)
        {
          return _context.Produsers.Any(e => e.Id == id);
        }
    }
}
