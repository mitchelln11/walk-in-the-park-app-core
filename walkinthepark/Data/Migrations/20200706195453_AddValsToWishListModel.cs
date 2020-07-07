using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class AddValsToWishListModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HikerId",
                table: "Wishlist",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkId",
                table: "Wishlist",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParkName",
                table: "Wishlist",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HikerId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ParkId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ParkName",
                table: "Wishlist");
        }
    }
}
