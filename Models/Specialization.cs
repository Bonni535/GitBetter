using Microsoft.EntityFrameworkCore;
namespace GitBetter.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //public ICollection<FacilitySpecialization> FacilitySpecializations { get; set; }
    }
}