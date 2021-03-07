using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class Tickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tikets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    TeacherReceiver = table.Column<string>(nullable: true),
                    StudentReceiver = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tikets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tikets_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tikets_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiketMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    DurationTime = table.Column<string>(nullable: true),
                    lenght = table.Column<string>(nullable: true),
                    TiketId = table.Column<int>(nullable: false),
                    MediaType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiketMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiketMedias_Tikets_TiketId",
                        column: x => x.TiketId,
                        principalTable: "Tikets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a76fcc7-757f-49a6-9dfe-ee34c0a5e4f0", new DateTime(2021, 2, 27, 16, 49, 26, 894, DateTimeKind.Local).AddTicks(6958), "AQAAAAEAACcQAAAAEGzayFCtht8eyFMvw4gyGAsOZyT0Ox3YjF840Mv1lPLdgUiEYkJ0Opd83Y77A3tARg==", "1f852b47-7210-426b-ae94-28a46a9f7abd" });

            migrationBuilder.CreateIndex(
                name: "IX_TiketMedias_TiketId",
                table: "TiketMedias",
                column: "TiketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tikets_ApplicationUserId",
                table: "Tikets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tikets_StudentId",
                table: "Tikets",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiketMedias");

            migrationBuilder.DropTable(
                name: "Tikets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "817c452e-0c1a-4534-bd93-28cb3a51754c", new DateTime(2021, 2, 23, 16, 35, 26, 821, DateTimeKind.Local).AddTicks(8880), "AQAAAAEAACcQAAAAEEhkr0WYVNT8zwdGnLGL3+AW/LBiMlVl5nGVztgc1bfJQKBahA8NemGJYIZPyZCmeQ==", "1f72b696-8cf0-4fca-8118-c983b2cde567" });
        }
    }
}
