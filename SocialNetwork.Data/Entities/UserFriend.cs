namespace SocialNetwork.Data.Entities
{
    public class UserFriend
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string FriendId { get; set; }

        public User Friend { get; set; }
    }
}