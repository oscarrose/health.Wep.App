using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Health.Web.App.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace Health.Web.App.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly SaludWebAppContext _context;
        private readonly IServicesAppointment _servicesAppointment;
        private readonly IEmailSender _emailSend;
      


        public AppointmentsController(SaludWebAppContext context, IServicesAppointment servicesAppointment, IEmailSender emailSend)
        {
            _context = context;
            _servicesAppointment = servicesAppointment;
            _emailSend = emailSend;
        }
       

        // GET: Appointments
        public async Task<IActionResult> Index(string status, string patientsSearch)
        {

            return View(await _servicesAppointment.GetAppointment(status, patientsSearch));


        }

        // GET: Appointments/Details/5 for start a appointments
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var appointment= _servicesAppointment.GetDetailAppoint(id);
           ServicesAppointment.thisStart = DateTime.Now;

            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.Start = ServicesAppointment.thisStart;
            ViewBag.Appoint = id;

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
                _servicesAppointment.GetEmailPatients(appointment.PatientId);
                _servicesAppointment.MessageAppoint(appointment.PatientId, appointment.AccountDoctorId,appointment.DateAppointments.ToLongDateString(),appointment.StartTime.ToString(), appointment.EndTime.ToString());

                await _emailSend.SendEmailAsync(ServicesAppointment.Get_Email_Patient_ForAppoint, "Confirmation of your appointment",
                    $"{ServicesAppointment.GetMessageAppoint} ");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
       public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _servicesAppointment.GetAppointEdit(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni", appointment.PatientId);
            return View (appointment);
        }

       
        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AccountDoctorId,PatientId,DateAppointments,StartTime,EndTime,Status,Comment,SendEmailConfirmed")] Appointment appointment)
        {
           
            if (id!= appointment.AppointmentId)
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni", appointment.PatientId);
            return View(appointment);
        }



        // GET: Appointments/end appointments
        public IActionResult EndAppoint(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _servicesAppointment.GetAppointEdit(id);
           ServicesAppointment.thisEnd = DateTime.Now;
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AccountDoctorId"] = new SelectList(_context.AspNetUsers, "Id", "Id", appointment.AccountDoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni", appointment.PatientId);
            return View(appointment);
        }

        /// <summary>
        /// For end a appointments for security
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EndAppoint(int id, [Bind("AppointmentId,AccountDoctorId,PatientId,DateAppointments,StartTime,EndTime,Status,Comment,SendEmailConfirmed")] Appointment appointment)
        {

            if (id != appointment.AppointmentId)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servicesAppointment.InsertTrancking(id,ServicesAppointment.thisStart, ServicesAppointment.thisEnd);
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "Dni", appointment.PatientId);
            return View(appointment);
        }



  

        private bool AppointmentExists(int id)
        {
            return _servicesAppointment.AppointmentsExists(id);
        }
        

    }

 
}
