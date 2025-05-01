using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThongTinLietSi.Migrations
{
    /// <inheritdoc />
    public partial class Init0123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonVi",
                table: "LietSis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonVi",
                table: "LietSis");
        }
    }
}
