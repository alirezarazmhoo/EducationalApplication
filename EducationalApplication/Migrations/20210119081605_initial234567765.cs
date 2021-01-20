using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initial234567765 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NeedEducationPostsToAccept = table.Column<bool>(nullable: false),
                    NeedBannersToAccept = table.Column<bool>(nullable: false),
                    BannerCanShow = table.Column<int>(nullable: false),
                    PostCanShow = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "BannerCanShow", "NeedBannersToAccept", "NeedEducationPostsToAccept", "PostCanShow" },
                values: new object[] { 1, 0, false, false, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
