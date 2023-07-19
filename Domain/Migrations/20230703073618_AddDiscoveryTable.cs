using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddDiscoveryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SliderDiscoveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderDiscoveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SliderDiscoveriesTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LangCode = table.Column<string>(type: "text", nullable: false),
                    Tittle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Links = table.Column<string>(type: "text", nullable: false),
                    SliderDiscoveryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderDiscoveriesTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SliderDiscoveriesTranslates_SliderDiscoveries_SliderDiscove~",
                        column: x => x.SliderDiscoveryId,
                        principalTable: "SliderDiscoveries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SliderDiscoveriesTranslates_SliderDiscoveryId",
                table: "SliderDiscoveriesTranslates",
                column: "SliderDiscoveryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliderDiscoveriesTranslates");

            migrationBuilder.DropTable(
                name: "SliderDiscoveries");
        }
    }
}
