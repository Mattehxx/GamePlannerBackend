using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessions_AspNetUsers_MasterId",
                table: "GameSessions");

            migrationBuilder.RenameColumn(
                name: "GameSessionEndTime",
                table: "GameSessions",
                newName: "GameSessionStartDate");

            migrationBuilder.RenameColumn(
                name: "GameSessionDate",
                table: "GameSessions",
                newName: "GameSessionEndDate");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Recurrences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "MasterId",
                table: "GameSessions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessions_AspNetUsers_MasterId",
                table: "GameSessions",
                column: "MasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessions_AspNetUsers_MasterId",
                table: "GameSessions");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Recurrences");

            migrationBuilder.RenameColumn(
                name: "GameSessionStartDate",
                table: "GameSessions",
                newName: "GameSessionEndTime");

            migrationBuilder.RenameColumn(
                name: "GameSessionEndDate",
                table: "GameSessions",
                newName: "GameSessionDate");

            migrationBuilder.AlterColumn<string>(
                name: "MasterId",
                table: "GameSessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessions_AspNetUsers_MasterId",
                table: "GameSessions",
                column: "MasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
