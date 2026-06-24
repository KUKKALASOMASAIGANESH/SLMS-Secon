using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShelfToLibraryResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "LibraryResources",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryResources_ShelfId",
                table: "LibraryResources",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryResources_Shelves_ShelfId",
                table: "LibraryResources",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryResources_Shelves_ShelfId",
                table: "LibraryResources");

            migrationBuilder.DropIndex(
                name: "IX_LibraryResources_ShelfId",
                table: "LibraryResources");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "LibraryResources");
        }
    }
}
