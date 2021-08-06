using Health.Web.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Health.Web.App.Services
{
    public interface IServicesPatients
    {
       
        void createPatients(Patient patient);
        void DeletePatients(int id);
        void EditPatients(Patient patient);
        Patient GetDetailsPatients(int? id);
        Task<IEnumerable<Patient>> GetPatients(string searchString);
        bool PatientsExist(int id);
        string SendRegistrePatients(string FirstName, string LastName, string DNI, string HealthInsurance, string Disease, string AllergicMedicine);
    }
}