using Microsoft.EntityFrameworkCore.Migrations;

namespace walkinthepark.Data.Migrations
{
    public partial class DiscriminatorErrorGetRegisteredUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "Hikers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Hikers_ApplicationId",
                table: "Hikers",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hikers_AspNetUsers_ApplicationId",
                table: "Hikers",
                column: "ApplicationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hikers_AspNetUsers_ApplicationId",
                table: "Hikers");

            migrationBuilder.DropIndex(
                name: "IX_Hikers_ApplicationId",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Hikers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
