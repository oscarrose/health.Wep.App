using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    [Table("DoctorV1")]
    public partial class DoctorV1
    {
        [Key]
        [Column("DoctorID")]
        public int DoctorId { get; set; }
        [Column("AccountDoctorID")]
        [StringLength(450)]
        [DisplayName("Doctor")]
        public string AccountDoctorId { get; set; }
        [StringLength(256)]
        public string Email { get; set; }

        [ForeignKey(nameof(AccountDoctorId))]
        [InverseProperty(nameof(AspNetUser.DoctorV1s))]
        public virtual AspNetUser AccountDoctor { get; set; }
    }
}
