using Microsoft.AspNetCore.Mvc;
using Group5Flight.Models;

namespace Group5Flight.Controllers
{
    public class ValidationController : Controller
    {
        private FlightContext context;

        public ValidationController(FlightContext ctx)
        {
            context = ctx;
        }

        // ASP.NET Core model binding maps EditingFlight.* query params to editingFlight.*
        public JsonResult CheckFlightCode(Flight editingFlight)
        {
            if (string.IsNullOrEmpty(editingFlight.FlightCode))
                return Json(true);

            var exists = context.Flights.Any(f =>
                f.FlightCode.ToLower() == editingFlight.FlightCode.ToLower() &&
                f.Date.Date == editingFlight.Date.Date &&
                (editingFlight.FlightId == 0 || f.FlightId != editingFlight.FlightId));

            if (exists)
            {
                TempData["okFlightCode"] = false;
                return Json($"Flight code '{editingFlight.FlightCode}' on {editingFlight.Date:MM/dd/yyyy} already exists.");
            }

            TempData["okFlightCode"] = true;
            return Json(true);
        }
    }
}
