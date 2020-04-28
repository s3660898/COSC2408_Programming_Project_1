using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShare.Data.Migrations
{
    public partial class FixedLongitudeTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Cars",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.Sql("UPDATE Cars SET Longitude = Longitue");

            migrationBuilder.DropColumn(
                name: "Longitue",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Longitue",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.Sql("UPDATE Cars SET Longitue = Longitude");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Cars");
        }
    }
}
