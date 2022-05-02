using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesDataCollector.Migrations
{
    public partial class appidfk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppId",
                table: "Users",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Applications_AppId",
                table: "Users",
                column: "AppId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Applications_AppId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AppId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Users");
        }
    }
}
