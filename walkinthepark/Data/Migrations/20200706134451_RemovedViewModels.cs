using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class RemovedViewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HikerParkWishlists");

            migrationBuilder.DropTable(
                name: "HikerTrailRatings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HikerParkWishlists",
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
                    table.PrimaryKey("PK_HikerParkWishlists", x => x.HikerParkWishlistId);
                    table.ForeignKey(
                        name: "FK_HikerParkWishlists_Hikers_HikerId",
                        column: x => x.HikerId,
                        principalTable: "Hikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HikerParkWishlists_Parks_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Parks",
                        principalColumn: "ParkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HikerTrailRatings",
                columns: table => new
                {
                    HikerTrailRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HikerId = table.Column<int>(type: "int", nullable: false),
                    IndividualRating = table.Column<int>(type: "int", nullable: false),
                    TrailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HikerTrailRatings", x => x.HikerTrailRatingId);
                    table.ForeignKey(
                        name: "FK_HikerTrailRatings_Hikers_HikerId",
                        column: x => x.HikerId,
                        principalTable: "Hikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HikerTrailRatings_HikingTrails_TrailId",
                        column: x => x.TrailId,
                        principalTable: "HikingTrails",
                        principalColumn: "TrailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HikerParkWishlists_HikerId",
                table: "HikerParkWishlists",
                column: "HikerId");

            migrationBuilder.CreateIndex(
                name: "IX_HikerParkWishlists_ParkId",
                table: "HikerParkWishlists",
                column: "ParkId");

            migrationBuilder.CreateIndex(
                name: "IX_HikerTrailRatings_HikerId",
                table: "HikerTrailRatings",
                column: "HikerId");

            migrationBuilder.CreateIndex(
                name: "IX_HikerTrailRatings_TrailId",
                table: "HikerTrailRatings",
                column: "TrailId");
        }
    }
}
