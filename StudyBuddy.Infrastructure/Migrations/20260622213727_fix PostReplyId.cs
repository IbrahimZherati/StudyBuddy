using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixPostReplyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeReplies_PostReply_PostReplyId1",
                table: "ClientUserLikeReplies");

            migrationBuilder.DropIndex(
                name: "IX_ClientUserLikeReplies_PostReplyId1",
                table: "ClientUserLikeReplies");

            migrationBuilder.DropColumn(
                name: "PostReplyId1",
                table: "ClientUserLikeReplies");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostReplyId",
                table: "ClientUserLikeReplies",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeReplies_PostReplyId",
                table: "ClientUserLikeReplies",
                column: "PostReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeReplies_PostReply_PostReplyId",
                table: "ClientUserLikeReplies",
                column: "PostReplyId",
                principalTable: "PostReply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUserLikeReplies_PostReply_PostReplyId",
                table: "ClientUserLikeReplies");

            migrationBuilder.DropIndex(
                name: "IX_ClientUserLikeReplies_PostReplyId",
                table: "ClientUserLikeReplies");

            migrationBuilder.AlterColumn<int>(
                name: "PostReplyId",
                table: "ClientUserLikeReplies",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "PostReplyId1",
                table: "ClientUserLikeReplies",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeReplies_PostReplyId1",
                table: "ClientUserLikeReplies",
                column: "PostReplyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUserLikeReplies_PostReply_PostReplyId1",
                table: "ClientUserLikeReplies",
                column: "PostReplyId1",
                principalTable: "PostReply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
