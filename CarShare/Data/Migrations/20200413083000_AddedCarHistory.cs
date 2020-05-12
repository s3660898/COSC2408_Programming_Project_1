using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShare.Data.Migrations
{
    public partial class AddedCarHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HireTime = table.Column<DateTime>(nullable: false),
                    HireDuration = table.Column<TimeSpan>(nullable: false),
                    InitialLatitude = table.Column<double>(nullable: false),
                    InitialLongitude = table.Column<double>(nullable: false),
                    ReturnedLatitude = table.Column<double>(nullable: false),
                    ReturnedLongitude = table.Column<double>(nullable: false),
                    ReturnedTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarHistory_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarHistory_CarId",
                table: "CarHistory",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarHistory_UserId",
                table: "CarHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarHistory");
        }
    }
}
