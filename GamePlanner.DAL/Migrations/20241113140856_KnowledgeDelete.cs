using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlanner.DAL.Migrations
{
    /// <inheritdoc />
    public partial class KnowledgeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Knowledges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Knowledges");
        }
    }
}
