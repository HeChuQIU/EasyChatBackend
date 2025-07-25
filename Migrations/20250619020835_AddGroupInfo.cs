using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyChatBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GroupOwnerId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupNotice = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JoinType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateIndex(
                name: "idx_key_groupid",
                table: "Groups",
                column: "GroupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
