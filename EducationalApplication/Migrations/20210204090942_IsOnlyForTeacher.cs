using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class IsOnlyForTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyForTeacher",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyForTeacher",
                table: "Announcements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21e7811a-522a-4991-90b6-3218d84fc5ca", new DateTime(2021, 2, 4, 12, 39, 42, 89, DateTimeKind.Local).AddTicks(5653), "AQAAAAEAACcQAAAAEFijzJ3MbLIfNancr+VIoqMSM8JeYuj3i3l3WuAHYJ0tDAJtgle/1KPVoZ5ufbypGg==", "748dd9d9-c500-4154-8134-b8fc3d2d28ff" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlyForTeacher",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsOnlyForTeacher",
                table: "Announcements");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe7e7c0e-124b-44a1-ad67-1198dbbcf901", new DateTime(2021, 2, 3, 10, 44, 50, 532, DateTimeKind.Local).AddTicks(4439), "AQAAAAEAACcQAAAAEOdDwY5U1ohtYFRAXCoGpo/BQSD/2AZq954lxF8KjpWBtxbWj7IgYLCeHmWWnFd6ug==", "bc3113cc-353d-4687-8831-c0dfbe0c354c" });
        }
    }
}
