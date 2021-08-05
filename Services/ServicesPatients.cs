using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health.Web.App.Data;
using Health.Web.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Health.Web.App.Services
{
    public class ServicesPatients
    {
        private readonly SaludWebAppContext _saludWebAppContext;

        public ServicesPatients(SaludWebAppContext saludWebAppContext)
        {
            _saludWebAppContext = saludWebAppContext;
        }


        public async  Task<IEnumerable<Patient>> GetPatients(string searchString)
        {
            var patients = from p in _saludWebAppContext.Patients
                           select p;
            if (!(String.IsNullOrEmpty(searchString)))
            {
                patients = patients.Where(p=> p.FirstName.Contains(searchString));

            }

            return await (patients.OrderBy(o => o.PatientId).ToListAsync());
                /* _saludWebAppContext.Patients.OrderBy(o=>o.PatientId).ToListAsync();*/
        }
    }
}
