using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class Announcmentscanshow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Announcement",
                table: "Settings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableDays",
                table: "Announcements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62648aa8-fa05-44ac-ae81-fbaa63d5b53a", new DateTime(2021, 2, 2, 12, 20, 0, 374, DateTimeKind.Local).AddTicks(9530), "AQAAAAEAACcQAAAAEJARqAZCTprbNaxerTAV0ENW/Cdm8G5DVP19O/vzw+FsUX+RwQ2HKE9MDofdnAuN7Q==", "72aa3472-4ad0-4ee0-80d3-84fa1e1cf6d6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Announcement",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AvailableDays",
                table: "Announcements");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97981410-92ae-4abe-8119-3c0a302d0b97", new DateTime(2021, 2, 2, 12, 6, 8, 346, DateTimeKind.Local).AddTicks(1628), "AQAAAAEAACcQAAAAEE34NjP5qt7Lbc3L6B3B3HQ/OYYg2VEgQgcSBQiy9vCYJBAUq7glbaGcwnOIN5iZFw==", "3f3ee2ae-a93a-4fbe-afe0-bf2a09e100c8" });
        }
    }
}
