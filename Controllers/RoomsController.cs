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
    public class RoomsController : Controller
    {
        private readonly HotelManagementContext _context;

        public RoomsController(HotelManagementContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var hotelManagementContext = _context.Rooms.Include(r => r.Hotel);
            return View(await hotelManagementContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
       [HttpGet]
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name");
            ViewData["TypeID"] = new SelectList(_context.RoomTypes, "TypeID", "Name");
            return View();
        }


        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,HotelID,TypeID,Status,RoomNumber")] Room room)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (ModelState.IsValid)
            {
                // Gán thông tin Hotel và RoomType dựa trên ID
                room.Hotel = await _context.Hotels.FindAsync(room.HotelID);
                room.RoomType = await _context.RoomTypes.FindAsync(room.TypeID);

                if (room.Hotel == null || room.RoomType == null)
                {
                    // Xử lý lỗi khi không tìm thấy Hotel hoặc RoomType
                    ModelState.AddModelError("", "Invalid Hotel or Room Type selection.");
                    ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name", room.HotelID);
                    ViewData["TypeID"] = new SelectList(_context.RoomTypes, "TypeID", "Name", room.TypeID);
                    return View(room);
                }

                // Thêm Room vào cơ sở dữ liệu
                _context.Add(room);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi, hiển thị lại form với dữ liệu đã nhập
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name", room.HotelID);
            ViewData["TypeID"] = new SelectList(_context.RoomTypes, "TypeID", "Name", room.TypeID);
            return View(room);
        }



        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomID,HotelID,TypeID,Status,RoomNumber")] Room room)
        {
            if (id != room.TypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.TypeID))
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
            return View(room);
        }


        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
