using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerId",
                table: "BannerTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BannerTranslates_BannerId",
                table: "BannerTranslates",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerTranslates_Banners_BannerId",
                table: "BannerTranslates");

            migrationBuilder.DropIndex(
                name: "IX_BannerTranslates_BannerId",
                table: "BannerTranslates");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "BannerTranslates");
        }
    }
}
