using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Data.Migrations
{
    public partial class initial54535 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    SocialNetWorkLink = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    AvailableDate = table.Column<DateTime>(nullable: false),
                    CreditDays = table.Column<int>(nullable: false),
                    BannerPlace = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    ShowOnMainPage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banners_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BannerToPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannerId = table.Column<int>(nullable: false),
                    EducationPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerToPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerToPosts_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannerToPosts_EducationPosts_EducationPostId",
                        column: x => x.EducationPostId,
                        principalTable: "EducationPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_CategoryId",
                table: "Banners",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BannerToPosts_BannerId",
                table: "BannerToPosts",
                column: "BannerId");

            migrationBuilder.CreateIndex(
                name: "IX_BannerToPosts_EducationPostId",
                table: "BannerToPosts",
                column: "EducationPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerToPosts");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Students");
        }
    }
}
