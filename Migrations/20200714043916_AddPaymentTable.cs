using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTube.Migrations
{
    public partial class AddPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    StripeUserId = table.Column<string>(nullable: true),
                    current_period_start = table.Column<DateTime>(nullable: false),
                    current_period_end = table.Column<DateTime>(nullable: false),
                    PlanType = table.Column<string>(nullable: true),
                    Last4 = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
