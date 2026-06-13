using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addownertogroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientUserId",
                table: "GroupChats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_ClientUserId",
                table: "GroupChats",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_ClientUsers_ClientUserId",
                table: "GroupChats",
                column: "ClientUserId",
                principalTable: "ClientUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_ClientUsers_ClientUserId",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_ClientUserId",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "GroupChats");
        }
    }
}
