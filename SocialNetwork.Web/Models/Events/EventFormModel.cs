using SocialNetwork.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models.Events
{
    public class EventFormModel
    {
        [Url]
        [Display(Name ="Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxEventTitleLength)]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime DateStarts { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime DateEnds { get; set; }
    }
}