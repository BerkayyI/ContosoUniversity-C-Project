using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    public partial class RemoveKursCourseIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Courses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentID",
                table: "Courses",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentID",
                table: "Courses",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Courses");
        }
    }
}
