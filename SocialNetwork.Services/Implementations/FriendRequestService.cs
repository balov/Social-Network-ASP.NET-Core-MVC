using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Entities.Enums;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly SocialNetworkDbContext db;
        private readonly IUserService userService;

        public FriendRequestService(SocialNetworkDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }

        public void Accept(string senderId, string receiverId)
        {
            if (this.Exists(senderId, receiverId) && this.userService.UserExists(senderId) && this.userService.UserExists(receiverId))
            {
                var friendRequest = db.FriendRequests.FirstOrDefault(fr => fr.ReceiverId == receiverId && fr.SenderId == senderId);
                friendRequest.FriendRequestStatus = FriendRequestStatus.Accepted;
                this.userService.MakeFriends(senderId, receiverId);
                this.db.SaveChanges();
            }
        }

        public void Create(string senderId, string receiverId)
        {
            if (!this.Exists(senderId, receiverId) && this.userService.UserExists(senderId) && this.userService.UserExists(receiverId))
            {
                var friendRequest = new FriendRequest
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    FriendRequestStatus = FriendRequestStatus.Pending
                };

                this.db.FriendRequests.Add(friendRequest);
                this.db.SaveChanges();
            }
        }

        public void Decline(string senderId, string receiverId)
        {
            if (this.Exists(senderId, receiverId) && this.userService.UserExists(senderId) && this.userService.UserExists(receiverId))
            {
                var friendRequest = db.FriendRequests.FirstOrDefault(fr => fr.ReceiverId == receiverId && fr.SenderId == senderId);
                this.db.Remove(friendRequest);
                this.db.SaveChanges();
            }
        }

        public void Delete(string senderId, string receiverId)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string senderId, string receiverId) =>
             this.db.FriendRequests.Any(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
    }
}