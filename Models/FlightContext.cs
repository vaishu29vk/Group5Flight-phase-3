using Microsoft.EntityFrameworkCore;

namespace Group5Flight.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options) { }

        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<Airline> Airlines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Airlines
            modelBuilder.Entity<Airline>().HasData(
                new Airline { AirlineId = 1, Name = "American Airlines", ImageName = "american.png" },
                new Airline { AirlineId = 2, Name = "Delta",             ImageName = "delta.png"    },
                new Airline { AirlineId = 3, Name = "United",            ImageName = "united.png"   },
                new Airline { AirlineId = 4, Name = "Southwest",         ImageName = "southwest.png"}
            );

            // Seed Flights
            modelBuilder.Entity<Flight>().HasData(
                new { FlightId = 1,  FlightCode = "AA101", AirlineId = 1, From = "Chicago",     To = "New York",     Date = DateTime.Today.AddDays(2), DepartureTime = DateTime.Today.AddDays(2).AddHours(8),    ArrivalTime = DateTime.Today.AddDays(2).AddHours(10).AddMinutes(15), CabinType = "Economy",       AircraftType = "Boeing 737-800",  Emission = 120.5, Price = 189.00m, AirlineName = "American Airlines", AirlineImage = "american.png" },
                new { FlightId = 2,  FlightCode = "AA215", AirlineId = 1, From = "Chicago",     To = "Los Angeles",  Date = DateTime.Today.AddDays(3), DepartureTime = DateTime.Today.AddDays(3).AddHours(10),   ArrivalTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(30), CabinType = "Business",      AircraftType = "Boeing 737 MAX 8",Emission = 210.0, Price = 499.00m, AirlineName = "American Airlines", AirlineImage = "american.png" },
                new { FlightId = 3,  FlightCode = "DL330", AirlineId = 2, From = "New York",    To = "Chicago",      Date = DateTime.Today.AddDays(2), DepartureTime = DateTime.Today.AddDays(2).AddHours(14),   ArrivalTime = DateTime.Today.AddDays(2).AddHours(16).AddMinutes(10), CabinType = "Economy Plus",  AircraftType = "Airbus A320",     Emission = 98.3,  Price = 245.00m, AirlineName = "Delta",             AirlineImage = "delta.png"    },
                new { FlightId = 4,  FlightCode = "DL441", AirlineId = 2, From = "Los Angeles", To = "Chicago",      Date = DateTime.Today.AddDays(4), DepartureTime = DateTime.Today.AddDays(4).AddHours(7),    ArrivalTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(45), CabinType = "Basic Economy", AircraftType = "Airbus A321",     Emission = 310.0, Price = 139.00m, AirlineName = "Delta",             AirlineImage = "delta.png"    },
                new { FlightId = 5,  FlightCode = "UA552", AirlineId = 3, From = "Chicago",     To = "Miami",        Date = DateTime.Today.AddDays(5), DepartureTime = DateTime.Today.AddDays(5).AddHours(9),    ArrivalTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(30), CabinType = "Economy",       AircraftType = "Boeing 737-700",  Emission = 175.2, Price = 215.00m, AirlineName = "United",            AirlineImage = "united.png"   },
                new { FlightId = 6,  FlightCode = "UA663", AirlineId = 3, From = "Miami",       To = "New York",     Date = DateTime.Today.AddDays(3), DepartureTime = DateTime.Today.AddDays(3).AddHours(13),   ArrivalTime = DateTime.Today.AddDays(3).AddHours(15).AddMinutes(55), CabinType = "Business",      AircraftType = "Boeing 737 MAX 9",Emission = 88.0,  Price = 389.00m, AirlineName = "United",            AirlineImage = "united.png"   },
                new { FlightId = 7,  FlightCode = "WN774", AirlineId = 4, From = "Dallas",      To = "Chicago",      Date = DateTime.Today.AddDays(2), DepartureTime = DateTime.Today.AddDays(2).AddHours(6).AddMinutes(30), ArrivalTime = DateTime.Today.AddDays(2).AddHours(9).AddMinutes(15), CabinType = "Economy",       AircraftType = "Boeing 737-800",  Emission = 142.7, Price = 159.00m, AirlineName = "Southwest",         AirlineImage = "southwest.png"},
                new { FlightId = 8,  FlightCode = "WN885", AirlineId = 4, From = "Chicago",     To = "Dallas",       Date = DateTime.Today.AddDays(6), DepartureTime = DateTime.Today.AddDays(6).AddHours(16),   ArrivalTime = DateTime.Today.AddDays(6).AddHours(19).AddMinutes(5),  CabinType = "Economy Plus",  AircraftType = "Boeing 737 MAX 8",Emission = 148.0, Price = 178.00m, AirlineName = "Southwest",         AirlineImage = "southwest.png"},
                new { FlightId = 9,  FlightCode = "AA323", AirlineId = 1, From = "New York",    To = "Miami",        Date = DateTime.Today.AddDays(4), DepartureTime = DateTime.Today.AddDays(4).AddHours(11),   ArrivalTime = DateTime.Today.AddDays(4).AddHours(13).AddMinutes(40), CabinType = "Basic Economy", AircraftType = "Airbus A319",     Emission = 77.5,  Price = 119.00m, AirlineName = "American Airlines", AirlineImage = "american.png" },
                new { FlightId = 10, FlightCode = "DL997", AirlineId = 2, From = "Miami",       To = "Los Angeles",  Date = DateTime.Today.AddDays(7), DepartureTime = DateTime.Today.AddDays(7).AddHours(8).AddMinutes(30), ArrivalTime = DateTime.Today.AddDays(7).AddHours(11).AddMinutes(15), CabinType = "Business",      AircraftType = "Airbus A321neo",  Emission = 265.0, Price = 549.00m, AirlineName = "Delta",             AirlineImage = "delta.png"    }
            );
        }
    }
}