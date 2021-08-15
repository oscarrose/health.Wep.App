using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Health.Web.App.Models
{
    [Table("Doctor")]
    public partial class Doctor
    {
        [Key]
        [Column("DoctorID")]
        public int DoctorId { get; set; }
        [Required]
        [Column("AccountDoctorID")]
        [StringLength(450)]
        public string AccountDoctorId { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [StringLength(20)]
        public string NumberPhone { get; set; }
        [Required]
        [StringLength(15)]
        public string AccountType { get; set; }
        [StringLength(30)]
        public string Speciality { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [ForeignKey(nameof(AccountDoctorId))]
        [InverseProperty(nameof(AspNetUser.Doctors))]
        public virtual AspNetUser AccountDoctor { get; set; }
    }
}
