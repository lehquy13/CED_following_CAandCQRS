using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Request_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestGettingClass",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "RequestGettingClass",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestGettingClass");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "RequestGettingClass");
        }
    }
}
