using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyChatBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserContacts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ContactId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContacts", x => new { x.UserId, x.ContactId });
                });

            migrationBuilder.CreateIndex(
                name: "idx_contact_id",
                table: "UserContacts",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserContacts");
        }
    }
}
