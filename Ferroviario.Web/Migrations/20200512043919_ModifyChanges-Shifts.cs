using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ferroviario.Web.Migrations
{
    public partial class ModifyChangesShifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_FirstServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_SecondServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_ServiceEntity_Id",
                table: "ShiftEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftEntity",
                table: "ShiftEntity");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Changes",
                newName: "SecondDriverId");

            migrationBuilder.RenameColumn(
                name: "SecondServiceId",
                table: "Changes",
                newName: "SecondDriverServiceId");

            migrationBuilder.RenameColumn(
                name: "FirstServiceId",
                table: "Changes",
                newName: "FirstDriverServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_SecondServiceId",
                table: "Changes",
                newName: "IX_Changes_SecondDriverServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_FirstServiceId",
                table: "Changes",
                newName: "IX_Changes_FirstDriverServiceId");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ShiftEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShiftEntity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShiftEntity",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "SecondDriverId",
                table: "Changes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstDriverId",
                table: "Changes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftEntity",
                table: "ShiftEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftEntity_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftEntity_UserId",
                table: "ShiftEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_FirstDriverId",
                table: "Changes",
                column: "FirstDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Changes_SecondDriverId",
                table: "Changes",
                column: "SecondDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_AspNetUsers_FirstDriverId",
                table: "Changes",
                column: "FirstDriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ServiceEntity_FirstDriverServiceId",
                table: "Changes",
                column: "FirstDriverServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_AspNetUsers_SecondDriverId",
                table: "Changes",
                column: "SecondDriverId",
                principalTable: "AspNetUsers",
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
                name: "FK_ShiftEntity_ServiceEntity_ServiceId",
                table: "ShiftEntity",
                column: "ServiceId",
                principalTable: "ServiceEntity",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_AspNetUsers_FirstDriverId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_FirstDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_AspNetUsers_SecondDriverId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_Changes_ServiceEntity_SecondDriverServiceId",
                table: "Changes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_ServiceEntity_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ShiftEntity_AspNetUsers_UserId",
                table: "ShiftEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftEntity",
                table: "ShiftEntity");

            migrationBuilder.DropIndex(
                name: "IX_ShiftEntity_ServiceId",
                table: "ShiftEntity");

            migrationBuilder.DropIndex(
                name: "IX_ShiftEntity_UserId",
                table: "ShiftEntity");

            migrationBuilder.DropIndex(
                name: "IX_Changes_FirstDriverId",
                table: "Changes");

            migrationBuilder.DropIndex(
                name: "IX_Changes_SecondDriverId",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "FirstDriverId",
                table: "Changes");

            migrationBuilder.RenameColumn(
                name: "SecondDriverServiceId",
                table: "Changes",
                newName: "SecondServiceId");

            migrationBuilder.RenameColumn(
                name: "SecondDriverId",
                table: "Changes",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "FirstDriverServiceId",
                table: "Changes",
                newName: "FirstServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_SecondDriverServiceId",
                table: "Changes",
                newName: "IX_Changes_SecondServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Changes_FirstDriverServiceId",
                table: "Changes",
                newName: "IX_Changes_FirstServiceId");

            migrationBuilder.AlterColumn<int>(
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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShiftEntity",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Changes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftEntity",
                table: "ShiftEntity",
                columns: new[] { "Id", "UserId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ServiceEntity_FirstServiceId",
                table: "Changes",
                column: "FirstServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_ServiceEntity_SecondServiceId",
                table: "Changes",
                column: "SecondServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftEntity_ServiceEntity_Id",
                table: "ShiftEntity",
                column: "Id",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
