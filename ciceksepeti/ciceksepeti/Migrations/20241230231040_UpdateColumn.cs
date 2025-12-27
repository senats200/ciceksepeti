using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ciceksepeti.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
          name: "Id",
          table: "UserInfo");

            // 2. Kolonu tekrar oluştur ve Identity özelliğini ekle
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserInfo",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

        }

        /// <inheritdoc />
        
    }
}
