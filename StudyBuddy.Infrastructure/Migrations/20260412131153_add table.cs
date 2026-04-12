using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeFeed_ClientUser_ClientUserId",
                table: "ClientUserLikeFeed");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeFeed_Feed_FeedId",
                table: "ClientUserLikeFeed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientUserLikeFeed",
                table: "ClientUserLikeFeed");

            migrationBuilder.RenameTable(
                name: "ClientUserLikeFeed",
                newName: "ClientUserLikeFeeds");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUserLikeFeed_FeedId",
                table: "ClientUserLikeFeeds",
                newName: "IX_ClientUserLikeFeeds_FeedId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUserLikeFeed_ClientUserId",
                table: "ClientUserLikeFeeds",
                newName: "IX_ClientUserLikeFeeds_ClientUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientUserLikeFeeds",
                table: "ClientUserLikeFeeds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeFeeds_ClientUser_ClientUserId",
                table: "ClientUserLikeFeeds",
                column: "ClientUserId",
                principalTable: "ClientUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeFeeds_Feed_FeedId",
                table: "ClientUserLikeFeeds",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeFeeds_ClientUser_ClientUserId",
                table: "ClientUserLikeFeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeFeeds_Feed_FeedId",
                table: "ClientUserLikeFeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientUserLikeFeeds",
                table: "ClientUserLikeFeeds");

            migrationBuilder.RenameTable(
                name: "ClientUserLikeFeeds",
                newName: "ClientUserLikeFeed");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUserLikeFeeds_FeedId",
                table: "ClientUserLikeFeed",
                newName: "IX_ClientUserLikeFeed_FeedId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUserLikeFeeds_ClientUserId",
                table: "ClientUserLikeFeed",
                newName: "IX_ClientUserLikeFeed_ClientUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientUserLikeFeed",
                table: "ClientUserLikeFeed",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeFeed_ClientUser_ClientUserId",
                table: "ClientUserLikeFeed",
                column: "ClientUserId",
                principalTable: "ClientUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeFeed_Feed_FeedId",
                table: "ClientUserLikeFeed",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
