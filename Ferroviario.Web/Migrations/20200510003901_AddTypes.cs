using Microsoft.EntityFrameworkCore.Migrations;

namespace Ferroviario.Web.Migrations
{
    public partial class AddTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeEntityId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestTypeEntityId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestTypeEntityId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TypeId",
                table: "Requests",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestTypes_TypeId",
                table: "Requests",
                column: "TypeId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestTypes_TypeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_TypeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestTypeEntityId",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestTypeEntityId",
                table: "Requests",
                column: "RequestTypeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestTypes_RequestTypeEntityId",
                table: "Requests",
                column: "RequestTypeEntityId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
