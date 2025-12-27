using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ciceksepeti.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

           


            migrationBuilder.CreateTable(
                name: "ProductSalesSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalQuantitySold = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSalesSummary");



        
        }
    }
}
