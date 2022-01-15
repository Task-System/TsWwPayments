using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TsWwPayments.Migrations
{
    public partial class Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "Transmissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Transmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "Transmissions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Transmissions");
        }
    }
}
