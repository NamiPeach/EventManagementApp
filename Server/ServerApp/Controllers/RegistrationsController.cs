using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly AppDbContext _context;

        public RegistrationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Registrations
        public async Task<IActionResult> Index()
        {
            var registrations = await _context.Registrations
                .Include(r => r.Organizer)
                .Include(r => r.Event)
                .ToListAsync();
            return View(registrations);
        }

        // GET /Registrations/Create
        public IActionResult Create()
        {
            ViewBag.Organizers = new SelectList(_context.Organizers, "ID", "Name");
            ViewBag.Events = new SelectList(_context.Events, "ID", "Name");
            return View();
        }

        // POST /Registrations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizerID,EventID,ParticipantName,Status")] Registration newRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Organizers = new SelectList(_context.Organizers, "ID", "Name", newRegistration.OrganizerID);
            ViewBag.Events = new SelectList(_context.Events, "ID", "Name", newRegistration.EventID);
            return View(newRegistration);
        }

        // GET /Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null) return NotFound();

            ViewBag.Organizers = new SelectList(_context.Organizers, "ID", "Name", registration.OrganizerID);
            ViewBag.Events = new SelectList(_context.Events, "ID", "Name", registration.EventID);
            return View(registration);
        }

        // POST /Registrations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrganizerID,EventID,ParticipantName,Status")] Registration updatedRegistration)
        {
            if (id != updatedRegistration.ID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Organizers = new SelectList(_context.Organizers, "ID", "Name", updatedRegistration.OrganizerID);
            ViewBag.Events = new SelectList(_context.Events, "ID", "Name", updatedRegistration.EventID);
            return View(updatedRegistration);
        }

        // GET /Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var registration = await _context.Registrations
                .Include(r => r.Organizer)
                .Include(r => r.Event)
                .FirstOrDefaultAsync(r => r.ID == id);
            if (registration == null) return NotFound();
            return View(registration);
        }

        // POST /Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
