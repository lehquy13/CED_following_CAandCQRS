using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refine_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorReview_Tutor_TutorId",
                table: "TutorReview");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorReview_User_LearnerId",
                table: "TutorReview");

            migrationBuilder.DropIndex(
                name: "IX_TutorReview_LearnerId",
                table: "TutorReview");

            migrationBuilder.DropIndex(
                name: "IX_TutorReview_TutorId",
                table: "TutorReview");

            migrationBuilder.DropColumn(
                name: "LearnerId",
                table: "TutorReview");

            migrationBuilder.RenameColumn(
                name: "TutorId",
                table: "TutorReview",
                newName: "ClassInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorReview_ClassInformationId",
                table: "TutorReview",
                column: "ClassInformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorReview_ClassInformation_ClassInformationId",
                table: "TutorReview",
                column: "ClassInformationId",
                principalTable: "ClassInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorReview_ClassInformation_ClassInformationId",
                table: "TutorReview");

            migrationBuilder.DropIndex(
                name: "IX_TutorReview_ClassInformationId",
                table: "TutorReview");

            migrationBuilder.RenameColumn(
                name: "ClassInformationId",
                table: "TutorReview",
                newName: "TutorId");

            migrationBuilder.AddColumn<Guid>(
                name: "LearnerId",
                table: "TutorReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TutorReview_LearnerId",
                table: "TutorReview",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorReview_TutorId",
                table: "TutorReview",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorReview_Tutor_TutorId",
                table: "TutorReview",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorReview_User_LearnerId",
                table: "TutorReview",
                column: "LearnerId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
