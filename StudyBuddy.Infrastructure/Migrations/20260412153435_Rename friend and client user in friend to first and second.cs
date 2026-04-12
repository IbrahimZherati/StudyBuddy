using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Renamefriendandclientuserinfriendtofirstandsecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Friend_ClientUser",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "fk_Friend_ClientUser_0",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_FriendId",
                table: "Friend");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friend",
                newName: "SecondFriendId");

            migrationBuilder.RenameColumn(
                name: "ClientUserId",
                table: "Friend",
                newName: "FirstFriendId1");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_ClientUserId",
                table: "Friend",
                newName: "IX_Friend_FirstFriendId1");

            migrationBuilder.AddColumn<int>(
                name: "FirstFriendId",
                table: "Friend",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FirstFriendId",
                table: "Friend",
                column: "FirstFriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_ClientUser_FirstFriendId1",
                table: "Friend",
                column: "FirstFriendId1",
                principalTable: "ClientUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_Friend_ClientUser_0",
                table: "Friend",
                column: "FirstFriendId",
                principalTable: "ClientUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_ClientUser_FirstFriendId1",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "fk_Friend_ClientUser_0",
                table: "Friend");

            migrationBuilder.DropIndex(
                name: "IX_Friend_FirstFriendId",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "FirstFriendId",
                table: "Friend");

            migrationBuilder.RenameColumn(
                name: "SecondFriendId",
                table: "Friend",
                newName: "FriendId");

            migrationBuilder.RenameColumn(
                name: "FirstFriendId1",
                table: "Friend",
                newName: "ClientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_FirstFriendId1",
                table: "Friend",
                newName: "IX_Friend_ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendId",
                table: "Friend",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "fk_Friend_ClientUser",
                table: "Friend",
                column: "ClientUserId",
                principalTable: "ClientUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_Friend_ClientUser_0",
                table: "Friend",
                column: "FriendId",
                principalTable: "ClientUser",
                principalColumn: "Id");
        }
    }
}
