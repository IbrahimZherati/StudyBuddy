using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addclientUserIdtoFeedReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "FeedReplys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedReplys_ClientUserId",
                table: "FeedReplys",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedReplys_ClientUsers_ClientUserId",
                table: "FeedReplys",
                column: "ClientUserId",
                principalTable: "ClientUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedReplys_ClientUsers_ClientUserId",
                table: "FeedReplys");

            migrationBuilder.DropIndex(
                name: "IX_FeedReplys_ClientUserId",
                table: "FeedReplys");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "FeedReplys");
        }
    }
}
