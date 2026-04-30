using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makegendernullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_Universities_UniversityId",
                table: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_UniversityId",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "GroupChats");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "ClientUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "GroupChats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "ClientUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_UniversityId",
                table: "GroupChats",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_Universities_UniversityId",
                table: "GroupChats",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
