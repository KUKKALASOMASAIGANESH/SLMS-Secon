using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BookIssueUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryItemId",
                table: "BookIssues",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "LibraryResourceId",
                table: "BookIssues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_LibraryResourceId",
                table: "BookIssues",
                column: "LibraryResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_LibraryResources_LibraryResourceId",
                table: "BookIssues",
                column: "LibraryResourceId",
                principalTable: "LibraryResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_LibraryResources_LibraryResourceId",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_LibraryResourceId",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "LibraryResourceId",
                table: "BookIssues");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryItemId",
                table: "BookIssues",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
