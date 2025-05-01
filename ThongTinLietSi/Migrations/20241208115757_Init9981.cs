using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThongTinLietSi.Migrations
{
    /// <inheritdoc />
    public partial class Init9981 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img",
                table: "TinTucs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "TinTucs");
        }
    }
}
