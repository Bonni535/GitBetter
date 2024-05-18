using Microsoft.EntityFrameworkCore;
namespace GitBetter.Models
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<FacilitySpecialization> FacilitySpecializations { get; set; }
    }
}