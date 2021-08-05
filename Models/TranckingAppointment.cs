using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    public partial class TranckingAppointment
    {
        [Key]
        [Column("TranckingID")]
        public int TranckingId { get; set; }
        [Column("AppointmentID")]
        public int? AppointmentId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("TranckingAppointments")]
        public virtual Appointment Appointment { get; set; }
    }
}
