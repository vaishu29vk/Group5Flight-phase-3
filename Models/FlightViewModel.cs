namespace Group5Flight.Models
{
    public class FlightViewModel
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public List<Airline> Airlines { get; set; } = new List<Airline>();
        public Flight? EditingFlight { get; set; }
        public string ActiveFrom   { get; set; } = string.Empty;
        public string ActiveTo     { get; set; } = string.Empty;
        public string ActiveDate   { get; set; } = string.Empty;
        public string ActiveCabin  { get; set; } = string.Empty;
        public List<string> FromCities { get; set; } = new List<string>();
        public List<string> ToCities   { get; set; } = new List<string>();
        public int SelectionCount  { get; set; }
        public bool ShowPicks      { get; set; } = false;

        public static readonly List<string> CabinTypes = new List<string>
        {
            "Basic Economy", "Economy", "Economy Plus", "Business"
        };

        public static readonly List<string> AircraftTypes = new List<string>
        {
            "Airbus A319", "Airbus A320", "Airbus A321", "Airbus A321neo",
            "Boeing 737-700", "Boeing 737-800", "Boeing 737 MAX 8", "Boeing 737 MAX 9"
        };
    }
}