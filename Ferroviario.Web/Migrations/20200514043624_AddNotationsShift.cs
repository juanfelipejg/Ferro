using Microsoft.EntityFrameworkCore.Migrations;

namespace Ferroviario.Web.Migrations
{
    public partial class AddNotationsShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_AspNetUsers_UserId",
                table: "ShiftEntity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShiftEntity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ShiftEntity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_AspNetUsers_UserId",
                table: "ShiftEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_AspNetUsers_UserId",
                table: "ShiftEntity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShiftEntity",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ShiftEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_AspNetUsers_UserId",
                table: "ShiftEntity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
