using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesDataCollector.Migrations
{
    public partial class filesizeadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "FileSize",
                table: "GameData",
                type: "nvarchar(max)",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "GameData");
        }
    }
}
