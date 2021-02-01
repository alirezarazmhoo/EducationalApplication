using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class FavoritModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_Banners_BannerId",
                table: "Favorits");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_EducationPosts_EducationPostId",
                table: "Favorits");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_Students_StudentsId",
                table: "Favorits");

            migrationBuilder.AlterColumn<int>(
                name: "StudentsId",
                table: "Favorits",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EducationPostId",
                table: "Favorits",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BannerId",
                table: "Favorits",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_Banners_BannerId",
                table: "Favorits",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_EducationPosts_EducationPostId",
                table: "Favorits",
                column: "EducationPostId",
                principalTable: "EducationPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_Students_StudentsId",
                table: "Favorits",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_Banners_BannerId",
                table: "Favorits");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_EducationPosts_EducationPostId",
                table: "Favorits");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_Students_StudentsId",
                table: "Favorits");

            migrationBuilder.AlterColumn<int>(
                name: "StudentsId",
                table: "Favorits",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationPostId",
                table: "Favorits",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BannerId",
                table: "Favorits",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_Banners_BannerId",
                table: "Favorits",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_EducationPosts_EducationPostId",
                table: "Favorits",
                column: "EducationPostId",
                principalTable: "EducationPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_Students_StudentsId",
                table: "Favorits",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
