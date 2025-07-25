using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyChatBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContactApply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserContactApplies",
                columns: table => new
                {
                    ApplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplyUserId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    ReceiveUserId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    LastApplyTime = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ApplyInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContactApplies", x => x.ApplyId);
                });

            migrationBuilder.CreateIndex(
                name: "idx_apply_user_id",
                table: "UserContactApplies",
                column: "ApplyUserId");

            migrationBuilder.CreateIndex(
                name: "idx_contact_id",
                table: "UserContactApplies",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "idx_receive_user_id",
                table: "UserContactApplies",
                column: "ReceiveUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserContactApplies");
        }
    }
}
