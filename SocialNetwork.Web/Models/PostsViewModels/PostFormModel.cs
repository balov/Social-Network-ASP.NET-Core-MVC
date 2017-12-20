using Microsoft.AspNetCore.Http;
using SocialNetwork.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models.PostsViewModels
{
    public class PostFormModel
    {
        [Required]
        [Display(Name = "What do you think?")]
        public string Text { get; set; }

        [Display(Name = "How do you feel?")]
        public Feeling Feeling { get; set; }

        [Display(Name = "Upload a photo")]
        public IFormFile Photo { get; set; }
    }
}