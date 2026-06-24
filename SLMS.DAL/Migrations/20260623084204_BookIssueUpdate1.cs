using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BookIssueUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Users_IssuedByUserId",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_IssuedByUserId",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "IssuedByUserId",
                table: "BookIssues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssuedByUserId",
                table: "BookIssues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_IssuedByUserId",
                table: "BookIssues",
                column: "IssuedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Users_IssuedByUserId",
                table: "BookIssues",
                column: "IssuedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
