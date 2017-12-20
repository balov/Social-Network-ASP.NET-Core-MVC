using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Infrastructure;
using SocialNetwork.Web.Infrastructure.Filters;
using SocialNetwork.Web.Models.Comment;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public CommentController(IPostService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }

        public IActionResult Create(int postId)
        {
            var postCommentViewModel = this.postService.PostById(postId);

            PostCommentCreateModel postCommentCreateModel = Mapper.Map<PostCommentCreateModel>(postCommentViewModel);

            return this.ViewOrNotFound(postCommentCreateModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(PostCommentCreateModel model, string returnUrl = null)
        {
            if (CoreValidator.CheckIfStringIsNullOrEmpty(model.CommentText))
            {
                ModelState.AddModelError(string.Empty, "You cannot submit an empty comment!");
                return View(model);
            }

            this.commentService.Create(model.CommentText, User.GetUserId(), model.Id);
            return RedirectToAction("Index", "Users");
        }
    }
}