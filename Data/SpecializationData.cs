using GitBetter.Models;

namespace GitBetter.Data
{
    public class SpecializationData
    {
        public static List<Specialization> Specializations = new List<Specialization>
        {
            new Specialization { Id = 1, Title = "Women's Health", Description= "A specialization related to all Women's Health issues" },
            new Specialization { Id = 2, Title = "Orthopedic ", Description = "A specialization related to all Orthopedic issues"},
        };
    }
}