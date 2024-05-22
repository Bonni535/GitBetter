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

    }
}