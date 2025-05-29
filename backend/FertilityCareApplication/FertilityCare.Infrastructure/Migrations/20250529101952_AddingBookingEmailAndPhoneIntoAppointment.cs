using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingBookingEmailAndPhoneIntoAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingEmail",
                table: "Appointment",
                type: "NVARCHAR(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingPhone",
                table: "Appointment",
                type: "NVARCHAR(12)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingEmail",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "BookingPhone",
                table: "Appointment");
        }
    }
}
