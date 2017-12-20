using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services;
using SocialNetwork.Web.Extensions;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class FriendRequestsController : Controller
    {
        private readonly IFriendRequestService friendRequestService;
        private readonly IUserService userService;

        public FriendRequestsController(IFriendRequestService friendRequestService, IUserService userService)
        {
            this.friendRequestService = friendRequestService;
            this.userService = userService;
        }

        public IActionResult AddFriend(string senderId, string receiverId)
        {
            if (!this.userService.UserExists(senderId) || !this.userService.UserExists(receiverId) || senderId != this.User.GetUserId())
            {
                return NotFound();
            }

            this.friendRequestService.Create(senderId, receiverId);

            return RedirectToAction("AccountDetails", "Users", new { id = receiverId });
        }

        public IActionResult Accept(string senderId, string receiverId)
        {
            this.friendRequestService.Accept(senderId, receiverId);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }

        public IActionResult Decline(string senderId, string receiverId)
        {
            this.friendRequestService.Decline(senderId, receiverId);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }
    }
}