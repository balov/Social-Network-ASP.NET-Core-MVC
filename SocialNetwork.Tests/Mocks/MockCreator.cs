using Microsoft.EntityFrameworkCore;
using Moq;
using SocialNetwork.Data;
using SocialNetwork.Services.Implementations;
using System;

namespace SocialNetwork.Tests.Mocks
{
    public class MockCreator
    {
        public static SocialNetworkDbContext GetDb()
        {
            var dbOptions = new DbContextOptionsBuilder<SocialNetworkDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SocialNetworkDbContext(dbOptions);
        }

        public static Mock<UserService> UserServiceMock()
        {
            return new Mock<UserService>(GetDb(), null, null, null);
        }

        public static Mock<PostService> PostServiceMock()
        {
            return new Mock<PostService>(GetDb(), null, null);
        }

        public static Mock<CommentService> CommentServiceMock()
        {
            return new Mock<CommentService>(GetDb());
        }
    }
}