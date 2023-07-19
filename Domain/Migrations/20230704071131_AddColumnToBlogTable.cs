using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddColumnToBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogTranslates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogTranslates_BlogId",
                table: "BlogTranslates",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTranslates_Blogs_BlogId",
                table: "BlogTranslates",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTranslates_Blogs_BlogId",
                table: "BlogTranslates");

            migrationBuilder.DropIndex(
                name: "IX_BlogTranslates_BlogId",
                table: "BlogTranslates");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogTranslates");
        }
    }
}
