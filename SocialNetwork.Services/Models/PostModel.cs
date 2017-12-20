using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Models
{
    public class PostModel : IMapFrom<Post>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public byte[] Photo { get; set; }

        public byte[] UserProfilePicture { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public Feeling Feeling { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; } = new List<CommentModel>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Post, PostModel>()
                .ForMember(p => p.UserProfilePicture, cfg => cfg.MapFrom(p => p.User.ProfilePicture))
                .ForMember(p => p.UserFullName, cfg => cfg.MapFrom(p => p.User.FirstName + " " + p.User.LastName))
                .ForMember(p => p.Comments, cfg => cfg.MapFrom(p => Mapper.Map<IEnumerable<CommentModel>>(p.Comments)));
        }
    }
}