using Microsoft.EntityFrameworkCore;
using GitBetter.Models;
using GitBetter.Data;

public class GitBetterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<FacilitySpecialization> FacilitySpecializations { get; set;}

    public GitBetterDbContext(DbContextOptions<GitBetterDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(UserData.Users);
        modelBuilder.Entity<Facility>().HasData(FacilityData.Facilities);
        modelBuilder.Entity<Specialization>().HasData(SpecializationData.Specializations);
        modelBuilder.Entity<Appointment>().HasData(AppointmentData.Appointments);
        modelBuilder.Entity<FacilitySpecialization>().HasData(FacilitySpecializationData.FacilitySpecializations);


        // User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Facility)
            .WithMany(f => f.Users)
            .HasForeignKey(u => u.FacilityId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.PatientAppointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ProviderAppointments)
            .WithOne(a => a.Provider)
            .HasForeignKey(a => a.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        // FacilitySpecialization
        modelBuilder.Entity<FacilitySpecialization>()
            .HasOne(fs => fs.Facility)
            .WithMany(f => f.FacilitySpecializations)
            .HasForeignKey(fs => fs.FacilityId);

        modelBuilder.Entity<FacilitySpecialization>()
            .HasOne(fs => fs.Specialization)
            .WithMany(s => s.FacilitySpecializations)
            .HasForeignKey(fs => fs.SpecializationId);

        // Appointment
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(u => u.PatientAppointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Provider)
            .WithMany(u => u.ProviderAppointments)
            .HasForeignKey(a => a.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Facility)
            .WithMany(f => f.Appointments)
            .HasForeignKey(a => a.FacilityId);
    }
}