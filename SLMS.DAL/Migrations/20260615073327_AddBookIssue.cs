using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBookIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "BookIssues",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_EmployeeId",
                table: "BookIssues",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Employees_EmployeeId",
                table: "BookIssues",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Employees_EmployeeId",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_EmployeeId",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "BookIssues");
        }
    }
}
