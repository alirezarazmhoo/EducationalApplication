using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initial542543 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersToEducationPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    StudentsId = table.Column<int>(nullable: true),
                    EducationPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToEducationPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersToEducationPosts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersToEducationPosts_EducationPosts_EducationPostId",
                        column: x => x.EducationPostId,
                        principalTable: "EducationPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersToEducationPosts_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersToEducationPosts_ApplicationUserId",
                table: "UsersToEducationPosts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToEducationPosts_EducationPostId",
                table: "UsersToEducationPosts",
                column: "EducationPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToEducationPosts_StudentsId",
                table: "UsersToEducationPosts",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersToEducationPosts");
        }
    }
}
