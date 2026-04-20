using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group5Flight.Models;

namespace Group5Flight.Areas.Airlines.Controllers
{
    [Area("Airlines")]
    public class FlightsController : Controller
    {
        private FlightContext context;

        public FlightsController(FlightContext ctx)
        {
            context = ctx;
        }

        public IActionResult Manage()
        {
            return Content("Manage Flights");
        }

        public IActionResult Regulation()
        {
            return Content("Airline Regulations");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new FlightViewModel
            {
                Flights       = context.Flights.Include(f => f.Airline).ToList(),
                Airlines      = context.Airlines.ToList(),
                EditingFlight = new Flight()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var flight = context.Flights.Find(id);
            if (flight == null)
                return RedirectToAction("Index");

            var model = new FlightViewModel
            {
                Flights       = context.Flights.Include(f => f.Airline).ToList(),
                Airlines      = context.Airlines.ToList(),
                EditingFlight = flight
            };
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Index(FlightViewModel vm)
        {
            var flight = vm.EditingFlight ?? new Flight();

            // Server-side duplicate check runs when client-side remote validation didn't set TempData
            bool remoteValidated = TempData["okFlightCode"] as bool? ?? false;
            if (!remoteValidated && !string.IsNullOrEmpty(flight.FlightCode))
            {
                bool duplicate = context.Flights.Any(f =>
                    f.FlightCode.ToLower() == flight.FlightCode.ToLower() &&
                    f.Date.Date == flight.Date.Date &&
                    (flight.FlightId == 0 || f.FlightId != flight.FlightId));

                if (duplicate)
                    ModelState.AddModelError("EditingFlight." + nameof(Flight.FlightCode),
                        $"Flight code '{flight.FlightCode}' on {flight.Date:MM/dd/yyyy} already exists.");
            }

            if (ModelState.IsValid)
            {
                var airline = context.Airlines.Find(flight.AirlineId);
                if (airline != null)
                {
                    flight.AirlineName  = airline.Name;
                    flight.AirlineImage = airline.ImageName;
                }

                if (flight.FlightId == 0)
                {
                    context.Flights.Add(flight);
                    TempData["message"] = $"Flight {flight.FlightCode} has been added.";
                }
                else
                {
                    // Property-by-property update to avoid detached entity issues
                    var existing = context.Flights.Find(flight.FlightId);
                    if (existing != null)
                    {
                        existing.FlightCode    = flight.FlightCode;
                        existing.AirlineId     = flight.AirlineId;
                        existing.AirlineName   = flight.AirlineName;
                        existing.AirlineImage  = flight.AirlineImage;
                        existing.From          = flight.From;
                        existing.To            = flight.To;
                        existing.Date          = flight.Date;
                        existing.DepartureTime = flight.DepartureTime;
                        existing.ArrivalTime   = flight.ArrivalTime;
                        existing.CabinType     = flight.CabinType;
                        existing.AircraftType  = flight.AircraftType;
                        existing.Emission      = flight.Emission;
                        existing.Price         = flight.Price;
                    }
                    TempData["message"] = $"Flight {flight.FlightCode} has been updated.";
                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.Flights  = context.Flights.Include(f => f.Airline).ToList();
            vm.Airlines = context.Airlines.ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] int id)
        {
            var flight = context.Flights.Find(id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
                TempData["message"] = $"Flight {flight.FlightCode} has been deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
