using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceApp.Migrations
{
    public partial class Registrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_ApplicationUser_ApplicationUserId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Cars_CarId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Events_EventId",
                table: "Registration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registration",
                table: "Registration");

            migrationBuilder.RenameTable(
                name: "Registration",
                newName: "Registrations");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_EventId",
                table: "Registrations",
                newName: "IX_Registrations_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_CarId",
                table: "Registrations",
                newName: "IX_Registrations_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_ApplicationUserId",
                table: "Registrations",
                newName: "IX_Registrations_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_ApplicationUser_ApplicationUserId",
                table: "Registrations",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Cars_CarId",
                table: "Registrations",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Events_EventId",
                table: "Registrations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_ApplicationUser_ApplicationUserId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Cars_CarId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Events_EventId",
                table: "Registrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.RenameTable(
                name: "Registrations",
                newName: "Registration");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_EventId",
                table: "Registration",
                newName: "IX_Registration_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_CarId",
                table: "Registration",
                newName: "IX_Registration_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ApplicationUserId",
                table: "Registration",
                newName: "IX_Registration_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registration",
                table: "Registration",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_ApplicationUser_ApplicationUserId",
                table: "Registration",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "ApplicationUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Cars_CarId",
                table: "Registration",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Events_EventId",
                table: "Registration",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
