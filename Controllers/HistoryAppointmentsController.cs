using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health.Web.App.Data;
using Health.Web.App.Models;

namespace Health.Web.App.Controllers
{
    public class HistoryAppointmentsController : Controller
    {
        private readonly SaludWebAppContext _context;

        public HistoryAppointmentsController(SaludWebAppContext context)
        {
            _context = context;
        }

        // GET: HistoryAppointments
        public async Task<IActionResult> Index()
        {
            var saludWebAppContext = _context.HistoryAppointments.Include(h => h.Appointment).Include(h => h.Patient);
            return View(await saludWebAppContext.ToListAsync());
        }

        // GET: HistoryAppointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyAppointment = await _context.HistoryAppointments
                .Include(h => h.Appointment)
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.HistoryAppointmentId == id);
            if (historyAppointment == null)
            {
                return NotFound();
            }

            return View(historyAppointment);
        }

        // GET: HistoryAppointments/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AccountDoctorId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "City");
            return View();
        }

        // POST: HistoryAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryAppointmentId,AppointmentId,PatientId,StartTime,EndTime,DurationAppointmentMinutes,CommentAppointment")] HistoryAppointment historyAppointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historyAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AccountDoctorId", historyAppointment.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "City", historyAppointment.PatientId);
            return View(historyAppointment);
        }

        // GET: HistoryAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyAppointment = await _context.HistoryAppointments.FindAsync(id);
            if (historyAppointment == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AccountDoctorId", historyAppointment.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "City", historyAppointment.PatientId);
            return View(historyAppointment);
        }

        // POST: HistoryAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryAppointmentId,AppointmentId,PatientId,StartTime,EndTime,DurationAppointmentMinutes,CommentAppointment")] HistoryAppointment historyAppointment)
        {
            if (id != historyAppointment.HistoryAppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historyAppointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoryAppointmentExists(historyAppointment.HistoryAppointmentId))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AccountDoctorId", historyAppointment.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "City", historyAppointment.PatientId);
            return View(historyAppointment);
        }

        // GET: HistoryAppointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyAppointment = await _context.HistoryAppointments
                .Include(h => h.Appointment)
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.HistoryAppointmentId == id);
            if (historyAppointment == null)
            {
                return NotFound();
            }

            return View(historyAppointment);
        }

        // POST: HistoryAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historyAppointment = await _context.HistoryAppointments.FindAsync(id);
            _context.HistoryAppointments.Remove(historyAppointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoryAppointmentExists(int id)
        {
            return _context.HistoryAppointments.Any(e => e.HistoryAppointmentId == id);
        }
    }
}
