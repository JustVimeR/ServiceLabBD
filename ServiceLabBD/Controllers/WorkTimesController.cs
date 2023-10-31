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
    public class WorkTimesController : Controller
    {
        private readonly ServiceContext _context;

        public WorkTimesController(ServiceContext context)
        {
            _context = context;
        }

        // GET: WorkTimes
        public async Task<IActionResult> Index()
        {
              return View(await _context.WorkTimes.ToListAsync());
        }

        // GET: WorkTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkTimes == null)
            {
                return NotFound();
            }

            var workTime = await _context.WorkTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTime == null)
            {
                return NotFound();
            }

            return View(workTime);
        }

        // GET: WorkTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime")] WorkTime workTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workTime);
        }

        // GET: WorkTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkTimes == null)
            {
                return NotFound();
            }

            var workTime = await _context.WorkTimes.FindAsync(id);
            if (workTime == null)
            {
                return NotFound();
            }
            return View(workTime);
        }

        // POST: WorkTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime")] WorkTime workTime)
        {
            if (id != workTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTimeExists(workTime.Id))
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
            return View(workTime);
        }

        // GET: WorkTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkTimes == null)
            {
                return NotFound();
            }

            var workTime = await _context.WorkTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTime == null)
            {
                return NotFound();
            }

            return View(workTime);
        }

        // POST: WorkTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkTimes == null)
            {
                return Problem("Entity set 'ServiceContext.WorkTimes'  is null.");
            }
            var workTime = await _context.WorkTimes.FindAsync(id);
            if (workTime != null)
            {
                _context.WorkTimes.Remove(workTime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTimeExists(int id)
        {
          return _context.WorkTimes.Any(e => e.Id == id);
        }
    }
}
