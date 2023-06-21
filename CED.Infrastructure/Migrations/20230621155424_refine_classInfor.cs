using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refine_classInfor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassInformation_User_StudentId",
                table: "ClassInformation");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "ClassInformation",
                newName: "LearnerId");

            migrationBuilder.RenameColumn(
                name: "StudentGender",
                table: "ClassInformation",
                newName: "NumberOfLearner");

            migrationBuilder.RenameColumn(
                name: "NumberOfStudent",
                table: "ClassInformation",
                newName: "LearnerGender");

            migrationBuilder.RenameIndex(
                name: "IX_ClassInformation_StudentId",
                table: "ClassInformation",
                newName: "IX_ClassInformation_LearnerId");

            migrationBuilder.AddColumn<string>(
                name: "LearnerName",
                table: "ClassInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassInformation_User_LearnerId",
                table: "ClassInformation",
                column: "LearnerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassInformation_User_LearnerId",
                table: "ClassInformation");

            migrationBuilder.DropColumn(
                name: "LearnerName",
                table: "ClassInformation");

            migrationBuilder.RenameColumn(
                name: "NumberOfLearner",
                table: "ClassInformation",
                newName: "StudentGender");

            migrationBuilder.RenameColumn(
                name: "LearnerId",
                table: "ClassInformation",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "LearnerGender",
                table: "ClassInformation",
                newName: "NumberOfStudent");

            migrationBuilder.RenameIndex(
                name: "IX_ClassInformation_LearnerId",
                table: "ClassInformation",
                newName: "IX_ClassInformation_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassInformation_User_StudentId",
                table: "ClassInformation",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
