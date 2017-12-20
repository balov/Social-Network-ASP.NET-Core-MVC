using Microsoft.AspNetCore.Mvc;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using SocialNetwork.Services;
using SocialNetwork.Web.Infrastructure;
using SocialNetwork.Web.Infrastructure.Filters;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Models.HomeViewModels;
using System;
using System.Diagnostics;
using System.Text;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(GlobalConstants.AdminRole))
            {
                return RedirectToAction("Index", "Home", new { area = GlobalConstants.AdminArea });
            }
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateRecaptcha]
        public IActionResult Contact(EmailModel model)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine($"From: {model.Name} {model.Email}");
            message.AppendLine();
            message.AppendLine(model.Message);

            try
            {
                this.emailSender.SendEmailAsync(GlobalConstants.SocialNetworkEmail, model.Subject, message.ToString());
                ViewData["SuccessMessage"] = "Your email has been successfully sent!";
                return View(model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("EmailSenderror", "Something went wrong, please try again later!");
                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}