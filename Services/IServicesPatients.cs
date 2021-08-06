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
        string SendDateOfPatients(string email,string FirstName, string LastName, string DNI,string DateBrith,string phone,
            string country,string city,string street,string HealthInsurance, string Disease, string AllergicMedicine);
    }
}