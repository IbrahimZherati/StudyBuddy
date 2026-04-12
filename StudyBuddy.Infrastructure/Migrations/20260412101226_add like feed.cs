using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addlikefeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientUserLikeFeed",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FeedId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserLikeFeed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeFeed_ClientUser_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeFeed_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeFeed_ClientUserId",
                table: "ClientUserLikeFeed",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeFeed_FeedId",
                table: "ClientUserLikeFeed",
                column: "FeedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientUserLikeFeed");
        }
    }
}
