using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services;
using SocialNetwork.Services.Models;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Infrastructure;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private const int PageSize = 3;

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index(int? page)
        {
            UserAccountModel user = this.userService.UserDetailsFriendsCommentsAndPosts(this.User.GetUserId(), page ?? 1, PageSize);

            return this.ViewOrNotFound(user);
        }

        public IActionResult AccountDetails(string id, int? page)
        {
            //could be optimized
            if (User.GetUserId() == id)
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.FullAuthorization;
            }
            else if (this.userService.CheckIfFriends(User.GetUserId(), id))
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.FriendAuthorization;
            }
            else
            {
                ViewData[GlobalConstants.Authorization] = GlobalConstants.NoAuthorization;
            }

            UserAccountModel user = this.userService.UserDetails(id, page ?? 1, PageSize);

            return this.ViewOrNotFound(user);
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = this.userService.All(page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
            else
            {
                var users = this.userService.UsersBySearchTerm(searchTerm, page ?? 1, PageSize);
                return this.ViewOrNotFound(users);
            }
        }
    }
}