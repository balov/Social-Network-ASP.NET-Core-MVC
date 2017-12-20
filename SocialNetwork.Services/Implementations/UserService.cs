using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Infrastructure;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SocialNetworkDbContext db;
        private readonly IPhotoService photoService;
        private readonly IPostService postService;
        private readonly IEventService eventService;

        public UserService(SocialNetworkDbContext db, IPhotoService photoService, IPostService postService, IEventService eventService)
        {
            this.db = db;
            this.photoService = photoService;
            this.postService = postService;
            this.eventService = eventService;
        }

        public void AddProfilePicture(IFormFile photo, string userId)
        {
            if (this.UserExists(userId))
            {
                var user = this.db.Users.Find(userId);
                user.ProfilePicture = this.photoService.PhotoAsBytes(photo);
                this.db.SaveChanges();
            }
        }

        public bool CheckIfFriends(string requestUserId, string targetUserId)
        {
            return this.db.UserFriend.Any(uf =>
            (uf.UserId == requestUserId && uf.FriendId == targetUserId) || (uf.UserId == targetUserId && uf.FriendId == requestUserId));
        }

        public void MakeFriends(string senderId, string receiverId)
        {
            if (this.UserExists(senderId) && this.UserExists(receiverId) && !this.CheckIfFriends(senderId, receiverId))
            {
                var userFriend = new UserFriend
                {
                    UserId = senderId,
                    FriendId = receiverId
                };
                this.db.UserFriend.Add(userFriend);
                this.db.SaveChanges();
            }
        }

        public UserAccountModel UserDetails(string userId, int pageIndex, int pageSize)
        {
            if (this.UserExists(userId))
            {
                var userPosts = this.postService.PostsByUserId(userId, pageIndex, pageSize);
                var userAccountModel = db.Users.Where(u => u.Id == userId).ProjectTo<UserAccountModel>().FirstOrDefault();
                var friends = this.db
                    .UserFriend
                    .Where(u => u.UserId == userId && !u.Friend.IsDeleted)
                    .Select(u => u.Friend)
                    .ProjectTo<UserListModel>()
                    .ToList();

                var otherFriends = this.db
                    .UserFriend
                    .Where(u => u.FriendId == userId && !u.User.IsDeleted)
                    .Select(u => u.User)
                    .ProjectTo<UserListModel>()
                    .ToList();

                friends.AddRange(otherFriends);

                userAccountModel.Posts = userPosts;
                userAccountModel.Friends = friends;

                return userAccountModel;
            }
            else
            {
                return null;
            }
        }

        public virtual UserAccountModel UserDetailsFriendsCommentsAndPosts(string userId, int pageIndex, int pageSize)
        {
            if (this.UserExists(userId))
            {
                var userAccountModel = db.Users.Where(u => u.Id == userId).ProjectTo<UserAccountModel>().FirstOrDefault();
                userAccountModel.Posts = this.postService.FriendPostsByUserId(userId, pageIndex, pageSize);
                userAccountModel.Events = this.eventService.UpcomingThreeEvents();

                return userAccountModel;
            }
            else
            {
                return null;
            }
        }

        public bool UserExists(string userId) => this.db.Users.Any(u => u.Id == userId && u.IsDeleted == false);

        public PaginatedList<UserListModel> UsersBySearchTerm(string searchTerm, int pageIndex, int pageSize)
        {
            var users = this.db.Users
                .Where(u => (u.FirstName.ToLower().Contains(searchTerm.ToLower())
                || u.LastName.ToLower().Contains(searchTerm.ToLower())
                || u.UserName.ToLower().Contains(searchTerm.ToLower()))
                && u.UserName != ServiceConstants.AdminUserName
                && u.IsDeleted == false)
                .ProjectTo<UserListModel>()
                .ToList();

            return users != null ? PaginatedList<UserListModel>.Create(users, pageIndex, pageSize) : null;
        }

        public PaginatedList<UserListModel> All(int pageIndex, int pageSize)
        {
            var users = this.db.Users
                .Where(u => u.UserName != ServiceConstants.AdminUserName && u.IsDeleted == false)
                .ProjectTo<UserListModel>()
                .ToList();

            return users != null ? PaginatedList<UserListModel>.Create(users, pageIndex, pageSize) : null;
        }

        public object GetUserFullName(string id)
        {
            if (this.UserExists(id))
            {
                var user = this.db.Users.Find(id);
                return user.FirstName + " " + user.LastName;
            }
            return null;
        }

        public bool CheckIfDeleted(string userId)
        {
            throw new System.NotImplementedException();
        }

        public UserModel GetById(string id)
        {
            if (this.UserExists(id))
            {
                return Mapper.Map<UserModel>(this.db.Users.Find(id));
            }

            return null;
        }

        public void EditUser(string id, string firstName, string lastName, int age, string email, string username)
        {
            var user = this.db.Users.Find(id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = username;
            user.Age = age;
            user.Email = email;

            this.db.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var user = this.db.Users.Find(id);

            user.IsDeleted = true;

            this.db.SaveChanges();
        }

        public bool CheckIfDeletedByUserName(string username)
        {
            if (this.db.Users.Any(u => u.UserName == username))
            {
                return this.db.Users.FirstOrDefault(u => u.UserName == username).IsDeleted;
            }

            return true;
        }

        public List<string> FriendsIds(string userId)
        {
            if (this.UserExists(userId))
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
            else
            {
                return null;
            }
        }
    }
}