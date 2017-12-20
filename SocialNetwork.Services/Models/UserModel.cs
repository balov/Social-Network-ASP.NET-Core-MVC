using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Services.Models
{
    public class UserModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }
    }
}