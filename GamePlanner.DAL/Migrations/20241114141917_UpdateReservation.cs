using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "Reservations");
        }
    }
}
