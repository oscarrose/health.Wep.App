using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    [Table("patients")]
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            HistoryAppointments = new HashSet<HistoryAppointment>();
        }
        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        [Key]
        [Column("PatientID")]
        public int PatientId { get; set; }
        [Required]
        [StringLength(25)]
        [DisplayName("Fist Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
         [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [Column("DNI")]
        [StringLength(25)]
        [DisplayName("DNI")]
        public string Dni { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Date Birth")]
        public DateTime DateBirth { get; set; }
        [StringLength(20)]
        [DisplayName("Phone")]
        public string NumberPhone { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [Column("city")]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        public string Street { get; set; }
        [StringLength(25)]
        [DisplayName("Health Insurance")]
        public string HealthInsurance { get; set; }
        [StringLength(25)]
        public string Disease { get; set; }
        [StringLength(25)]
        [DisplayName("Allergic Medicine")]
        public string AllergicMedicine { get; set; }
        public bool? SendEmailConfirmed { get; set; }
        [StringLength(100)]
        public string Email { get; set; }

        [InverseProperty(nameof(Appointment.Patient))]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty(nameof(HistoryAppointment.Patient))]
        public virtual ICollection<HistoryAppointment> HistoryAppointments { get; set; }
    }
}
