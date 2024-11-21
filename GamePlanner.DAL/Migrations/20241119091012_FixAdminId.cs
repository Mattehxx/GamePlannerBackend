using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminUserId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminId",
                table: "Events",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AdminUserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminUserId",
                table: "Events",
                column: "AdminUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AdminUserId",
                table: "Events",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
