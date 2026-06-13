using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addgrouptonotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "Notifications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_GroupChatId",
                table: "Notifications",
                column: "GroupChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_GroupChats_GroupChatId",
                table: "Notifications",
                column: "GroupChatId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_GroupChats_GroupChatId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_GroupChatId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "Notifications");
        }
    }
}
