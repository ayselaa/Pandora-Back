using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class ChangeFooterItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterItems_FooterMenus_FooterMenuId",
                table: "FooterItems");

            migrationBuilder.AlterColumn<int>(
                name: "FooterMenuId",
                table: "FooterItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FooterItems_FooterMenus_FooterMenuId",
                table: "FooterItems",
                column: "FooterMenuId",
                principalTable: "FooterMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FooterItems_FooterMenus_FooterMenuId",
                table: "FooterItems");

            migrationBuilder.AlterColumn<int>(
                name: "FooterMenuId",
                table: "FooterItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_FooterItems_FooterMenus_FooterMenuId",
                table: "FooterItems",
                column: "FooterMenuId",
                principalTable: "FooterMenus",
                principalColumn: "Id");
        }
    }
}
