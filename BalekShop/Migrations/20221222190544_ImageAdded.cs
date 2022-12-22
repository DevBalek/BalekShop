using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalekShop.Migrations
{
    public partial class ImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookImage",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImage",
                table: "Book");
        }
    }
}
