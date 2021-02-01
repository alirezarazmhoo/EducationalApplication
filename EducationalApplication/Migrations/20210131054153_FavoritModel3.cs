using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class FavoritModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Favorits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_ApplicationUserId",
                table: "Favorits",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorits_AspNetUsers_ApplicationUserId",
                table: "Favorits",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorits_AspNetUsers_ApplicationUserId",
                table: "Favorits");

            migrationBuilder.DropIndex(
                name: "IX_Favorits_ApplicationUserId",
                table: "Favorits");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Favorits");
        }
    }
}
