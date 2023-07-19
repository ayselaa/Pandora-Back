using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class ChangeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "Products",
                newName: "ItemCode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "Stock");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "Package",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ItemCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Package",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "Products",
                newName: "ProductCode");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }
    }
}
