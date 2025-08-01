using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyAndIconPathToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Categories");
        }
    }
}
