using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixBookIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookIssues",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookIssues");
        }
    }
}
