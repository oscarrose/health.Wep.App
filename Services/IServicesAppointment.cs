using Health.Web.App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health.Web.App.Services
{
    
    public interface IServicesAppointment
    {

        void SendUpdateAppointment(string status, string DateAppoint, string StartTime, string EndTime);
        void InsertTrancking(int AppointId, DateTime Start, DateTime End);
        Task<IEnumerable<Appointment>> GetAppointment(string status, string patientsSearch);
        Appointment GetDetailAppoint(int? id);
        void CreateAppointments(Appointment appointment);
        void Editappointments(Appointment appointment);
        void DeleteAppointment(int id);
        bool AppointmentsExists(int id);

        Appointment GetAppointEdit(int? id);
        void MessageAppoint(int patientId, string accountDoctorId, string DateAppoint, string startTime, string EndTime);
        string GetEmailPatients(int patientId);
        bool DisponibleAppointment(string accountDoctorId, DateTime DateAppoitment,TimeSpan startTime, TimeSpan endTime);
    }
}