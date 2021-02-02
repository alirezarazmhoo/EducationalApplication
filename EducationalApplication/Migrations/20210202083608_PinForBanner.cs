using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class PinForBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pin",
                table: "Banners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97981410-92ae-4abe-8119-3c0a302d0b97", new DateTime(2021, 2, 2, 12, 6, 8, 346, DateTimeKind.Local).AddTicks(1628), "AQAAAAEAACcQAAAAEE34NjP5qt7Lbc3L6B3B3HQ/OYYg2VEgQgcSBQiy9vCYJBAUq7glbaGcwnOIN5iZFw==", "3f3ee2ae-a93a-4fbe-afe0-bf2a09e100c8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pin",
                table: "Banners");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "752f43f4-ddb3-4f78-82e4-7647d54743ae", new DateTime(2021, 2, 2, 8, 32, 17, 35, DateTimeKind.Local).AddTicks(7862), "AQAAAAEAACcQAAAAEF/I0uYuRW6GjjdS/I5gLzVM20w78sqr1dtV23uohYF3R41kz4GkiqkbtQE14Evwcw==", "ad7cce53-bf8c-43fa-be92-890ee5302dce" });
        }
    }
}
