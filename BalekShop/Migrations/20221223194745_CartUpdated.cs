using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalekShop.Migrations
{
    public partial class CartUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
