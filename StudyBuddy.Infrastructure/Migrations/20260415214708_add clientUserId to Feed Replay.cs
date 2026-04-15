using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addclientUserIdtoFeedReplay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "FeedReplays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedReplays_ClientUserId",
                table: "FeedReplays",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedReplays_ClientUsers_ClientUserId",
                table: "FeedReplays",
                column: "ClientUserId",
                principalTable: "ClientUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedReplays_ClientUsers_ClientUserId",
                table: "FeedReplays");

            migrationBuilder.DropIndex(
                name: "IX_FeedReplays_ClientUserId",
                table: "FeedReplays");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "FeedReplays");
        }
    }
}
