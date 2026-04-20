using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group5Flight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightCode = table.Column<string>(type: "TEXT", nullable: false),
                    AirlineId = table.Column<int>(type: "INTEGER", nullable: false),
                    From = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    To = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CabinType = table.Column<string>(type: "TEXT", nullable: false),
                    AircraftType = table.Column<string>(type: "TEXT", nullable: false),
                    Emission = table.Column<double>(type: "REAL", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    AirlineName = table.Column<string>(type: "TEXT", nullable: false),
                    AirlineImage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "AirlineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "AirlineId", "ImageName", "Name" },
                values: new object[,]
                {
                    { 1, "american.png", "American Airlines" },
                    { 2, "delta.png", "Delta" },
                    { 3, "united.png", "United" },
                    { 4, "southwest.png", "Southwest" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "AircraftType", "AirlineId", "AirlineImage", "AirlineName", "ArrivalTime", "CabinType", "Date", "DepartureTime", "Emission", "FlightCode", "From", "Price", "To" },
                values: new object[,]
                {
                    { 1, "Boeing 737-800", 1, "american.png", "American Airlines", new DateTime(2026, 4, 21, 10, 15, 0, 0, DateTimeKind.Local), "Economy", new DateTime(2026, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 21, 8, 0, 0, 0, DateTimeKind.Local), 120.5, "AA101", "Chicago", 189.00m, "New York" },
                    { 2, "Boeing 737 MAX 8", 1, "american.png", "American Airlines", new DateTime(2026, 4, 22, 12, 30, 0, 0, DateTimeKind.Local), "Business", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 22, 10, 0, 0, 0, DateTimeKind.Local), 210.0, "AA215", "Chicago", 499.00m, "Los Angeles" },
                    { 3, "Airbus A320", 2, "delta.png", "Delta", new DateTime(2026, 4, 21, 16, 10, 0, 0, DateTimeKind.Local), "Economy Plus", new DateTime(2026, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 21, 14, 0, 0, 0, DateTimeKind.Local), 98.299999999999997, "DL330", "New York", 245.00m, "Chicago" },
                    { 4, "Airbus A321", 2, "delta.png", "Delta", new DateTime(2026, 4, 23, 12, 45, 0, 0, DateTimeKind.Local), "Basic Economy", new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 23, 7, 0, 0, 0, DateTimeKind.Local), 310.0, "DL441", "Los Angeles", 139.00m, "Chicago" },
                    { 5, "Boeing 737-700", 3, "united.png", "United", new DateTime(2026, 4, 24, 12, 30, 0, 0, DateTimeKind.Local), "Economy", new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 24, 9, 0, 0, 0, DateTimeKind.Local), 175.19999999999999, "UA552", "Chicago", 215.00m, "Miami" },
                    { 6, "Boeing 737 MAX 9", 3, "united.png", "United", new DateTime(2026, 4, 22, 15, 55, 0, 0, DateTimeKind.Local), "Business", new DateTime(2026, 4, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 22, 13, 0, 0, 0, DateTimeKind.Local), 88.0, "UA663", "Miami", 389.00m, "New York" },
                    { 7, "Boeing 737-800", 4, "southwest.png", "Southwest", new DateTime(2026, 4, 21, 9, 15, 0, 0, DateTimeKind.Local), "Economy", new DateTime(2026, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 21, 6, 30, 0, 0, DateTimeKind.Local), 142.69999999999999, "WN774", "Dallas", 159.00m, "Chicago" },
                    { 8, "Boeing 737 MAX 8", 4, "southwest.png", "Southwest", new DateTime(2026, 4, 25, 19, 5, 0, 0, DateTimeKind.Local), "Economy Plus", new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 25, 16, 0, 0, 0, DateTimeKind.Local), 148.0, "WN885", "Chicago", 178.00m, "Dallas" },
                    { 9, "Airbus A319", 1, "american.png", "American Airlines", new DateTime(2026, 4, 23, 13, 40, 0, 0, DateTimeKind.Local), "Basic Economy", new DateTime(2026, 4, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 23, 11, 0, 0, 0, DateTimeKind.Local), 77.5, "AA323", "New York", 119.00m, "Miami" },
                    { 10, "Airbus A321neo", 2, "delta.png", "Delta", new DateTime(2026, 4, 26, 11, 15, 0, 0, DateTimeKind.Local), "Business", new DateTime(2026, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 4, 26, 8, 30, 0, 0, DateTimeKind.Local), 265.0, "DL997", "Miami", 549.00m, "Los Angeles" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");
        }
    }
}
