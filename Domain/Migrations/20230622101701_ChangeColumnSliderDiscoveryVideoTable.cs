using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class ChangeColumnSliderDiscoveryVideoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderDiscoveryVideoId",
                table: "SliderDiscoveryVideoTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SliderDiscoveryVideoTranslates_SliderDiscoveryVideoId",
                table: "SliderDiscoveryVideoTranslates",
                column: "SliderDiscoveryVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SliderDiscoveryVideoTranslates_SliderDiscoveryVideos_Slider~",
                table: "SliderDiscoveryVideoTranslates",
                column: "SliderDiscoveryVideoId",
                principalTable: "SliderDiscoveryVideos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderDiscoveryVideoTranslates_SliderDiscoveryVideos_Slider~",
                table: "SliderDiscoveryVideoTranslates");

            migrationBuilder.DropIndex(
                name: "IX_SliderDiscoveryVideoTranslates_SliderDiscoveryVideoId",
                table: "SliderDiscoveryVideoTranslates");

            migrationBuilder.DropColumn(
                name: "SliderDiscoveryVideoId",
                table: "SliderDiscoveryVideoTranslates");
        }
    }
}
