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
    public class DiscountCardsController : Controller
    {
        private readonly ServiceContext _context;

        public DiscountCardsController(ServiceContext context)
        {
            _context = context;
        }

        // GET: DiscountCards
        public async Task<IActionResult> Index()
        {
              return View(await _context.DiscountCards.ToListAsync());
        }

        // GET: DiscountCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiscountCards == null)
            {
                return NotFound();
            }

            var discountCard = await _context.DiscountCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCard == null)
            {
                return NotFound();
            }

            return View(discountCard);
        }

        // GET: DiscountCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscountCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BonusesTotal,DiscountTotal")] DiscountCard discountCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountCard);
        }

        // GET: DiscountCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiscountCards == null)
            {
                return NotFound();
            }

            var discountCard = await _context.DiscountCards.FindAsync(id);
            if (discountCard == null)
            {
                return NotFound();
            }
            return View(discountCard);
        }

        // POST: DiscountCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BonusesTotal,DiscountTotal")] DiscountCard discountCard)
        {
            if (id != discountCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountCardExists(discountCard.Id))
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
            return View(discountCard);
        }

        // GET: DiscountCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiscountCards == null)
            {
                return NotFound();
            }

            var discountCard = await _context.DiscountCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discountCard == null)
            {
                return NotFound();
            }

            return View(discountCard);
        }

        // POST: DiscountCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiscountCards == null)
            {
                return Problem("Entity set 'ServiceContext.DiscountCards'  is null.");
            }
            var discountCard = await _context.DiscountCards.FindAsync(id);
            if (discountCard != null)
            {
                _context.DiscountCards.Remove(discountCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountCardExists(int id)
        {
          return _context.DiscountCards.Any(e => e.Id == id);
        }
    }
}
