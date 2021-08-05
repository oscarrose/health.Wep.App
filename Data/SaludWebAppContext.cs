using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Health.Web.App.Models;

#nullable disable

namespace Health.Web.App.Data
{
    public partial class SaludWebAppContext : DbContext
    {
        public SaludWebAppContext()
        {
        }

        public SaludWebAppContext(DbContextOptions<SaludWebAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<HistoryAppointment> HistoryAppointments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<TranckingAppointment> TranckingAppointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.AccountDoctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AccountDoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Appointments_AccountID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_Appointments_PatientID");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.AccountType).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.NumberPhone).IsUnicode(false);

                entity.Property(e => e.Speciality).IsUnicode(false);
            });

            modelBuilder.Entity<HistoryAppointment>(entity =>
            {
                entity.Property(e => e.HistoryAppointmentId).ValueGeneratedNever();

                entity.Property(e => e.CommentAppointment).IsUnicode(false);

                entity.Property(e => e.DurationAppointmentMinutes).HasComputedColumnSql("(datediff(minute,[StartTime],[EndTime]))", false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.HistoryAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_AppointmentID");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.HistoryAppointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patients_History_PatientID");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.AllergicMedicine).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.Disease).IsUnicode(false);

                entity.Property(e => e.Dni).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.HealthInsurance).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.NumberPhone).IsUnicode(false);

                entity.Property(e => e.Street).IsUnicode(false);
            });

            modelBuilder.Entity<TranckingAppointment>(entity =>
            {
                entity.HasKey(e => e.TranckingId)
                    .HasName("PK_Trancking");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.TranckingAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK_Appointments_Trancking_AppointmentID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
