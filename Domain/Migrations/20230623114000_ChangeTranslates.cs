using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class ChangeTranslates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterItemId",
                table: "FooterItemTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FooterItemTranslates_FooterItemId",
                table: "FooterItemTranslates",
                column: "FooterItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_FooterItemTranslates_FooterItems_FooterItemId",
                table: "FooterItemTranslates",
                column: "FooterItemId",
                principalTable: "FooterItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterItemTranslates_FooterItems_FooterItemId",
                table: "FooterItemTranslates");

            migrationBuilder.DropIndex(
                name: "IX_FooterItemTranslates_FooterItemId",
                table: "FooterItemTranslates");

            migrationBuilder.DropColumn(
                name: "FooterItemId",
                table: "FooterItemTranslates");
        }
    }
}
