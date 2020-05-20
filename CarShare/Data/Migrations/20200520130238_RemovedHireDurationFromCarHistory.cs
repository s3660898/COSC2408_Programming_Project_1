using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShare.Data.Migrations
{
    public partial class RemovedHireDurationFromCarHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HireDuration",
                table: "CarHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HireDuration",
                table: "CarHistory",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
