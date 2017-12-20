using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services;
using SocialNetwork.Web.Extensions;
using SocialNetwork.Web.Infrastructure.Filters;
using SocialNetwork.Web.Models.Events;

namespace SocialNetwork.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService eventService;

        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create(EventFormModel model)
        {
            this.eventService.Create(
                model.ImageUrl,
                model.Title,
                model.Location,
                model.Description,
                model.DateStarts,
                model.DateEnds,
                this.User.GetUserId());

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Details(int id)
        {
            if (!this.eventService.Exists(id))
            {
                return NotFound();
            }

            return View(this.eventService.Details(id));
        }

        public IActionResult JointEvent(int id)
        {
            if (!this.eventService.Exists(id))
            {
                return NotFound();
            }

            this.eventService.AddUserToEvent(User.GetUserId(), id);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}