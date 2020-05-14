using Microsoft.EntityFrameworkCore.Migrations;

namespace Ferroviario.Web.Migrations
{
    public partial class CorrectionDatacontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_FirstDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_SecondDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_ServiceEntity_Id",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_ServiceEntity_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceEntity",
                table: "ServiceEntity");

            migrationBuilder.RenameTable(
                name: "ServiceEntity",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntity_Name",
                table: "Services",
                newName: "IX_Services_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Services_FirstDriverServiceId",
                table: "Changes",
                column: "FirstDriverServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Services_SecondDriverServiceId",
                table: "Changes",
                column: "SecondDriverServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_Services_Id",
                table: "ServiceDetails",
                column: "Id",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_Services_FirstDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_Services_SecondDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_Services_Id",
                table: "ServiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_Services_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "ServiceEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Services_Name",
                table: "ServiceEntity",
                newName: "IX_ServiceEntity_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceEntity",
                table: "ServiceEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ServiceEntity_FirstDriverServiceId",
                table: "Changes",
                column: "FirstDriverServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ServiceEntity_SecondDriverServiceId",
                table: "Changes",
                column: "SecondDriverServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_ServiceEntity_Id",
                table: "ServiceDetails",
                column: "Id",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_ServiceEntity_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
