using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddBannerGiftTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerGifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerGifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannerGiftTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BannerGiftId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerGiftTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerGiftTranslates_BannerGifts_BannerGiftId",
                        column: x => x.BannerGiftId,
                        principalTable: "BannerGifts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannerGiftTranslates_BannerGiftId",
                table: "BannerGiftTranslates",
                column: "BannerGiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerGiftTranslates");

            migrationBuilder.DropTable(
                name: "BannerGifts");
        }
    }
}
