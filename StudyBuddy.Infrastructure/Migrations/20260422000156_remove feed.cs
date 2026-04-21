using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removefeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReply_Posts_PostId1",
                table: "PostReply");

            migrationBuilder.DropTable(
                name: "ClientUserLikeFeeds");

            migrationBuilder.DropTable(
                name: "FeedReplys");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_PostReply_PostId1",
                table: "PostReply");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "PostReply");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "PostReply",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_PostReply_PostId",
                table: "PostReply",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReply_Posts_PostId",
                table: "PostReply",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReply_Posts_PostId",
                table: "PostReply");

            migrationBuilder.DropIndex(
                name: "IX_PostReply_PostId",
                table: "PostReply");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostReply",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId1",
                table: "PostReply",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ShareCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientUserLikeFeeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FeedId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserLikeFeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeFeeds_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUserLikeFeeds_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedReplys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FeedId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedReplys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedReplys_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedReplys_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostReply_PostId1",
                table: "PostReply",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeFeeds_ClientUserId",
                table: "ClientUserLikeFeeds",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserLikeFeeds_FeedId",
                table: "ClientUserLikeFeeds",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedReplys_ClientUserId",
                table: "FeedReplys",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedReplys_FeedId",
                table: "FeedReplys",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_ClientUserId",
                table: "Feeds",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReply_Posts_PostId1",
                table: "PostReply",
                column: "PostId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
