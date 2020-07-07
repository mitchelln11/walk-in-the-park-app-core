using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class AdjustWishListViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Parks_ParkId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_ParkId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ParkId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "ParkName",
                table: "Wishlist");

            migrationBuilder.AddColumn<int>(
                name: "Park",
                table: "Parks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parks_Park",
                table: "Parks",
                column: "Park");

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_Wishlist_Park",
                table: "Parks",
                column: "Park",
                principalTable: "Wishlist",
                principalColumn: "HikerParkWishlistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parks_Wishlist_Park",
                table: "Parks");

            migrationBuilder.DropIndex(
                name: "IX_Parks_Park",
                table: "Parks");

            migrationBuilder.DropColumn(
                name: "Park",
                table: "Parks");

            migrationBuilder.AddColumn<int>(
                name: "ParkId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParkName",
                table: "Wishlist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_ParkId",
                table: "Wishlist",
                column: "ParkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Parks_ParkId",
                table: "Wishlist",
                column: "ParkId",
                principalTable: "Parks",
                principalColumn: "ParkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
