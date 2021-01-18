using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initial5345534 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "EducationPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EducationPostViews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationPostId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPostViews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationPostViews");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "EducationPosts");
        }
    }
}
