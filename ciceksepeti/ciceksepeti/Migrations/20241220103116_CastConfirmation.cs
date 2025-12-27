using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ciceksepeti.Migrations
{
    /// <inheritdoc />
    public partial class CastConfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_OrderId",
                table: "OrderInformation",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInformation_Orders_OrderId",
                table: "OrderInformation",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInformation_Orders_OrderId",
                table: "OrderInformation");

            migrationBuilder.DropIndex(
                name: "IX_OrderInformation_OrderId",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderInformation");
        }
    }
}
