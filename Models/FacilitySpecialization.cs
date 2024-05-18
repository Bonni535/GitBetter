using Microsoft.EntityFrameworkCore;
namespace GitBetter.Models
{
    public class FacilitySpecialization
    {
        public int Id { get; set; }
        public int FacilityId { get; set; }
        public int SpecializationId { get; set; }
        public Facility Facility { get; set; }
        public Specialization Specialization { get; set; }

    }
}