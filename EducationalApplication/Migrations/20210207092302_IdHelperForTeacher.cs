using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class IdHelperForTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdHelper",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9631d06-37a6-4c24-b77e-fd9498d00453", new DateTime(2021, 2, 7, 12, 53, 1, 646, DateTimeKind.Local).AddTicks(6101), "AQAAAAEAACcQAAAAEER1caswt2k3DZACRiHOdfeI6l7RCoNck9n9Wr115bJgFq7TZVi/v8+OAl8v55FAUg==", "63426104-9fc6-415a-b54e-b178c3be9c1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHelper",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca6464a9-77b6-4442-bba6-e5b19ac23f0d", new DateTime(2021, 2, 6, 9, 16, 46, 473, DateTimeKind.Local).AddTicks(5996), "AQAAAAEAACcQAAAAEPwxm34gs99m9nN2aHWolH2skM0TtGzTDmQ1loNaLFrx61NFatse2Wr/oYrjTp0/jQ==", "59f6026c-3ef0-492d-9b7e-e9e64f8afa48" });
        }
    }
}
