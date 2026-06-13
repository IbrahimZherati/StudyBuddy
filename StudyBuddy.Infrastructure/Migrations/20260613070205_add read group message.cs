using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addreadgroupmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientUserGroupMessageReads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MessageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUserGroupMessageReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUserGroupMessageReads_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientUserGroupMessageReads_GroupMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "GroupMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserGroupMessageReads_ClientUserId",
                table: "ClientUserGroupMessageReads",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientUserGroupMessageReads_MessageId",
                table: "ClientUserGroupMessageReads",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientUserGroupMessageReads");
        }
    }
}
