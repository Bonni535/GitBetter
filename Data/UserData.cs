using GitBetter.Models;

namespace GitBetter.Data
{
    public class UserData
    {
        public static List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "James", Bio = "Hi I'm James!", Email = "JM@gmail.com", IsProvider = false, IsFavorite = false, FacilityId = 1, Uid = 1},
            new User { Id = 2, Name = "John", Bio = "", Email = "JHH@gmail.com", IsProvider = true, IsFavorite = false, FacilityId= 2, Uid = 2},
        };
    }
}