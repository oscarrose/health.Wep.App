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
    [Authorize(Roles = "Doctor")]
    public class TranckingAppointmentsController : Controller
    {
        private readonly SaludWebAppContext _context;

        public TranckingAppointmentsController(SaludWebAppContext context)
        {
            _context = context;
        }

        // GET: TranckingAppointments
        public async Task<IActionResult> Index()
        {
            var saludWebAppContext = _context.TranckingAppointments.Include(t => t.Appointment);
            return View(await saludWebAppContext.ToListAsync());
        }

        // GET: TranckingAppointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tranckingAppointment = await _context.TranckingAppointments
                .Include(t => t.Appointment)
                .FirstOrDefaultAsync(m => m.TranckingId == id);
            if (tranckingAppointment == null)
            {
                return NotFound();
            }

            return View(tranckingAppointment);
        }

        private bool TranckingAppointmentExists(int id)
        {
            return _context.TranckingAppointments.Any(e => e.TranckingId == id);
        }
    }
}
