using Microsoft.EntityFrameworkCore;
namespace GitBetter.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int FacilityId { get; set; }
        public DateTime Date { get; set; }
        public Boolean IsScheduled { get; set; }
        public string Notes { get; set; }

        public User Patient { get; set; }
        public Facility Facility { get; set;}

    }
}