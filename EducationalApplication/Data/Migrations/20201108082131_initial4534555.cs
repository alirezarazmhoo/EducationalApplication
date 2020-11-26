using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Data.Migrations
{
    public partial class initial4534555 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiToken",
                table: "Teachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiToken",
                table: "Teachers");
        }
    }
}
