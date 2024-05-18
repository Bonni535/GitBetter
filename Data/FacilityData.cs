using GitBetter.Models;

namespace GitBetter.Data
{
    public class FacilityData
    {
        public static List<Facility> Facilities = new List<Facility>
        {
            new Facility { Id = 1, Name = "PTDoneRight", Address = "", Description = ""},
            new Facility { Id = 2, Name = "WalkAnew", Address = "", Description = ""},
        };
    }
}