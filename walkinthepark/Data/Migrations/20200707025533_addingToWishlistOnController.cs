using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class addingToWishlistOnController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HikerParkWishlist_Hikers_HikerId",
                table: "HikerParkWishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_HikerParkWishlist_Parks_ParkId",
                table: "HikerParkWishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HikerParkWishlist",
                table: "HikerParkWishlist");

            migrationBuilder.RenameTable(
                name: "HikerParkWishlist",
                newName: "HikerParkWishlists");

            migrationBuilder.RenameIndex(
                name: "IX_HikerParkWishlist_ParkId",
                table: "HikerParkWishlists",
                newName: "IX_HikerParkWishlists_ParkId");

            migrationBuilder.AddColumn<string>(
                name: "ParkName",
                table: "HikerParkWishlists",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HikerParkWishlists",
                table: "HikerParkWishlists",
                columns: new[] { "HikerId", "ParkId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HikerParkWishlists_Hikers_HikerId",
                table: "HikerParkWishlists",
                column: "HikerId",
                principalTable: "Hikers",
                principalColumn: "HikerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HikerParkWishlists_Parks_ParkId",
                table: "HikerParkWishlists",
                column: "ParkId",
                principalTable: "Parks",
                principalColumn: "ParkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HikerParkWishlists_Hikers_HikerId",
                table: "HikerParkWishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_HikerParkWishlists_Parks_ParkId",
                table: "HikerParkWishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HikerParkWishlists",
                table: "HikerParkWishlists");

            migrationBuilder.DropColumn(
                name: "ParkName",
                table: "HikerParkWishlists");

            migrationBuilder.RenameTable(
                name: "HikerParkWishlists",
                newName: "HikerParkWishlist");

            migrationBuilder.RenameIndex(
                name: "IX_HikerParkWishlists_ParkId",
                table: "HikerParkWishlist",
                newName: "IX_HikerParkWishlist_ParkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HikerParkWishlist",
                table: "HikerParkWishlist",
                columns: new[] { "HikerId", "ParkId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HikerParkWishlist_Hikers_HikerId",
                table: "HikerParkWishlist",
                column: "HikerId",
                principalTable: "Hikers",
                principalColumn: "HikerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HikerParkWishlist_Parks_ParkId",
                table: "HikerParkWishlist",
                column: "ParkId",
                principalTable: "Parks",
                principalColumn: "ParkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
