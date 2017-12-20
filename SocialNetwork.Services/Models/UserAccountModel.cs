using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using System.Collections.Generic;

namespace SocialNetwork.Services.Models
{
    public class UserAccountModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public byte[] ProfilePicture { get; set; }

        public PaginatedList<PostModel> Posts { get; set; } = null;

        public IEnumerable<ReceivedFriendRequestModel> FriendRequestReceived { get; set; } = new List<ReceivedFriendRequestModel>();

        public IEnumerable<SentFriendRequestModel> FriendRequestSent { get; set; } = new List<SentFriendRequestModel>();

        public IEnumerable<UserListModel> Friends { get; set; } = new List<UserListModel>();

        public IEnumerable<EventModel> Events { get; set; } = new List<EventModel>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<User, UserAccountModel>()
                .ForMember(u => u.Posts, cfg => cfg.Ignore())
                .ForMember(u => u.Friends, cfg => cfg.Ignore());
        }
    }
}