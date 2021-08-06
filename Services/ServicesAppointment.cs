using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Health.Web.App.Services
{
    public class ServicesAppointment : IServicesAppointment
    {
        private readonly SaludWebAppContext _saludAppointment;
        public ServicesAppointment(SaludWebAppContext saludWebApp)
        {
            _saludAppointment = saludWebApp;
        }

        public bool AppointmentsExists(int id)
        {

            return _saludAppointment.Appointments.Any(e => e.AppointmentId == id);
        }

        /// <summary>
        /// for create new appointments
        /// </summary>
        /// <param name="appointment"></param>
        public void CreateAppointments(Appointment appointment)
        {
            _saludAppointment.Appointments.Add(appointment);
            _saludAppointment.SaveChanges();
        }

        public void DeleteAppointment(int id)
        {
            var appointment=_saludAppointment.Appointments.Find(id);
            _saludAppointment.Appointments.Remove(appointment);
            _saludAppointment.SaveChanges();
        }

        /// <summary>
        /// for edit the appointments
        /// </summary>
        /// <param name="appointment"></param>
        public void Editappointments(Appointment appointment)
        {
            _saludAppointment.Update(appointment);
             _saludAppointment.SaveChanges();
        }

        /// <summary>
        /// for list the date the appointments
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Appointment>> GetAppointment(string status, string patientsSearch)
        {
            var appointments = from a in _saludAppointment.Appointments
                               select a;

            // Use LINQ to get list of genres.
            //IQueryable<string> statusQuery = from a in _saludAppointment.Appointments
            //                                 orderby a.Status
            //                                 select a.Status;

            var appoint = _saludAppointment.Appointments.Include(a => a.AccountDoctor).Include(a => a.Patient);

            if (!String.IsNullOrEmpty(patientsSearch))
            {
                appointments = _saludAppointment.Appointments.Where(s =>Convert.ToString (s.PatientId).Contains(patientsSearch));

            }

            if (!string.IsNullOrEmpty(status))
            {
                appointments = appointments.Where(x => x.Status==status);
            }


            return await (appointments.ToListAsync());

        }

        /// <summary>
        /// get id for start appointments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Appointment GetDetailAppoint(int? id)
        {
            return _saludAppointment.Appointments
               .Include(a => a.AccountDoctor)
               .Include(a => a.Patient)
               .FirstOrDefault(m => m.AppointmentId == id);
        }
    }
}
