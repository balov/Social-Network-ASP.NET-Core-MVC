using AutoMapper.QueryableExtensions;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly SocialNetworkDbContext db;

        public CommentService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CommentModel> CommentsByPostId(int postId)
        {
            //TO DO - implement some paging in case there are many comments
            return this.db.Comments.Where(c => c.PostId == postId).OrderByDescending(c => c.Date).ProjectTo<CommentModel>().ToList();
        }

        public void Create(string commentText, string userId, int postId)
        {
            var comment = new Comment
            {
                Date = DateTime.UtcNow,
                Text = commentText,
                UserId = userId,
                PostId = postId
            };

            this.db.Comments.Add(comment);
            this.db.SaveChanges();
        }

        public void DeleteCommentsByPostId(int postId)
        {
            var comments = this.db.Comments.Where(c => c.PostId == postId);

            foreach (var comment in comments)
            {
                this.db.Remove(comment);
            }

            this.db.SaveChanges();
        }
    }
}