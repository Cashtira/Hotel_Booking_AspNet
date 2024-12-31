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
    public class HousekeepingsController : Controller
    {
        private readonly HotelManagementContext _context;

        public HousekeepingsController(HotelManagementContext context)
        {
            _context = context;
        }

        // GET: Housekeepings
        public async Task<IActionResult> Index()
        {
            var hotelManagementContext = _context.Housekeepings.Include(h => h.Room).Include(h => h.Staff);
            return View(await hotelManagementContext.ToListAsync());
        }

        // GET: Housekeepings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housekeeping = await _context.Housekeepings
                .Include(h => h.Room)
                .Include(h => h.Staff)
                .FirstOrDefaultAsync(m => m.HousekeepingID == id);
            if (housekeeping == null)
            {
                return NotFound();
            }

            return View(housekeeping);
        }

        // GET: Housekeepings/Create
        public IActionResult Create()
        {
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID");
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffID");
            return View();
        }

        // POST: Housekeepings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HousekeepingID,RoomID,StaffID,DateCleaned,Status")] Housekeeping housekeeping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(housekeeping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", housekeeping.RoomID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffID", housekeeping.StaffID);
            return View(housekeeping);
        }

        // GET: Housekeepings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housekeeping = await _context.Housekeepings.FindAsync(id);
            if (housekeeping == null)
            {
                return NotFound();
            }
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", housekeeping.RoomID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffID", housekeeping.StaffID);
            return View(housekeeping);
        }

        // POST: Housekeepings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HousekeepingID,RoomID,StaffID,DateCleaned,Status")] Housekeeping housekeeping)
        {
            if (id != housekeeping.HousekeepingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(housekeeping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousekeepingExists(housekeeping.HousekeepingID))
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
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", housekeeping.RoomID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffID", housekeeping.StaffID);
            return View(housekeeping);
        }

        // GET: Housekeepings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housekeeping = await _context.Housekeepings
                .Include(h => h.Room)
                .Include(h => h.Staff)
                .FirstOrDefaultAsync(m => m.HousekeepingID == id);
            if (housekeeping == null)
            {
                return NotFound();
            }

            return View(housekeeping);
        }

        // POST: Housekeepings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var housekeeping = await _context.Housekeepings.FindAsync(id);
            if (housekeeping != null)
            {
                _context.Housekeepings.Remove(housekeeping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousekeepingExists(int id)
        {
            return _context.Housekeepings.Any(e => e.HousekeepingID == id);
        }
    }
}
