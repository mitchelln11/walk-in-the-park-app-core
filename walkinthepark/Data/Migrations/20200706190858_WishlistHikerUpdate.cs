using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class WishlistHikerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Hikers_HikerId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_HikerId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "HikerId",
                table: "Wishlist");

            migrationBuilder.AddColumn<int>(
                name: "Hiker",
                table: "Hikers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hikers_Hiker",
                table: "Hikers",
                column: "Hiker");

            migrationBuilder.AddForeignKey(
                name: "FK_Hikers_Wishlist_Hiker",
                table: "Hikers",
                column: "Hiker",
                principalTable: "Wishlist",
                principalColumn: "HikerParkWishlistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hikers_Wishlist_Hiker",
                table: "Hikers");

            migrationBuilder.DropIndex(
                name: "IX_Hikers_Hiker",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Hiker",
                table: "Hikers");

            migrationBuilder.AddColumn<int>(
                name: "HikerId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_HikerId",
                table: "Wishlist",
                column: "HikerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Hikers_HikerId",
                table: "Wishlist",
                column: "HikerId",
                principalTable: "Hikers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
