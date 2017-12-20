using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;
using System;

namespace SocialNetwork.Services.Models
{
    public class MessageModel : IMapFrom<Message>, IHaveCustomMapping
    {
        public string SenderId { get; set; }

        public string MessageText { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsSeen { get; set; }

        public string SenderFullName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, MessageModel>()
                 .ForMember(m => m.SenderFullName, cfg => cfg.MapFrom(m => m.Sender.FirstName + " " + m.Sender.LastName));
        }
    }
}