using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TsWwPayments.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentsAccounts",
                columns: table => new
                {
                    PaymentsAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsAccounts", x => x.PaymentsAccountId);
                });

            migrationBuilder.CreateTable(
                name: "Transmissions",
                columns: table => new
                {
                    TransmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferredAmount = table.Column<long>(type: "bigint", nullable: false),
                    PaymentsAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transmissions", x => x.TransmissionId);
                    table.ForeignKey(
                        name: "FK_Transmissions_PaymentsAccounts_PaymentsAccountId",
                        column: x => x.PaymentsAccountId,
                        principalTable: "PaymentsAccounts",
                        principalColumn: "PaymentsAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transmissions_PaymentsAccountId",
                table: "Transmissions",
                column: "PaymentsAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transmissions");

            migrationBuilder.DropTable(
                name: "PaymentsAccounts");
        }
    }
}
