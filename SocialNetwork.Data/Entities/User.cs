using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class User : IdentityUser
    {
        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(DataConstants.NameMinLength), MaxLength(DataConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [Range(DataConstants.MinUserAge, DataConstants.MaxUserAge)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();

        [Required]
        [MaxLength(DataConstants.MaxPhotoLength)]
        public byte[] ProfilePicture { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<FriendRequest> FriendRequestSent { get; set; } = new List<FriendRequest>();

        public ICollection<FriendRequest> FriendRequestReceived { get; set; } = new List<FriendRequest>();

        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();

        public ICollection<Message> MessagesReceived { get; set; } = new List<Message>();

        public ICollection<UserFriend> Friends { get; set; } = new List<UserFriend>();

        public ICollection<UserFriend> OtherFriends { get; set; } = new List<UserFriend>();

        public ICollection<UserInterest> Interests { get; set; } = new List<UserInterest>();

        public ICollection<EventUser> Events { get; set; } = new List<EventUser>();
    }
}