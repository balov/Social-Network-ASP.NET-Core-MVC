using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Entities.Enums;

namespace SocialNetwork.Services.Models
{
    public class ReceivedFriendRequestModel : IMapFrom<FriendRequest>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string SenderFullName { get; set; }

        public FriendRequestStatus FriendRequestStatus { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<FriendRequest, ReceivedFriendRequestModel>()
                .ForMember(f => f.SenderFullName, cfg => cfg.MapFrom(f => f.Sender.FirstName + " " + f.Sender.LastName));
        }
    }
}