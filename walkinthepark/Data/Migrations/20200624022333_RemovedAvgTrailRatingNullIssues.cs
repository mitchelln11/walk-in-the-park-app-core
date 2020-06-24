using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class RemovedAvgTrailRatingNullIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageUserRating",
                table: "HikingTrails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageUserRating",
                table: "HikingTrails",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
