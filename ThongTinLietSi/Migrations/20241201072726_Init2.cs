using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThongTinLietSi.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuanHuyenName",
                table: "NghiaTrangs");

            migrationBuilder.DropColumn(
                name: "TinhThanhName",
                table: "NghiaTrangs");

            migrationBuilder.DropColumn(
                name: "XaPhuongName",
                table: "NghiaTrangs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuanHuyenName",
                table: "NghiaTrangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TinhThanhName",
                table: "NghiaTrangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "XaPhuongName",
                table: "NghiaTrangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
