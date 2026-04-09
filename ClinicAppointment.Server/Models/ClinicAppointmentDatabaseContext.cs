using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentProject.Models
{
    public class ClinicAppointmentDatabaseContext (DbContextOptions<ClinicAppointmentDatabaseContext> options) :DbContext (options)
    {

        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<StatusLookup> StatusLookup { get; set; }
        public virtual DbSet<Specialty> Specialty { get; set; }
        public virtual DbSet<AvailabilitySlots> AvailabilitySlots { get; set; }
        public virtual DbSet<StatusHistory> StatusHistory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.doctor_id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.patient_id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
