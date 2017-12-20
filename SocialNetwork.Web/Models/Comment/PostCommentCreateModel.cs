using AutoMapper;
using SocialNetwork.Common.Mapping;
using SocialNetwork.Data.Entities.Enums;
using SocialNetwork.Services.Models;
using SocialNetwork.Web.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models.Comment
{
    public class PostCommentCreateModel : IMapFrom<PostModel>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public string Photo { get; set; }

        public string UserProfilePicture { get; set; }

        public string UserFullName { get; set; }

        public Feeling Feeling { get; set; }

        [Required]
        [Display(Name ="Comment:")]
        public string CommentText { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<PostModel, PostCommentCreateModel>()
                .ForMember(p => p.CommentText, cfg => cfg.Ignore())
                .ForMember(p => p.Photo, cfg => cfg.MapFrom(p => p.Photo.ToRenderablePictureString()))
                .ForMember(p => p.UserProfilePicture, cfg => cfg.MapFrom(p => p.UserProfilePicture.ToRenderablePictureString()));
        }
    }
}