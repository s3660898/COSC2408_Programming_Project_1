using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShare.Data.Migrations
{
    public partial class AddedParkingLotToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingLotId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ParkingLotId",
                table: "Cars",
                column: "ParkingLotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ParkingLots_ParkingLotId",
                table: "Cars",
                column: "ParkingLotId",
                principalTable: "ParkingLots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ParkingLots_ParkingLotId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ParkingLotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ParkingLotId",
                table: "Cars");
        }
    }
}
