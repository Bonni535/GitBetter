using Microsoft.EntityFrameworkCore;
namespace GitBetter.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public Boolean IsProvider { get; set; }
        public Boolean IsFavorite { get; set; }
        public int? FacilityId { get; set; }
        public int Uid { get; set; }

        public Facility? Facility { get; set;}
        public ICollection<Appointment> Appointments { get; set; }
    }
}