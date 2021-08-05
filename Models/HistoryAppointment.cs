using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    [Table("HistoryAppointment")]
    public partial class HistoryAppointment
    {
        [Key]
        [Column("HistoryAppointmentID")]
        public int HistoryAppointmentId { get; set; }
        [Column("AppointmentID")]
        public int AppointmentId { get; set; }
        [Column("PatientID")]
        public int PatientId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? DurationAppointmentMinutes { get; set; }
        [StringLength(255)]
        public string CommentAppointment { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("HistoryAppointments")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(PatientId))]
        [InverseProperty("HistoryAppointments")]
        public virtual Patient Patient { get; set; }
    }
}
