using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class CustomGroupsToEducationPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomGroupsToEducationPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomGroupId = table.Column<int>(nullable: false),
                    EducationPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomGroupsToEducationPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomGroupsToEducationPosts_CustomGroups_CustomGroupId",
                        column: x => x.CustomGroupId,
                        principalTable: "CustomGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomGroupsToEducationPosts_EducationPosts_EducationPostId",
                        column: x => x.EducationPostId,
                        principalTable: "EducationPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca6464a9-77b6-4442-bba6-e5b19ac23f0d", new DateTime(2021, 2, 6, 9, 16, 46, 473, DateTimeKind.Local).AddTicks(5996), "AQAAAAEAACcQAAAAEPwxm34gs99m9nN2aHWolH2skM0TtGzTDmQ1loNaLFrx61NFatse2Wr/oYrjTp0/jQ==", "59f6026c-3ef0-492d-9b7e-e9e64f8afa48" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroupsToEducationPosts_CustomGroupId",
                table: "CustomGroupsToEducationPosts",
                column: "CustomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomGroupsToEducationPosts_EducationPostId",
                table: "CustomGroupsToEducationPosts",
                column: "EducationPostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomGroupsToEducationPosts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21e7811a-522a-4991-90b6-3218d84fc5ca", new DateTime(2021, 2, 4, 12, 39, 42, 89, DateTimeKind.Local).AddTicks(5653), "AQAAAAEAACcQAAAAEFijzJ3MbLIfNancr+VIoqMSM8JeYuj3i3l3WuAHYJ0tDAJtgle/1KPVoZ5ufbypGg==", "748dd9d9-c500-4154-8134-b8fc3d2d28ff" });
        }
    }
}
