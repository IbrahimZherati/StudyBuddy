using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixmessageandnotificationiduniqe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_FromClientUserId_ToClientUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromClientUserId_ToClientUserId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromClientUserId",
                table: "Notifications",
                column: "FromClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromClientUserId",
                table: "Messages",
                column: "FromClientUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_FromClientUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromClientUserId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromClientUserId_ToClientUserId",
                table: "Notifications",
                columns: new[] { "FromClientUserId", "ToClientUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromClientUserId_ToClientUserId",
                table: "Messages",
                columns: new[] { "FromClientUserId", "ToClientUserId" },
                unique: true);
        }
    }
}
