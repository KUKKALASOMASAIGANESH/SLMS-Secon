using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SLMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Employees_EmployeeId",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BookIssues_Users_IssuedByUserId",
                table: "BookIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReturns_BookIssues_BookIssueId",
                table: "BookReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReturns_Users_ReturnedByUserId",
                table: "BookReturns");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_BookReturns_BookIssueId",
                table: "BookReturns");

            migrationBuilder.DropIndex(
                name: "IX_BookReturns_ReturnedByUserId",
                table: "BookReturns");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_EmployeeId",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_InventoryItemId",
                table: "BookIssues");

            migrationBuilder.DropIndex(
                name: "IX_BookIssues_IssuedByUserId",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "ReturnedByUserId",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BookReturns");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookIssues");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "BookIssues");

            migrationBuilder.RenameColumn(
                name: "IssuedByUserId",
                table: "BookIssues",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "BookIssues",
                newName: "IsReturned");

            migrationBuilder.CreateTable(
                name: "BookIssue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedByUserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookIssue_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookIssue_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookIssue_Users_IssuedByUserId",
                        column: x => x.IssuedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookIssue_EmployeeId",
                table: "BookIssue",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssue_InventoryItemId",
                table: "BookIssue",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssue_IssuedByUserId",
                table: "BookIssue",
                column: "IssuedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookIssue");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookIssues",
                newName: "IssuedByUserId");

            migrationBuilder.RenameColumn(
                name: "IsReturned",
                table: "BookIssues",
                newName: "IsActive");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "BookReturns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BookReturns",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BookReturns",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "BookReturns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnedByUserId",
                table: "BookReturns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BookReturns",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BookIssues",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "BookIssues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BookIssues",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "BookIssues",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReturns_BookIssueId",
                table: "BookReturns",
                column: "BookIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReturns_ReturnedByUserId",
                table: "BookReturns",
                column: "ReturnedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_EmployeeId",
                table: "BookIssues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_InventoryItemId",
                table: "BookIssues",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_IssuedByUserId",
                table: "BookIssues",
                column: "IssuedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Employees_EmployeeId",
                table: "BookIssues",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_InventoryItems_InventoryItemId",
                table: "BookIssues",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookIssues_Users_IssuedByUserId",
                table: "BookIssues",
                column: "IssuedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReturns_BookIssues_BookIssueId",
                table: "BookReturns",
                column: "BookIssueId",
                principalTable: "BookIssues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReturns_Users_ReturnedByUserId",
                table: "BookReturns",
                column: "ReturnedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
