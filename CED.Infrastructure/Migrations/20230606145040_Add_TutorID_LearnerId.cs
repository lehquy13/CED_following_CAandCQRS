using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_TutorID_LearnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClassInformation_StudentId",
                table: "ClassInformation",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInformation_TutorId",
                table: "ClassInformation",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassInformation_Tutor_TutorId",
                table: "ClassInformation",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassInformation_User_StudentId",
                table: "ClassInformation",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassInformation_Tutor_TutorId",
                table: "ClassInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassInformation_User_StudentId",
                table: "ClassInformation");

            migrationBuilder.DropIndex(
                name: "IX_ClassInformation_StudentId",
                table: "ClassInformation");

            migrationBuilder.DropIndex(
                name: "IX_ClassInformation_TutorId",
                table: "ClassInformation");
        }
    }
}
