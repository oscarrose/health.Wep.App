using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Health.Web.App.Controllers
{
    [Authorize(Roles = "Doctor,Secretary")]
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

     
        private bool HistoryAppointmentExists(int id)
        {
            return _context.HistoryAppointments.Any(e => e.HistoryAppointmentId == id);
        }
    }
}
