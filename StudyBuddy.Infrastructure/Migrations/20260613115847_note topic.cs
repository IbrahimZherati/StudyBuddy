using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notetopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteTopicId",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoteTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Topic = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTopics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTopicId",
                table: "Notes",
                column: "NoteTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteTopics_NoteTopicId",
                table: "Notes",
                column: "NoteTopicId",
                principalTable: "NoteTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteTopics_NoteTopicId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "NoteTopics");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteTopicId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteTopicId",
                table: "Notes");
        }
    }
}
