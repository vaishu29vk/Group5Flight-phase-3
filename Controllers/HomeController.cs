using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group5Flight.Models;

namespace Group5Flight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FlightContext context;

        public HomeController(ILogger<HomeController> logger, FlightContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index()
        {
            var searchSession = new FlightSearchSession(HttpContext.Session);

            var sessionPicks = HttpContext.Session.GetObject<List<string>>("g5_session_picks");
            if (sessionPicks == null)
            {
                var getCookies = new FlightPickCookie(Request.Cookies);
                string[] ids = getCookies.GetMyPickIds();
                sessionPicks = ids.Length > 0 ? new List<string>(ids) : new List<string>();
                HttpContext.Session.SetObject("g5_session_picks", sessionPicks);
            }

            var allFlights = context.Flights.Include(f => f.Airline).ToList();

            var model = new FlightViewModel
            {
                ActiveFrom     = searchSession.FetchFrom(),
                ActiveTo       = searchSession.FetchTo(),
                ActiveDate     = searchSession.FetchDate(),
                ActiveCabin    = searchSession.FetchCabin(),
                SelectionCount = sessionPicks.Count,
                ShowPicks      = false,
                FromCities     = allFlights.Select(f => f.From).Distinct().OrderBy(c => c).ToList(),
                ToCities       = allFlights.Select(f => f.To).Distinct().OrderBy(c => c).ToList(),
                Flights        = FilterFlights(allFlights,
                                    searchSession.FetchFrom(),
                                    searchSession.FetchTo(),
                                    searchSession.FetchDate(),
                                    searchSession.FetchCabin())
            };

            return View("SearchFlights", model);
        }

        [HttpGet]
        public IActionResult SearchFlights(bool showPicks = false)
        {
            var searchSession = new FlightSearchSession(HttpContext.Session);

            // Restore picks from cookie if session was cleared
            var sessionPicks = HttpContext.Session.GetObject<List<string>>("g5_session_picks");
            if (sessionPicks == null)
            {
                var getCookies = new FlightPickCookie(Request.Cookies);
                string[] ids = getCookies.GetMyPickIds();
                sessionPicks = ids.Length > 0 ? new List<string>(ids) : new List<string>();
                HttpContext.Session.SetObject("g5_session_picks", sessionPicks);
            }

            var allFlights = context.Flights.Include(f => f.Airline).ToList();

            var model = new FlightViewModel
            {
                ActiveFrom     = searchSession.FetchFrom(),
                ActiveTo       = searchSession.FetchTo(),
                ActiveDate     = searchSession.FetchDate(),
                ActiveCabin    = searchSession.FetchCabin(),
                SelectionCount = sessionPicks.Count,
                ShowPicks      = showPicks,
                FromCities     = allFlights.Select(f => f.From).Distinct().OrderBy(c => c).ToList(),
                ToCities       = allFlights.Select(f => f.To).Distinct().OrderBy(c => c).ToList(),
                Flights        = showPicks
                                    ? context.Flights.Include(f => f.Airline)
                                        .Where(f => sessionPicks.Contains(f.FlightId.ToString()))
                                        .ToList()
                                    : FilterFlights(allFlights,
                                        searchSession.FetchFrom(),
                                        searchSession.FetchTo(),
                                        searchSession.FetchDate(),
                                        searchSession.FetchCabin())
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SearchFlights(FlightViewModel model)
        {
            var searchSession = new FlightSearchSession(HttpContext.Session);
            searchSession.StoreFrom(model.ActiveFrom   ?? string.Empty);
            searchSession.StoreTo(model.ActiveTo       ?? string.Empty);
            searchSession.StoreDate(model.ActiveDate   ?? string.Empty);
            searchSession.StoreCabin(model.ActiveCabin ?? string.Empty);

            return RedirectToAction("SearchFlights");
        }

        [HttpPost]
        public IActionResult AddPick(int flightId)
        {
            var sessionPicks = HttpContext.Session.GetObject<List<string>>("g5_session_picks")
                               ?? new List<string>();

            string idStr = flightId.ToString();

            if (!sessionPicks.Contains(idStr))
            {
                sessionPicks.Add(idStr);
                HttpContext.Session.SetObject("g5_session_picks", sessionPicks);

                var setCookies = new FlightPickCookie(Response.Cookies);
                setCookies.SetMyPickIds(sessionPicks);

                var flight = context.Flights.Find(flightId);
                TempData["message"] = flight != null
                    ? $"Flight {flight.FlightCode} has been added to your picks."
                    : "Flight added to your picks.";
            }
            else
            {
                TempData["message"] = "That flight is already in your picks.";
            }

            return RedirectToAction("SearchFlights");
        }

        [HttpPost]
        public IActionResult RemovePick(int flightId)
        {
            var sessionPicks = HttpContext.Session.GetObject<List<string>>("g5_session_picks")
                               ?? new List<string>();

            sessionPicks.Remove(flightId.ToString());
            HttpContext.Session.SetObject("g5_session_picks", sessionPicks);

            var setCookies = new FlightPickCookie(Response.Cookies);
            setCookies.SetMyPickIds(sessionPicks);

            var flight = context.Flights.Find(flightId);
            TempData["message"] = flight != null
                ? $"Flight {flight.FlightCode} has been removed from your picks."
                : "Flight removed.";

            return RedirectToAction("SearchFlights", new { showPicks = true });
        }

        [HttpPost]
        public IActionResult ClearPicks()
        {
            HttpContext.Session.Remove("g5_session_picks");

            var setCookies = new FlightPickCookie(Response.Cookies);
            setCookies.RemoveMyPickIds();

            TempData["message"] = "All picks have been cleared.";

            return RedirectToAction("SearchFlights", new { showPicks = true });
        }

        [HttpGet]
        public IActionResult FlightDetail(int id)
        {
            var flight = context.Flights.Include(f => f.Airline).FirstOrDefault(f => f.FlightId == id);
            if (flight == null) return RedirectToAction("SearchFlights");
            return View(flight);
        }

        public IActionResult Privacy()
        {
            return Content("Client Privacy Policy Content");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        private List<Flight> FilterFlights(List<Flight> source,
            string from, string to, string date, string cabin)
        {
            var results = source.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(from))
                results = results.Where(f => f.From == from);

            if (!string.IsNullOrWhiteSpace(to))
                results = results.Where(f => f.To == to);

            if (!string.IsNullOrWhiteSpace(date) &&
                DateTime.TryParse(date, out DateTime parsedDate))
                results = results.Where(f => f.Date.Date == parsedDate.Date);

            if (!string.IsNullOrWhiteSpace(cabin) && cabin != "All")
                results = results.Where(f => f.CabinType == cabin);

            return results.OrderBy(f => f.Date).ThenBy(f => f.DepartureTime).ToList();
        }
    }
}
