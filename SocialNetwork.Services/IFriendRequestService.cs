namespace SocialNetwork.Services
{
    public interface IFriendRequestService : IService
    {
        void Create(string senderId, string receiverId);

        void Accept(string senderId, string receiverId);

        void Delete(string senderId, string receiverId);

        void Decline(string senderId, string receiverId);

        bool Exists(string senderId, string receiverId);
    }
}