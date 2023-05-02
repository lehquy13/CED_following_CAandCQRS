using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserClassInfor_Juntion_Table_And_Avatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserClassInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isTutor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClassInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClassInformation_ClassInformation_ClassInformationId",
                        column: x => x.ClassInformationId,
                        principalTable: "ClassInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClassInformation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClassInformation_ClassInformationId",
                table: "UserClassInformation",
                column: "ClassInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClassInformation_UserId",
                table: "UserClassInformation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClassInformation");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");
        }
    }
}
