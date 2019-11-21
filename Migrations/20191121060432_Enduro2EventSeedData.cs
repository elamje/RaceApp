using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceApp.Migrations
{
    public partial class Enduro2EventSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Cost", "DateTime", "Description", "DiscountedCost", "EpochWeekendNum", "Name", "Type" },
                values: new object[] { 4, 500m, new DateTime(2020, 3, 16, 5, 20, 0, 0, DateTimeKind.Unspecified), "Event Details", 200m, 10, "Weekend10 Enduro 2", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4);
        }
    }
}
