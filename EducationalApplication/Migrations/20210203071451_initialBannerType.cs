using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initialBannerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyForTeacher",
                table: "Banners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Announcements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe7e7c0e-124b-44a1-ad67-1198dbbcf901", new DateTime(2021, 2, 3, 10, 44, 50, 532, DateTimeKind.Local).AddTicks(4439), "AQAAAAEAACcQAAAAEOdDwY5U1ohtYFRAXCoGpo/BQSD/2AZq954lxF8KjpWBtxbWj7IgYLCeHmWWnFd6ug==", "bc3113cc-353d-4687-8831-c0dfbe0c354c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlyForTeacher",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Announcements");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62648aa8-fa05-44ac-ae81-fbaa63d5b53a", new DateTime(2021, 2, 2, 12, 20, 0, 374, DateTimeKind.Local).AddTicks(9530), "AQAAAAEAACcQAAAAEJARqAZCTprbNaxerTAV0ENW/Cdm8G5DVP19O/vzw+FsUX+RwQ2HKE9MDofdnAuN7Q==", "72aa3472-4ad0-4ee0-80d3-84fa1e1cf6d6" });
        }
    }
}
