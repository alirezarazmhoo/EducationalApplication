using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class IsForTeacherForCustomGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForTeacher",
                table: "CustomGroups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ac6e418-8607-46ca-82b8-e3a56d668de7", new DateTime(2021, 2, 7, 15, 18, 4, 783, DateTimeKind.Local).AddTicks(6330), "AQAAAAEAACcQAAAAEMntdqyUgyauC8Zcf/DLSDFpUAJ4TqwjEsr+wUEIHBRUmJFuwgAi1VVB089TmqQOMg==", "9bb93642-4a8a-4b49-8ad0-2658b8340b3a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForTeacher",
                table: "CustomGroups");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "CreateDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9631d06-37a6-4c24-b77e-fd9498d00453", new DateTime(2021, 2, 7, 12, 53, 1, 646, DateTimeKind.Local).AddTicks(6101), "AQAAAAEAACcQAAAAEER1caswt2k3DZACRiHOdfeI6l7RCoNck9n9Wr115bJgFq7TZVi/v8+OAl8v55FAUg==", "63426104-9fc6-415a-b54e-b178c3be9c1e" });
        }
    }
}
