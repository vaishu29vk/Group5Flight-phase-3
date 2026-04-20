using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Group5Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Please enter a flight code.")]
        [RegularExpression(@"^[A-Za-z]{2}\d{1,4}$",
            ErrorMessage = "Flight code must start with 2 letters followed by 1-4 digits (e.g. AA101).")]
        [Remote(action: "CheckFlightCode", controller: "Validation", areaName: "",
            AdditionalFields = nameof(Date) + "," + nameof(FlightId),
            ErrorMessage = "This flight code already exists on that date.")]
        [Display(Name = "Flight Code")]
        public string FlightCode { get; set; } = string.Empty;

        [Display(Name = "Airline")]
        public int AirlineId { get; set; }

        [Required(ErrorMessage = "Please enter a departure city.")]
        [StringLength(50, ErrorMessage = "Departure city must be 50 characters or less.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Departure city must contain letters only.")]
        [Display(Name = "From")]
        public string From { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a destination city.")]
        [StringLength(50, ErrorMessage = "Destination city must be 50 characters or less.")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Destination city must contain letters only.")]
        [Display(Name = "To")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a flight date.")]
        [FutureDate(3, ErrorMessage = "Flight date must be after today and within 3 years.")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter a departure time.")]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Please enter an arrival time.")]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Cabin Type")]
        public string CabinType { get; set; } = string.Empty;

        [Display(Name = "Aircraft Type")]
        public string AircraftType { get; set; } = string.Empty;

        [Range(0.1, 5000, ErrorMessage = "Emission must be between 0.1 and 5,000 kg.")]
        [Display(Name = "Emission")]
        public double Emission { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.01, 50000, ErrorMessage = "Price must be between $0.01 and $50,000.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public string AirlineName  { get; set; } = string.Empty;
        public string AirlineImage { get; set; } = string.Empty;

        public Airline? Airline { get; set; }
    }
}