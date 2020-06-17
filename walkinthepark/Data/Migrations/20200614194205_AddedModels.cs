using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class AddedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Hikers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Hikers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Hikers",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Hikers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Hikers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Hikers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Hikers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Hikers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    ParkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkName = table.Column<string>(nullable: true),
                    ParkState = table.Column<string>(nullable: true),
                    ParkLatitude = table.Column<string>(nullable: true),
                    ParkLongitude = table.Column<string>(nullable: true),
                    ParkDescription = table.Column<string>(nullable: true),
                    ParkCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.ParkId);
                });

            migrationBuilder.CreateTable(
                name: "HikerParkWishlists",
                columns: table => new
                {
                    HikerParkWishlistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HikerId = table.Column<int>(nullable: false),
                    ParkId = table.Column<int>(nullable: false),
                    ParkName = table.Column<string>(nullable: true)
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
                name: "HikingTrails",
                columns: table => new
                {
                    TrailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrailName = table.Column<string>(nullable: true),
                    TrailDifficulty = table.Column<string>(nullable: true),
                    TrailSummary = table.Column<string>(nullable: true),
                    TrailLength = table.Column<double>(nullable: false),
                    TrailCondition = table.Column<string>(nullable: true),
                    HikingApiCode = table.Column<string>(nullable: true),
                    AverageUserRating = table.Column<decimal>(nullable: false),
                    ParkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HikingTrails", x => x.TrailId);
                    table.ForeignKey(
                        name: "FK_HikingTrails_Parks_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Parks",
                        principalColumn: "ParkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HikerTrailRatings",
                columns: table => new
                {
                    HikerTrailRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HikerId = table.Column<int>(nullable: false),
                    TrailId = table.Column<int>(nullable: false),
                    IndividualRating = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_HikingTrails_ParkId",
                table: "HikingTrails",
                column: "ParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HikerParkWishlists");

            migrationBuilder.DropTable(
                name: "HikerTrailRatings");

            migrationBuilder.DropTable(
                name: "HikingTrails");

            migrationBuilder.DropTable(
                name: "Parks");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Hikers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Hikers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Hikers",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hikers",
                newName: "id");
        }
    }
}
