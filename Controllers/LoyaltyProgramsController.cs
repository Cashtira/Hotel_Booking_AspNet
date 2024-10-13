using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCmodel.Models;

namespace MVCmodel.Controllers
{
    public class LoyaltyProgramsController : Controller
    {
        private readonly HotelManagementContext _context;

        public LoyaltyProgramsController(HotelManagementContext context)
        {
            _context = context;
        }

        // GET: LoyaltyPrograms
        public async Task<IActionResult> Index()
        {
            var hotelManagementContext = _context.LoyaltyPrograms.Include(l => l.Guest);
            return View(await hotelManagementContext.ToListAsync());
        }

        // GET: LoyaltyPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loyaltyProgram = await _context.LoyaltyPrograms
                .Include(l => l.Guest)
                .FirstOrDefaultAsync(m => m.ProgramID == id);
            if (loyaltyProgram == null)
            {
                return NotFound();
            }

            return View(loyaltyProgram);
        }

        // GET: LoyaltyPrograms/Create
        public IActionResult Create()
        {
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestID", "GuestID");
            return View();
        }

        // POST: LoyaltyPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramID,GuestID,Points,Tier")] LoyaltyProgram loyaltyProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loyaltyProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestID", "GuestID", loyaltyProgram.GuestID);
            return View(loyaltyProgram);
        }

        // GET: LoyaltyPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loyaltyProgram = await _context.LoyaltyPrograms.FindAsync(id);
            if (loyaltyProgram == null)
            {
                return NotFound();
            }
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestID", "GuestID", loyaltyProgram.GuestID);
            return View(loyaltyProgram);
        }

        // POST: LoyaltyPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramID,GuestID,Points,Tier")] LoyaltyProgram loyaltyProgram)
        {
            if (id != loyaltyProgram.ProgramID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loyaltyProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoyaltyProgramExists(loyaltyProgram.ProgramID))
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
            ViewData["GuestID"] = new SelectList(_context.Guests, "GuestID", "GuestID", loyaltyProgram.GuestID);
            return View(loyaltyProgram);
        }

        // GET: LoyaltyPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loyaltyProgram = await _context.LoyaltyPrograms
                .Include(l => l.Guest)
                .FirstOrDefaultAsync(m => m.ProgramID == id);
            if (loyaltyProgram == null)
            {
                return NotFound();
            }

            return View(loyaltyProgram);
        }

        // POST: LoyaltyPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loyaltyProgram = await _context.LoyaltyPrograms.FindAsync(id);
            if (loyaltyProgram != null)
            {
                _context.LoyaltyPrograms.Remove(loyaltyProgram);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoyaltyProgramExists(int id)
        {
            return _context.LoyaltyPrograms.Any(e => e.ProgramID == id);
        }
    }
}
