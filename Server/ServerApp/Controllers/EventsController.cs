using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        // GET /Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST /Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Date,Location")] Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newEvent);
        }

        // GET /Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        // POST /Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Date,Location")] Event updatedEvent)
        {
            if (id != updatedEvent.ID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(updatedEvent);
        }

        // GET /Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.Events.FirstOrDefaultAsync(e => e.ID == id);
            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        // POST /Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem != null)
            {
                _context.Events.Remove(eventItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
