using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ContactNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "ClassInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "ClassInformation");
        }
    }
}
