using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            HistoryAppointments = new HashSet<HistoryAppointment>();
            TranckingAppointments = new HashSet<TranckingAppointment>();
        }

        [Key]
        [Column("AppointmentID")]
        public int AppointmentId { get; set; }
        [Required]
        [Column("AccountDoctorID")]
        [StringLength(450)]
        public string AccountDoctorId { get; set; }
        [Column("PatientID")]
        public int PatientId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateAppointments { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [StringLength(12)]
        public string Status { get; set; }
        [StringLength(255)]
        public string Comment { get; set; }
        public bool? SendEmailConfirmed { get; set; }

        [ForeignKey(nameof(AccountDoctorId))]
        [InverseProperty(nameof(AspNetUser.Appointments))]
        public virtual AspNetUser AccountDoctor { get; set; }
        [ForeignKey(nameof(PatientId))]
        [InverseProperty("Appointments")]
        public virtual Patient Patient { get; set; }
        [InverseProperty(nameof(HistoryAppointment.Appointment))]
        public virtual ICollection<HistoryAppointment> HistoryAppointments { get; set; }
        [InverseProperty(nameof(TranckingAppointment.Appointment))]
        public virtual ICollection<TranckingAppointment> TranckingAppointments { get; set; }
    }
}
