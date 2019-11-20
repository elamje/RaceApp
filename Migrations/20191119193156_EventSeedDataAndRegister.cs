using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceApp.Migrations
{
    public partial class EventSeedDataAndRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUsers");

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountQualified = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registration_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Registration_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Cost", "DateTime", "Description", "DiscountedCost", "EpochWeekendNum", "Name", "Type" },
                values: new object[] { 1, 12.5m, new DateTime(2020, 3, 15, 5, 20, 0, 0, DateTimeKind.Unspecified), "Event Details", 10m, 10, "Weekend10 Enduro", 1 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Cost", "DateTime", "Description", "DiscountedCost", "EpochWeekendNum", "Name", "Type" },
                values: new object[] { 2, 5.5m, new DateTime(2020, 3, 15, 5, 20, 0, 0, DateTimeKind.Unspecified), "Event Details", 3m, 10, "Weekend10 Short", 2 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Cost", "DateTime", "Description", "DiscountedCost", "EpochWeekendNum", "Name", "Type" },
                values: new object[] { 3, 120.5m, new DateTime(2020, 4, 16, 5, 20, 0, 0, DateTimeKind.Unspecified), "Event Details", 100m, 16, "Weekend16 Enduro", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_ApplicationUserId",
                table: "Registration",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CarId",
                table: "Registration",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_EventId",
                table: "Registration",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "EventUsers",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUsers", x => new { x.EventId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_EventUsers_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "ApplicationUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUsers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUsers_ApplicationUserId",
                table: "EventUsers",
                column: "ApplicationUserId");
        }
    }
}
