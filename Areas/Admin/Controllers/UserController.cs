using Microsoft.AspNetCore.Mvc;

namespace Group5Flight.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manage()
        {
            var manageText = "Admin Manage Users Content";
            return Content(manageText);
        }

        public IActionResult Rights()
        {
            var rightsText = "Admin Rights & Obligations Content";
            return Content(rightsText);
        }
    }
}
