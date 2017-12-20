using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Tests.Mocks;
using SocialNetwork.Web.Controllers;
using SocialNetwork.Web.Models.Comment;
using System.Linq;
using Xunit;

namespace SocialNetwork.Tests.Web.Controllers
{
    public class CommentControllerTest
    {
        [Fact]
        public void ControllerShouldBeOnlyForAuhtorizedUsers()
        {
            //Arrange
            var controller = typeof(CommentController);

            //Act
            var attributes = controller.GetCustomAttributes(true);

            //Assert
            attributes
           .Should()
           .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public void CheckIfCreateMethodReturnsViewWithErrorWhenModelTextIsEmpty()
        {
            //Arrange
            var postService = MockCreator.PostServiceMock();
            var commentService = MockCreator.CommentServiceMock();

            var controller = new CommentController(postService.Object, commentService.Object);

            var model = new PostCommentCreateModel
            {
                CommentText = ""
            };

            //Act
            var result = controller.Create(model, null);

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void CheckIfCreateMethodReturnsRedirectWhenOk()
        {
            //Arrange
            var postService = MockCreator.PostServiceMock();
            var commentService = MockCreator.CommentServiceMock();

            var controller = new CommentController(postService.Object, commentService.Object);

            var model = new PostCommentCreateModel
            {
                CommentText = "Test"
            };

            //Act
            var result = controller.Create(model, null);

            //Assert
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
        }
    }
}