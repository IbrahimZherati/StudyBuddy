using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateinvitegroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "GroupInvites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupInvites_GroupChatId",
                table: "GroupInvites",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupInvites_GroupChats_GroupChatId",
                table: "GroupInvites",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupInvites_GroupChats_GroupChatId",
                table: "GroupInvites");

            migrationBuilder.DropIndex(
                name: "IX_GroupInvites_GroupChatId",
                table: "GroupInvites");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "GroupInvites");
        }
    }
}
