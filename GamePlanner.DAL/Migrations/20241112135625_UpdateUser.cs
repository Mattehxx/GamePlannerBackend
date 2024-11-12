using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Knowledges_KnowledgeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Knowledges");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_KnowledgeId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "KnowledgeId",
                table: "AspNetUsers",
                newName: "Level");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "AspNetUsers",
                newName: "KnowledgeId");

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.KnowledgeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KnowledgeId",
                table: "AspNetUsers",
                column: "KnowledgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Knowledges_KnowledgeId",
                table: "AspNetUsers",
                column: "KnowledgeId",
                principalTable: "Knowledges",
                principalColumn: "KnowledgeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
