using AutoMapper.QueryableExtensions;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class MessangerService : IMessangerService
    {
        private readonly SocialNetworkDbContext db;

        public MessangerService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public List<MessageModel> All()
        {
            return this.db
                .Messages
                .ProjectTo<MessageModel>()
                .ToList();
        }

        public PaginatedList<MessageModel> AllByUserIds(string userId, string otherUserId, int pageIndex, int pageSize)
        {
            var messages = this.db
                .Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == otherUserId) || (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderBy(m => m.DateSent)
                .ProjectTo<MessageModel>()
                .ToList();

            return messages != null ? PaginatedList<MessageModel>.Create(messages, pageIndex, pageSize) : null;
        }

        public void Create(string senderId, string receiverId, string text)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                DateSent = DateTime.UtcNow,
                IsSeen = false,
                MessageText = text
            };

            this.db.Add(message);
            this.db.SaveChanges();
        }
    }
}