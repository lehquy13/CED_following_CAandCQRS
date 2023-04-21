using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_ClassInfor_Entity_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ClassInformation",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInformation_SubjectId",
                table: "ClassInformation",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassInformation_Subject_SubjectId",
                table: "ClassInformation",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassInformation_Subject_SubjectId",
                table: "ClassInformation");

            migrationBuilder.DropIndex(
                name: "IX_ClassInformation_SubjectId",
                table: "ClassInformation");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ClassInformation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
