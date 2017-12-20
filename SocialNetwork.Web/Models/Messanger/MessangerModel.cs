using SocialNetwork.Data;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Web.Models.Messanger
{
    public class MessangerModel
    {
        public PaginatedList<MessageModel> Messages { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxMessageLength)]
        public string MessageText { get; set; }
    }
}