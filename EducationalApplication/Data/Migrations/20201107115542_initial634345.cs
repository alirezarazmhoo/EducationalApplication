using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Data.Migrations
{
    public partial class initial634345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "EducationPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EducationPosts_TeacherId",
                table: "EducationPosts",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPosts_Teachers_TeacherId",
                table: "EducationPosts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationPosts_Teachers_TeacherId",
                table: "EducationPosts");

            migrationBuilder.DropIndex(
                name: "IX_EducationPosts_TeacherId",
                table: "EducationPosts");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "EducationPosts");
        }
    }
}
