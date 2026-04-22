using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makemajorrequiredinclientUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUsers_Majors_MajorId",
                table: "ClientUsers");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "ClientUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUsers_Majors_MajorId",
                table: "ClientUsers",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUsers_Majors_MajorId",
                table: "ClientUsers");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "ClientUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUsers_Majors_MajorId",
                table: "ClientUsers",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id");
        }
    }
}
