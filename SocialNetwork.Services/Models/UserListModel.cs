using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Services.Models
{
    public class UserListModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int NumberOfPosts { get; set; }

        public byte[] ProfilePicture { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserListModel>()
                .ForMember(u => u.FullName, cfg => cfg.MapFrom(u => u.FirstName + " " + u.LastName))
                .ForMember(u => u.NumberOfPosts, cfg => cfg.MapFrom(u => u.Posts.Count));
        }
    }
}