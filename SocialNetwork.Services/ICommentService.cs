using SocialNetwork.Services.Models;
using System.Collections.Generic;

namespace SocialNetwork.Services
{
    public interface ICommentService : IService
    {
        void Create(string commentText, string userId, int postId);

        void DeleteCommentsByPostId(int postId);

        IEnumerable<CommentModel> CommentsByPostId(int postId);
    }
}