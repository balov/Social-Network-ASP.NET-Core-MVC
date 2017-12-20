namespace SocialNetwork.Data.Entities
{
    public class UserInterest
    {
        public int InterestId { get; set; }

        public Interest Interest { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}