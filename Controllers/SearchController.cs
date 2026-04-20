using Microsoft.AspNetCore.Mvc;

namespace Group5Flight.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("SearchFlights", "Home");
        }
    }
}
