using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fine",
                table: "BookIssue",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fine",
                table: "BookIssue");
        }
    }
}
