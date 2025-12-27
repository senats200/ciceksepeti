using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ciceksepeti.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWeeklySalesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeeklySales",
                columns: table => new
                {
                    
                    WeekStartDate = table.Column<string>(type: "datetime2(7)", nullable: false),
                    WeekEndDate = table.Column<decimal>(type: "datetime2(7)", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeklySales");
        }
    }
}
