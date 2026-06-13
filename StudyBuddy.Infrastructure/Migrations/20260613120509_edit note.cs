using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editnote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteTopics_NoteTopicId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteTopicId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "NoteTopics");

            migrationBuilder.RenameColumn(
                name: "NoteTopicId",
                table: "Notes",
                newName: "IsFavorite");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "NoteTopics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "NoteTopics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTopics_NoteId",
                table: "NoteTopics",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTopics_TopicId",
                table: "NoteTopics",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTopics_Notes_NoteId",
                table: "NoteTopics",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTopics_Topics_TopicId",
                table: "NoteTopics",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTopics_Notes_NoteId",
                table: "NoteTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteTopics_Topics_TopicId",
                table: "NoteTopics");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_NoteTopics_NoteId",
                table: "NoteTopics");

            migrationBuilder.DropIndex(
                name: "IX_NoteTopics_TopicId",
                table: "NoteTopics");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "NoteTopics");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "NoteTopics");

            migrationBuilder.RenameColumn(
                name: "IsFavorite",
                table: "Notes",
                newName: "NoteTopicId");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "NoteTopics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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
    }
}
