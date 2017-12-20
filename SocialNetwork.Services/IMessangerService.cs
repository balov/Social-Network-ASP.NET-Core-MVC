using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;

namespace SocialNetwork.Services
{
    public interface IMessangerService : IService
    {
        void Create(string senderId, string receiverId, string text);

        PaginatedList<MessageModel> AllByUserIds(string userId, string otherUserId, int pageIndex, int pageSize);
    }
}