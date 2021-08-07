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
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;


namespace Health.Web.App.Controllers
{
    [AllowAnonymous]
    public class PatientsController : Controller
    {
 
        private readonly IServicesPatients _servicesPatients;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<PatientsController> _logger;

        //private readonly EmailSender _emailSender;

        public PatientsController( IServicesPatients servicesPatients, IEmailSender emailSender, ILogger<PatientsController> logger)
        {
         
            _servicesPatients = servicesPatients;
            _emailSender = emailSender;
            _logger = logger;


        }

        // GET: Patients
        public async Task<IActionResult> Index(string SearchString)
        {
            return View(await _servicesPatients.GetPatients(SearchString));
        }

        // GET: Patients/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _servicesPatients.GetDetailsPatients(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,FirstName,LastName,Email,Dni,DateBirth,NumberPhone,Country,City,Street,HealthInsurance,Disease,AllergicMedicine,SendEmailConfirmed")] Patient patient)
        {
            if (ModelState.IsValid)     
            {
                _servicesPatients.createPatients(patient);

                _servicesPatients.SendDateOfPatients(patient.Email, patient.FirstName, patient.LastName, patient.Dni, patient.DateBirth.ToString(), patient.NumberPhone,
                          patient.Country, patient.City, patient.Street, patient.HealthInsurance, patient.Disease, patient.AllergicMedicine); 

                var callbackUrl = Url.Page(
                        "/Home/Index",
                        pageHandler: null,
                        values: new { patientdId = patient.PatientId },
                        protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(patient.Email,"Confirm your registration",
                    $"{ServicesPatients.getmessage} <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _servicesPatients.GetDetailsPatients(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,FirstName,LastName,Email,Dni,DateBirth,NumberPhone,Country,City,Street,HealthInsurance,Disease,AllergicMedicine,SendEmailConfirmed")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servicesPatients.EditPatients(patient);


                    _servicesPatients.SendDateOfPatients(patient.Email, patient.FirstName, patient.LastName, patient.Dni, patient.DateBirth.ToString(), patient.NumberPhone,
                        patient.Country, patient.City, patient.Street, patient.HealthInsurance, patient.Disease, patient.AllergicMedicine);

                    var callbackUrl = Url.Page(
                            "/Home/Index",
                            pageHandler: null,
                            values: new { patientdId = patient.PatientId },
                            protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(patient.Email, "your data was updated",
                        $"{ServicesPatients.getmessage} <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _servicesPatients.GetDetailsPatients(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _servicesPatients.DeletePatients(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _servicesPatients.PatientsExist( id);
        }
    }
}
