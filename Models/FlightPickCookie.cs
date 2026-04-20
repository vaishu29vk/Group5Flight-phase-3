namespace Group5Flight.Models
{
    // Cookie wrapper class — Chapter 9 pattern (slides 40-41).
    // Two constructors: one for reading (request), one for writing (response).
    // Stores selected flight IDs as a delimiter-joined persistent cookie.
    // Different from Group 2's FlightCookies:
    //   - Class name: FlightPickCookie
    //   - Cookie key: "g5_picks"
    //   - Delimiter : "."
    //   - Methods   : GetMyPickIds / SetMyPickIds / RemoveMyPickIds
    public class FlightPickCookie
    {
        private const string PicksKey  = "g5_picks";
        private const string Delimiter = ".";

        private IRequestCookieCollection requestCookies  { get; set; } = null!;
        private IResponseCookies         responseCookies { get; set; } = null!;

        // Constructor for reading in GET actions
        public FlightPickCookie(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        // Constructor for writing in POST actions
        public FlightPickCookie(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        // Returns array of flight ID strings from the cookie
        public string[] GetMyPickIds()
        {
            string cookie = requestCookies[PicksKey] ?? string.Empty;
            if (string.IsNullOrEmpty(cookie))
                return Array.Empty<string>();
            else
                return cookie.Split(Delimiter);
        }

        // Saves flight IDs as delimiter-joined persistent cookie (14-day expiry)
        public void SetMyPickIds(List<string> ids)
        {
            string idsString = string.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(14)
            };
            RemoveMyPickIds();
            responseCookies.Append(PicksKey, idsString, options);
        }

        // Deletes the picks cookie
        public void RemoveMyPickIds()
        {
            responseCookies.Delete(PicksKey);
        }
    }
}
