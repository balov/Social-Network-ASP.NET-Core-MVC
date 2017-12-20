using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SocialNetwork.Services.Models;
using SocialNetwork.Tests.Mocks;
using SocialNetwork.Web.Controllers;
using System.Linq;
using Xunit;

namespace SocialNetwork.Tests.Web.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void ControllerShouldBeOnlyForAuhtorizedUsers()
        {
            //Arrange
            var controller = typeof(UsersController);

            //Act
            var attributes = controller.GetCustomAttributes(true);

            //Assert
            attributes
           .Should()
           .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public void IndexShouldReturnViewWhenModelsOK()
        {
            //Arrange
            var userService = MockCreator.UserServiceMock();

            userService
                .Setup(u =>
                u.UserDetailsFriendsCommentsAndPosts(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new UserAccountModel());

            var controller = new UsersController(userService.Object);

            //Act
            var result = controller.Index(null);

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void IndexShouldReturnNotFoundwhenModelIsNull()
        {
            //Arrange
            var userService = MockCreator.UserServiceMock();

            userService
                .Setup(u =>
                u.UserDetailsFriendsCommentsAndPosts(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<UserAccountModel>(null);

            var controller = new UsersController(userService.Object);

            //Act
            var result = controller.Index(null);

            //Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }
    }
}