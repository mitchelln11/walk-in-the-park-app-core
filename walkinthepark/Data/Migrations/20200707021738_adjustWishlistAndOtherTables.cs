using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class adjustWishlistAndOtherTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hikers_Wishlist_Hiker",
                table: "Hikers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parks_Wishlist_Park",
                table: "Parks");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Parks_Park",
                table: "Parks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hikers",
                table: "Hikers");

            migrationBuilder.DropIndex(
                name: "IX_Hikers_Hiker",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Park",
                table: "Parks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Hiker",
                table: "Hikers");

            migrationBuilder.AddColumn<int>(
                name: "HikerId",
                table: "Hikers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hikers",
                table: "Hikers",
                column: "HikerId");

            migrationBuilder.CreateTable(
                name: "HikerParkWishlist",
                columns: table => new
                {
                    HikerId = table.Column<int>(nullable: false),
                    ParkId = table.Column<int>(nullable: false),
                    HikerParkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HikerParkWishlist", x => new { x.HikerId, x.ParkId });
                    table.ForeignKey(
                        name: "FK_HikerParkWishlist_Hikers_HikerId",
                        column: x => x.HikerId,
                        principalTable: "Hikers",
                        principalColumn: "HikerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HikerParkWishlist_Parks_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Parks",
                        principalColumn: "ParkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HikerParkWishlist_ParkId",
                table: "HikerParkWishlist",
                column: "ParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HikerParkWishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hikers",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "HikerId",
                table: "Hikers");

            migrationBuilder.AddColumn<int>(
                name: "Park",
                table: "Parks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Hikers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Hiker",
                table: "Hikers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hikers",
                table: "Hikers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    HikerParkWishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HikerId = table.Column<int>(type: "int", nullable: false),
                    ParkId = table.Column<int>(type: "int", nullable: false),
                    ParkName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.HikerParkWishlistId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parks_Park",
                table: "Parks",
                column: "Park");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_Wishlist_Park",
                table: "Parks",
                column: "Park",
                principalTable: "Wishlist",
                principalColumn: "HikerParkWishlistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
