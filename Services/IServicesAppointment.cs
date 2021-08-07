using Health.Web.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health.Web.App.Services
{
    
    public interface IServicesAppointment
    {
        Task<IEnumerable<Appointment>> GetAppointment(string status, string patientsSearch);
        Appointment GetDetailAppoint(int? id);
        void CreateAppointments(Appointment appointment);
        void Editappointments(Appointment appointment);
        void DeleteAppointment(int id);
        bool AppointmentsExists(int id);
     

        void MessageAppoint(int patientId, string accountDoctorId, string DateAppoint, string startTime, string EndTime);
        string GetEmailPatients(int patientId);
    }
}