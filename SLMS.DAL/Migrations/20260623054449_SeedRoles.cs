using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "RoleName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 23, 5, 44, 49, 77, DateTimeKind.Utc).AddTicks(6920), "System Administrator", true, "Admin", null },
                    { 2, new DateTime(2026, 6, 23, 5, 44, 49, 77, DateTimeKind.Utc).AddTicks(6924), "Library Manager", true, "Librarian", null },
                    { 3, new DateTime(2026, 6, 23, 5, 44, 49, 77, DateTimeKind.Utc).AddTicks(6925), "Normal Employee", true, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
