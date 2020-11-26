using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Data.Migrations
{
    public partial class initial5345345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchoolName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolName", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SchoolName",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "نامشخص" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolName");
        }
    }
}
