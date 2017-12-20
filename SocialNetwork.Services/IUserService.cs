using Microsoft.AspNetCore.Http;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using System.Collections.Generic;

namespace SocialNetwork.Services
{
    public interface IUserService : IService
    {
        UserModel GetById(string id);

        void AddProfilePicture(IFormFile photo, string userId);

        bool UserExists(string userId);

        UserAccountModel UserDetails(string userId, int pageIndex, int pageSize);

        UserAccountModel UserDetailsFriendsCommentsAndPosts(string userId, int pageIndex, int pageSize);

        bool CheckIfFriends(string requestUserId, string targetUserId);

        bool CheckIfDeleted(string userId);

        bool CheckIfDeletedByUserName(string username);

        void MakeFriends(string senderId, string receiverId);

        PaginatedList<UserListModel> UsersBySearchTerm(string searchTerm, int pageIndex, int pageSize);

        PaginatedList<UserListModel> All(int pageIndex, int pageSize);

        object GetUserFullName(string id);

        void EditUser(string id, string firstName, string lastName, int age, string email, string username);

        void DeleteUser(string id);

        List<string> FriendsIds(string userId);
    }
}