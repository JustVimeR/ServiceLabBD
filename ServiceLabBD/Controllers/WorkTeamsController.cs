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
    public class WorkTeamsController : Controller
    {
        private readonly ServiceContext _context;

        public WorkTeamsController(ServiceContext context)
        {
            _context = context;
        }

        // GET: WorkTeams
        public async Task<IActionResult> Index()
        {
            var serviceContext = _context.WorkTeams.Include(w => w.Master).Include(w => w.Mechanic);
            return View(await serviceContext.ToListAsync());
        }

        // GET: WorkTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkTeams == null)
            {
                return NotFound();
            }

            var workTeam = await _context.WorkTeams
                .Include(w => w.Master)
                .Include(w => w.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTeam == null)
            {
                return NotFound();
            }

            return View(workTeam);
        }

        // GET: WorkTeams/Create
        public IActionResult Create()
        {
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "FullName");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            return View();
        }

        // POST: WorkTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MasterId,MechanicId")] WorkTeam workTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "FullName", workTeam.MasterId);
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName", workTeam.MechanicId);
            return View(workTeam);
        }

        // GET: WorkTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkTeams == null)
            {
                return NotFound();
            }

            var workTeam = await _context.WorkTeams.FindAsync(id);
            if (workTeam == null)
            {
                return NotFound();
            }
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "FullName", workTeam.MasterId);
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName", workTeam.MechanicId);
            return View(workTeam);
        }

        // POST: WorkTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MasterId,MechanicId")] WorkTeam workTeam)
        {
            if (id != workTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTeamExists(workTeam.Id))
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
            ViewData["MasterId"] = new SelectList(_context.Masters, "Id", "FullName", workTeam.MasterId);
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName", workTeam.MechanicId);
            return View(workTeam);
        }

        // GET: WorkTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkTeams == null)
            {
                return NotFound();
            }

            var workTeam = await _context.WorkTeams
                .Include(w => w.Master)
                .Include(w => w.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTeam == null)
            {
                return NotFound();
            }

            return View(workTeam);
        }

        // POST: WorkTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkTeams == null)
            {
                return Problem("Entity set 'ServiceContext.WorkTeams'  is null.");
            }
            var workTeam = await _context.WorkTeams.FindAsync(id);
            if (workTeam != null)
            {
                _context.WorkTeams.Remove(workTeam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTeamExists(int id)
        {
          return _context.WorkTeams.Any(e => e.Id == id);
        }
    }
}
