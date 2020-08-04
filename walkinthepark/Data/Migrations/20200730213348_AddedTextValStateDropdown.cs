using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class AddedTextValStateDropdown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnumState",
                table: "Hikers");

            migrationBuilder.AddColumn<string>(
                name: "SelectedState",
                table: "Hikers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedState",
                table: "Hikers");

            migrationBuilder.AddColumn<int>(
                name: "EnumState",
                table: "Hikers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
