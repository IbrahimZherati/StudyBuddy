using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addLiketoPostReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientUserLikeReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostReplyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostReplyId1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserLikeReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeReplies_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeReplies_PostReply_PostReplyId1",
                        column: x => x.PostReplyId1,
                        principalTable: "PostReply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeReplies_ClientUserId",
                table: "ClientUserLikeReplies",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeReplies_PostReplyId1",
                table: "ClientUserLikeReplies",
                column: "PostReplyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientUserLikeReplies");
        }
    }
}
