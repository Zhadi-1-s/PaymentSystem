using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Payments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AllowedCurrencies = table.Column<string>(type: "TEXT", nullable: false),
                    MinAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AllowedCurrencies", "Description", "IsActive", "MaxAmount", "MinAmount", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "USD", "International banking operations. USD only.", true, 50000m, 10m, "IBO Finance" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "EUR,RUB", "European transfers. EUR and RUB accepted.", true, 20000m, 5m, "EuroTrans" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "USD,EUR,RUB", "Global payments. All currencies accepted.", true, 100000m, 1m, "GlobalPay" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Companies_CompanyId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CompanyId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Payments");
        }
    }
}
