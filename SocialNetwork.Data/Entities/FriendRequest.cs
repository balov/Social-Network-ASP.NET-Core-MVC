using SocialNetwork.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class FriendRequest
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        [Required]
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}