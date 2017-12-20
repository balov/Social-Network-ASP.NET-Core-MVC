using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services;
using SocialNetwork.Web.Areas.Admin.Models.Users;
using SocialNetwork.Web.Infrastructure;
using SocialNetwork.Web.Infrastructure.Filters;

namespace SocialNetwork.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private const int PageSize = 3;

        private readonly IUserService userService;

        public static object Index()
        {
            throw new NotImplementedException();
        }

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Search(string searchTerm, int? page)
        {
            ViewData[GlobalConstants.SearchTerm] = searchTerm;

            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = this.userService.All(page ?? 1, PageSize);
                return View(users);
            }
            else
            {
                var users = this.userService.UsersBySearchTerm(searchTerm, page ?? 1, PageSize);
                return View(users);
            }
        }

        public IActionResult Edit(string id)
        {
            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            var userEditModel = Mapper.Map<UserEditModel>(this.userService.GetById(id));

            return View(userEditModel);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Edit(string id, UserEditModel model)
        {
            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            this.userService.EditUser(id, model.FirstName, model.LastName, model.Age, model.Email, model.Username);

            return RedirectToAction(nameof(Search), new { searchTerm = model.FirstName });
        }

        public IActionResult Delete(string id)
        {
            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            var userEditModel = Mapper.Map<UserEditModel>(this.userService.GetById(id));

            return View(userEditModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Destroy(string id)
        {
            if (!this.userService.UserExists(id))
            {
                return NotFound();
            }

            this.userService.DeleteUser(id);

            return RedirectToAction(nameof(Search));
        }
    }
}