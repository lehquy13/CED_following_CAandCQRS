using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CED.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_TutorVerificationInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGettingClass_ClassInformation_Id",
                table: "RequestGettingClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestGettingClass_Tutor_Id",
                table: "RequestGettingClass");

            migrationBuilder.AddColumn<short>(
                name: "Rate",
                table: "Tutor",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "TutorVerificationInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorVerificationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorVerificationInfo_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestGettingClass_ClassInformationId",
                table: "RequestGettingClass",
                column: "ClassInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGettingClass_TutorId",
                table: "RequestGettingClass",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorVerificationInfo_TutorId",
                table: "TutorVerificationInfos",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGettingClass_ClassInformation_ClassInformationId",
                table: "RequestGettingClass",
                column: "ClassInformationId",
                principalTable: "ClassInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGettingClass_Tutor_TutorId",
                table: "RequestGettingClass",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGettingClass_ClassInformation_ClassInformationId",
                table: "RequestGettingClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestGettingClass_Tutor_TutorId",
                table: "RequestGettingClass");

            migrationBuilder.DropTable(
                name: "TutorVerificationInfos");

            migrationBuilder.DropIndex(
                name: "IX_RequestGettingClass_ClassInformationId",
                table: "RequestGettingClass");

            migrationBuilder.DropIndex(
                name: "IX_RequestGettingClass_TutorId",
                table: "RequestGettingClass");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Tutor");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGettingClass_ClassInformation_Id",
                table: "RequestGettingClass",
                column: "Id",
                principalTable: "ClassInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGettingClass_Tutor_Id",
                table: "RequestGettingClass",
                column: "Id",
                principalTable: "Tutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
