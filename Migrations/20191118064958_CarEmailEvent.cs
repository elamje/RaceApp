using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceApp.Migrations
{
    public partial class CarEmailEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "CarNumber", "EngineBuilder", "EngineType", "Make", "Model" },
                values: new object[] { 1, 96, null, null, "Ford", "GT" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "CarNumber", "EngineBuilder", "EngineType", "Make", "Model" },
                values: new object[] { 2, 12, "Carol Shelby", "Gas", "Ferrari", "Enzo" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Cost", "DateTime", "DiscountedCost", "EpochWeekendNum", "Type" },
                values: new object[] { 1, 12.5m, new DateTime(2020, 3, 15, 5, 20, 0, 0, DateTimeKind.Unspecified), 10m, null, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);
        }
    }
}
