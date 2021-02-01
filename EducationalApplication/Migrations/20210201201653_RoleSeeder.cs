using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class RoleSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "190b4dfc-ea9a-4d7b-872c-e453752ec483");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "6fb0ca0f-9248-4bea-8b3e-2e44a6c81e82");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ApiToken", "ConcurrencyStamp", "CreateDate", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "Mobile", "NationalCode", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Url", "UserName", "UserType" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, null, null, "29a64ffb-5471-4310-ab2a-3388ce6774b1", new DateTime(2021, 2, 1, 23, 35, 38, 144, DateTimeKind.Local).AddTicks(7274), null, false, null, false, null, 0L, null, null, "MainOwner", null, "AQAAAAEAACcQAAAAELZRkHCFepFMIY3LQg4ZTiJHuwygb5DrUyBUJa+5l/bv1IKqMsGpC93UIL+7W0GO7Q==", null, false, "b568cd27-382d-4854-8116-35da4742f4a5", false, null, "MainOwner", 0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });
        }
    }
}
