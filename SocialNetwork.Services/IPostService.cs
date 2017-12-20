using Microsoft.AspNetCore.Http;
using SocialNetwork.Data.Entities.Enums;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;

namespace SocialNetwork.Services
{
    public interface IPostService : IService
    {
        void Create(string userId, Feeling feeling, string text, IFormFile photo);

        void Edit(int postId, Feeling feeling, string text, IFormFile photo);

        bool Exists(int id);

        bool UserIsAuthorizedToEdit(int postId, string userId);

        PaginatedList<PostModel> PostsByUserId(string userId, int pageIndex, int pageSize);

        PaginatedList<PostModel> FriendPostsByUserId(string userId, int pageIndex, int pageSize);

        PostModel PostById(int postId);

        void Delete(int postId);

        void Like(int postId);
    }
}