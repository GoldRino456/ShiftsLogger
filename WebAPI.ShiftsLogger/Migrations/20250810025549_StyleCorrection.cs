using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.ShiftsLogger.Migrations
{
    /// <inheritdoc />
    public partial class StyleCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "durationInHours",
                table: "Shifts",
                newName: "DurationInHours");

            migrationBuilder.RenameColumn(
                name: "clockOutTime",
                table: "Shifts",
                newName: "ClockOutTime");

            migrationBuilder.RenameColumn(
                name: "clockInTime",
                table: "Shifts",
                newName: "ClockInTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationInHours",
                table: "Shifts",
                newName: "durationInHours");

            migrationBuilder.RenameColumn(
                name: "ClockOutTime",
                table: "Shifts",
                newName: "clockOutTime");

            migrationBuilder.RenameColumn(
                name: "ClockInTime",
                table: "Shifts",
                newName: "clockInTime");
        }
    }
}
