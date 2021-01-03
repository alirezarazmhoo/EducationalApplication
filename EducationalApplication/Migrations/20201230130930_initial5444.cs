using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initial5444 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomGroups_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersToCustomGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    StudentsId = table.Column<int>(nullable: true),
                    CustomGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToCustomGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersToCustomGroups_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersToCustomGroups_CustomGroups_CustomGroupId",
                        column: x => x.CustomGroupId,
                        principalTable: "CustomGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersToCustomGroups_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroups_ApplicationUserId",
                table: "CustomGroups",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToCustomGroups_ApplicationUserId",
                table: "UsersToCustomGroups",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToCustomGroups_CustomGroupId",
                table: "UsersToCustomGroups",
                column: "CustomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersToCustomGroups_StudentsId",
                table: "UsersToCustomGroups",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersToCustomGroups");

            migrationBuilder.DropTable(
                name: "CustomGroups");
        }
    }
}
