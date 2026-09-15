using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Authorize]
    public class OrganizersController : Controller
    {
        private readonly AppDbContext _context;

        public OrganizersController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Organizers
        public async Task<IActionResult> Index()
        {
            var organizers = await _context.Organizers.ToListAsync();
            return View(organizers);
        }

        // GET /Organizers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST /Organizers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PasswordHash")] Organizer newOrganizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newOrganizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newOrganizer);
        }

        // GET /Organizers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer == null) return NotFound();

            return View(organizer);
        }

        // POST /Organizers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,PasswordHash")] Organizer updatedOrganizer)
        {
            if (id != updatedOrganizer.ID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedOrganizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(updatedOrganizer);
        }

        // GET /Organizers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.ID == id);
            if (organizer == null) return NotFound();

            return View(organizer);
        }

        // POST /Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
