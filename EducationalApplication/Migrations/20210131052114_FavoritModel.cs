using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class FavoritModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationPostId = table.Column<int>(nullable: false),
                    BannerId = table.Column<int>(nullable: false),
                    StudentsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorits_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorits_EducationPosts_EducationPostId",
                        column: x => x.EducationPostId,
                        principalTable: "EducationPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorits_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_BannerId",
                table: "Favorits",
                column: "BannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_EducationPostId",
                table: "Favorits",
                column: "EducationPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorits_StudentsId",
                table: "Favorits",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorits");
        }
    }
}
