using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Entities.Enums;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly SocialNetworkDbContext db;
        private readonly IPhotoService photoService;
        private readonly ICommentService commentService;

        public PostService(SocialNetworkDbContext db, IPhotoService photoService, ICommentService commentService)
        {
            this.db = db;
            this.photoService = photoService;
            this.commentService = commentService;
        }

        public void Create(string userId, Feeling feeling, string text, IFormFile photo)
        {
            var post = new Post
            {
                UserId = userId,
                Feeling = feeling,
                Text = text,
                Likes = 0,
                Date = DateTime.UtcNow,
                Photo = photo != null ? this.photoService.PhotoAsBytes(photo) : null
            };

            db.Posts.Add(post);
            db.SaveChanges();
        }

        public void Delete(int postId)
        {
            var post = this.db.Posts.Find(postId);
            this.commentService.DeleteCommentsByPostId(postId);
            this.db.Remove(post);
            this.db.SaveChanges();
        }

        public void Edit(int postId, Feeling feeling, string text, IFormFile photo)
        {
            var post = this.db.Posts.Find(postId);
            post.Feeling = feeling;
            post.Text = text;
            post.Photo = photo != null ? this.photoService.PhotoAsBytes(photo) : null;
            this.db.SaveChanges();
        }

        public bool Exists(int id) => this.db.Posts.Any(p => p.Id == id);

        public PaginatedList<PostModel> FriendPostsByUserId(string userId, int pageIndex, int pageSize)
        {
            var friendListIds = this.FriendsIds(userId);

            var posts = this.db
                .Posts
                .Where(p => friendListIds.Contains(p.UserId) || p.UserId == userId)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .ProjectTo<PostModel>()
                .ToList()
                .OrderByDescending(p => p.Date);

            return posts != null ? PaginatedList<PostModel>.Create(posts, pageIndex, pageSize) : null;
        }

        public void Like(int postId)
        {
            if (this.Exists(postId))
            {
                var post = this.db.Posts.Find(postId);
                post.Likes++;
                this.db.SaveChanges();
            }
        }

        public PostModel PostById(int postId)
        {
            return this.db.Posts.Where(p => p.Id == postId).ProjectTo<PostModel>().FirstOrDefault();
        }

        public PaginatedList<PostModel> PostsByUserId(string userId, int pageIndex, int pageSize)
        {
            var posts = this.db
                .Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .ProjectTo<PostModel>()
                .ToList()
                .OrderByDescending(p => p.Date);

            return posts != null ? PaginatedList<PostModel>.Create(posts, pageIndex, pageSize) : null;
        }

        public bool UserIsAuthorizedToEdit(int postId, string userId) => this.db.Posts.Any(p => p.Id == postId && p.UserId == userId);

        private List<string> FriendsIds(string userId)
        {
            var friends = this.db
                .UserFriend
                .Where(u => u.UserId == userId)
                .Select(u => u.Friend.Id)
                .ToList();

            var otherFriends = this.db
                .UserFriend
                .Where(u => u.FriendId == userId)
                .Select(u => u.User.Id)
                .ToList();

            friends.AddRange(otherFriends);

            return friends;
        }
    }
}