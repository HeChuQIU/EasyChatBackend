using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyChatBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupInfoAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AvatarCover",
                table: "Groups",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AvatarFile",
                table: "Groups",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarCover",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AvatarFile",
                table: "Groups");
        }
    }
}
