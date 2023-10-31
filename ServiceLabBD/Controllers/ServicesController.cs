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
    public class ServicesController : Controller
    {
        private readonly ServiceContext _context;

        public ServicesController(ServiceContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var serviceContext = _context.Services.Include(s => s.Auto).Include(s => s.Client).Include(s => s.Equipment).Include(s => s.Manager).Include(s => s.WorkTeam).Include(s => s.WorkTime).Include(s => s.WorkType);
            return View(await serviceContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Auto)
                .Include(s => s.Client)
                .Include(s => s.Equipment)
                .Include(s => s.Manager)
                .Include(s => s.WorkTeam)
                .Include(s => s.WorkTime)
                .Include(s => s.WorkType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Model");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName");
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "Id", "Name");
            ViewData["ManagerId"] = new SelectList(_context.Managers, "Id", "FullName");
            ViewData["WorkTeamId"] = new SelectList(_context.WorkTeams, "Id", "Id");
            ViewData["WorkTimeId"] = new SelectList(_context.WorkTimes, "Id", "Id");
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkTypeId,ClientId,AutoId,ManagerId,WorkTeamId,EquipmentId,WorkTimeId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Model", service.AutoId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "Id", "Name", service.EquipmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "Id", "FullName", service.ManagerId);
            ViewData["WorkTeamId"] = new SelectList(_context.WorkTeams, "Id", "Id", service.WorkTeamId);
            ViewData["WorkTimeId"] = new SelectList(_context.WorkTimes, "Id", "Id", service.WorkTimeId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Name", service.WorkTypeId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Model", service.AutoId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "Id", "Name", service.EquipmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "Id", "FullName", service.ManagerId);
            ViewData["WorkTeamId"] = new SelectList(_context.WorkTeams, "Id", "Id", service.WorkTeamId);
            ViewData["WorkTimeId"] = new SelectList(_context.WorkTimes, "Id", "Id", service.WorkTimeId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Name", service.WorkTypeId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkTypeId,ClientId,AutoId,ManagerId,WorkTeamId,EquipmentId,WorkTimeId")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["AutoId"] = new SelectList(_context.Autos, "Id", "Model", service.AutoId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "FullName", service.ClientId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipment, "Id", "Name", service.EquipmentId);
            ViewData["ManagerId"] = new SelectList(_context.Managers, "Id", "FullName", service.ManagerId);
            ViewData["WorkTeamId"] = new SelectList(_context.WorkTeams, "Id", "Id", service.WorkTeamId);
            ViewData["WorkTimeId"] = new SelectList(_context.WorkTimes, "Id", "Id", service.WorkTimeId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Name", service.WorkTypeId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Auto)
                .Include(s => s.Client)
                .Include(s => s.Equipment)
                .Include(s => s.Manager)
                .Include(s => s.WorkTeam)
                .Include(s => s.WorkTime)
                .Include(s => s.WorkType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'ServiceContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.Id == id);
        }
    }
}
