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
    public class MastersController : Controller
    {
        private readonly ServiceContext _context;

        public MastersController(ServiceContext context)
        {
            _context = context;
        }

        // GET: Masters
        public async Task<IActionResult> Index()
        {
              return View(await _context.Masters.ToListAsync());
        }

        // GET: Masters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Masters == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // GET: Masters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Masters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Experience,QualificationLevel")] Master master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(master);
        }

        // GET: Masters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Masters == null)
            {
                return NotFound();
            }

            var master = await _context.Masters.FindAsync(id);
            if (master == null)
            {
                return NotFound();
            }
            return View(master);
        }

        // POST: Masters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Experience,QualificationLevel")] Master master)
        {
            if (id != master.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterExists(master.Id))
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
            return View(master);
        }

        // GET: Masters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Masters == null)
            {
                return NotFound();
            }

            var master = await _context.Masters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (master == null)
            {
                return NotFound();
            }

            return View(master);
        }

        // POST: Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Masters == null)
            {
                return Problem("Entity set 'ServiceContext.Masters'  is null.");
            }
            var master = await _context.Masters.FindAsync(id);
            if (master != null)
            {
                _context.Masters.Remove(master);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterExists(int id)
        {
          return _context.Masters.Any(e => e.Id == id);
        }
    }
}
