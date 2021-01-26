using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initiaFERT5TL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "UsersToCustomGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "UsersToCustomGroups");
        }
    }
}
