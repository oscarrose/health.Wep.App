using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Health.Web.App.Services
{
    public class ServicesPatients : IServicesPatients
    {
        public static string getmessage;
        
        private readonly SaludWebAppContext _saludWebAppContext;
         
        public ServicesPatients(SaludWebAppContext saludWebAppContext)
        {
            _saludWebAppContext = saludWebAppContext;
           
        }

        /// <summary>
        /// /for show of data of the patients
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Patient>> GetPatients(string searchString)
        {
            var patients = from p in _saludWebAppContext.Patients
                           select p;
            if (!(String.IsNullOrEmpty(searchString)))
            {
                patients = patients.Where(p => p.FirstName.Contains(searchString));

            }

            return await (patients.OrderBy(o => o.PatientId).ToListAsync());

        }

        /// <summary>
        /// get datail and dalete and edit of patients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Patient GetDetailsPatients(int? id)
        {
            return _saludWebAppContext.Patients
                 .FirstOrDefault(m => m.PatientId == id);
        }

        /// <summary>
        /// create new patients
        /// </summary>
        /// <param name="patient"></param>
        public void createPatients(Patient patient)
        {
            _saludWebAppContext.Add(patient);
            _saludWebAppContext.SaveChanges();
        }

        public void EditPatients(Patient patient)
        {
            _saludWebAppContext.Update(patient);
            _saludWebAppContext.SaveChanges();


        }

        /// <summary>
        /// For delete a patients
        /// </summary>
        /// <param name="patient"></param>
        public void DeletePatients(int id)
        {
            var patient = _saludWebAppContext.Patients.Find(id);
            _saludWebAppContext.Patients.Remove(patient);
            _saludWebAppContext.SaveChanges();
        }

        public bool PatientsExist(int id)
        {
            return _saludWebAppContext.Patients.Any(e => e.PatientId == id);
        }

        /// <summary>
        /// create the msg of send a patients
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="DNI"></param>
        /// <param name="HealthInsurance"></param>
        /// <param name="Disease"></param>
        /// <param name="AllergicMedicine"></param>
        /// <returns></returns>
        public string SendRegistrePatients(string FirstName, string LastName, string DNI, string HealthInsurance, string Disease, string AllergicMedicine)
        {
            string message = $"Hello, {FirstName + "" + LastName } was registered in health was completed, your: {DNI}, " +
                 $"your: {HealthInsurance}, your: {Disease}, has a: {Disease}, have: {AllergicMedicine}  ";

            

            return getmessage= message;
        }

        


    }
}
