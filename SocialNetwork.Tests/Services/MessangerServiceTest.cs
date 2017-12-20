using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Implementations;
using SocialNetwork.Services.Infrastructure.CustomDataStructures;
using SocialNetwork.Services.Models;
using SocialNetwork.Tests.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace SocialNetwork.Tests.Services
{
    public class MessangerServiceTest
    {
        [Fact]
        public void CheckIfReturnsAllMessagesByUserId()
        {
            const string UserId = "1";
            const string OtherUserId = "2";

            //Arrange
            Initializer.IniializeAuttoMapper();

            var db = this.GetDb();
          
            var message1 = new Message { Id = 1, SenderId = UserId, ReceiverId = OtherUserId };
            var message2 = new Message { Id = 2, SenderId = OtherUserId, ReceiverId = UserId };
            var message3 = new Message { Id = 3, SenderId = "3", ReceiverId = "4" };
           
            db.AddRange(message1, message2, message3);
            db.SaveChanges();

            var messangerService = new MessangerService(db);

            //Act
            List<MessageModel> result = messangerService.All();
            //var result = messangerService.AllByUserIds(OtherUserId, UserId, 1, 6);

            //Assert
            result
                .Should()
                .HaveCount(0);

        }

        private SocialNetworkDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SocialNetworkDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SocialNetworkDbContext(dbOptions);
        }
    }
}