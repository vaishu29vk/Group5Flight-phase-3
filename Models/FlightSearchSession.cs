namespace Group5Flight.Models
{
    // Session wrapper class — Chapter 9 pattern (slides 19, 24-25).
    // Encapsulates all session keys and get/set logic for filter criteria.
    // Different from Group 2's FlightSession:
    //   - Class name: FlightSearchSession
    //   - Keys use "search_" prefix
    //   - Methods named Store*/Fetch* instead of Set*/Get*
    //   - Date defaults to tomorrow when not yet stored
    public class FlightSearchSession
    {
        private const string FromKey  = "search_from";
        private const string ToKey    = "search_to";
        private const string DateKey  = "search_date";
        private const string CabinKey = "search_cabin";

        private ISession session { get; set; }

        public FlightSearchSession(ISession session) =>
            this.session = session;

        public void StoreFrom(string value)  => session.SetString(FromKey, value);
        public string FetchFrom()            => session.GetString(FromKey) ?? string.Empty;

        public void StoreTo(string value)    => session.SetString(ToKey, value);
        public string FetchTo()              => session.GetString(ToKey) ?? string.Empty;

        public void StoreDate(string value)  => session.SetString(DateKey, value);
        public string FetchDate()
        {
            var stored = session.GetString(DateKey);
            // Default to tomorrow in MM/DD/YYYY format — matches daterangepicker locale
            return string.IsNullOrEmpty(stored)
                ? DateTime.Today.AddDays(1).ToString("MM/dd/yyyy")
                : stored;
        }

        public void StoreCabin(string value) => session.SetString(CabinKey, value);
        public string FetchCabin()           => session.GetString(CabinKey) ?? string.Empty;
    }
}
