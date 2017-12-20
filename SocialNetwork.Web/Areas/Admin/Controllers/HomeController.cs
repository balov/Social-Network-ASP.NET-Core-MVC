using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}