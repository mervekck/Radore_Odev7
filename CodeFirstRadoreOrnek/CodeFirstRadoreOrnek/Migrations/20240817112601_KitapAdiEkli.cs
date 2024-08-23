using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirstRadoreOrnek.Migrations
{
    /// <inheritdoc />
    public partial class KitapAdiEkli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kitapAdi",
                table: "Kitap",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kitapAdi",
                table: "Kitap");
        }
    }
}
