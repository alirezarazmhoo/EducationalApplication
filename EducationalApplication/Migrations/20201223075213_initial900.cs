using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationalApplication.Migrations
{
    public partial class initial900 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersToClassRooms_ClassRooms_ClassRoomId",
                table: "TeachersToClassRooms");

            migrationBuilder.AlterColumn<int>(
                name: "ClassRoomId",
                table: "TeachersToClassRooms",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersToClassRooms_ClassRooms_ClassRoomId",
                table: "TeachersToClassRooms",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersToClassRooms_ClassRooms_ClassRoomId",
                table: "TeachersToClassRooms");

            migrationBuilder.AlterColumn<int>(
                name: "ClassRoomId",
                table: "TeachersToClassRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersToClassRooms_ClassRooms_ClassRoomId",
                table: "TeachersToClassRooms",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
