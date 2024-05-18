using GitBetter.Models;

namespace GitBetter.Data
{
    public class FacilitySpecializationData
    {
        public static List<FacilitySpecialization> FacilitySpecializations = new List<FacilitySpecialization>
        {
            new FacilitySpecialization { Id = 1, FacilityId = 1, SpecializationId = 2},
            new FacilitySpecialization { Id = 2, FacilityId = 2, SpecializationId = 2},
        };
    }
}