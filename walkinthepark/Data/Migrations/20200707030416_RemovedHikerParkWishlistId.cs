using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class RemovedHikerParkWishlistId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HikerParkId",
                table: "HikerParkWishlists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HikerParkId",
                table: "HikerParkWishlists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
