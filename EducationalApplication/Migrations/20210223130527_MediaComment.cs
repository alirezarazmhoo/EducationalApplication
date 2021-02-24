using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class MediaComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdParent",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommentMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    DurationTime = table.Column<string>(nullable: true),
                    lenght = table.Column<string>(nullable: true),
                    CommentId = table.Column<int>(nullable: false),
                    MediaType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentMedias_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "817c452e-0c1a-4534-bd93-28cb3a51754c", new DateTime(2021, 2, 23, 16, 35, 26, 821, DateTimeKind.Local).AddTicks(8880), "AQAAAAEAACcQAAAAEEhkr0WYVNT8zwdGnLGL3+AW/LBiMlVl5nGVztgc1bfJQKBahA8NemGJYIZPyZCmeQ==", "1f72b696-8cf0-4fca-8118-c983b2cde567" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentMedias_CommentId",
                table: "CommentMedias",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentMedias");

            migrationBuilder.DropColumn(
                name: "IdParent",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ac6e418-8607-46ca-82b8-e3a56d668de7", new DateTime(2021, 2, 7, 15, 18, 4, 783, DateTimeKind.Local).AddTicks(6330), "AQAAAAEAACcQAAAAEMntdqyUgyauC8Zcf/DLSDFpUAJ4TqwjEsr+wUEIHBRUmJFuwgAi1VVB089TmqQOMg==", "9bb93642-4a8a-4b49-8ad0-2658b8340b3a" });
        }
    }
}
