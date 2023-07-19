using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddFooterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooterItemTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterItemTranslates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoftDelete = table.Column<bool>(type: "boolean", nullable: false),
                    FooterMenuId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterItems_FooterMenus_FooterMenuId",
                        column: x => x.FooterMenuId,
                        principalTable: "FooterMenus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FooterMenuTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FooterMenuId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenuTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterMenuTranslates_FooterMenus_FooterMenuId",
                        column: x => x.FooterMenuId,
                        principalTable: "FooterMenus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FooterItems_FooterMenuId",
                table: "FooterItems",
                column: "FooterMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenuTranslates_FooterMenuId",
                table: "FooterMenuTranslates",
                column: "FooterMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FooterItems");

            migrationBuilder.DropTable(
                name: "FooterItemTranslates");

            migrationBuilder.DropTable(
                name: "FooterMenuTranslates");

            migrationBuilder.DropTable(
                name: "FooterMenus");
        }
    }
}
