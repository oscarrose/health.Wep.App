﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Health.Web.App.Services;

namespace Health.Web.App.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly SaludWebAppContext _context;
        private readonly IServicesAppointment _servicesAppointment;

        public AppointmentsController(SaludWebAppContext context, IServicesAppointment servicesAppointment)
        {
            _context = context;
            _servicesAppointment = servicesAppointment;
        }


       

        // GET: Appointments
        public async Task<IActionResult> Index(string status, string patientsSearch)
        {

            return View(await _servicesAppointment.GetAppointment(status, patientsSearch));


        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var appointment= _servicesAppointment.GetDetailAppoint(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AccountDoctorId,PatientId,DateAppointments,StartTime,EndTime,Status,Comment,SendEmailConfirmed")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _servicesAppointment.CreateAppointments(appointment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FisrtName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _servicesAppointment.GetDetailAppoint(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FisrtName", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AccountDoctorId,PatientId,DateAppointments,StartTime,EndTime,Status,Comment,SendEmailConfirmed")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servicesAppointment.Editappointments(appointment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FisrtaName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment =_servicesAppointment.GetDetailAppoint(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _servicesAppointment.DeleteAppointment(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _servicesAppointment.AppointmentsExists(id);
        }
    }
}
