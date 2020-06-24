using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class AddHikingTrailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HikingApiCode",
                table: "HikingTrails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HikingApiCode",
                table: "HikingTrails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
