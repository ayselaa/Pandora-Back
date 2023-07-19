using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class ChangeBannerProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Banners");

            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Banners",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Banners");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Banners",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
