using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnchancementOnTheisToResolveConfilcts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "ThesisComittees");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DiscussionDate",
                table: "Theses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityOrFaculty",
                table: "Theses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Supervisings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Supervisings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_DiscussionDate",
                table: "Theses",
                column: "DiscussionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_UniversityOrFaculty",
                table: "Theses",
                column: "UniversityOrFaculty");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_ThesisId",
                table: "Supervisings",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisings_Theses_ThesisId",
                table: "Supervisings",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisings_Theses_ThesisId",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Theses_DiscussionDate",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_UniversityOrFaculty",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_ThesisId",
                table: "Supervisings");

            migrationBuilder.DropColumn(
                name: "DiscussionDate",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "UniversityOrFaculty",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Supervisings");

            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Supervisings");

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "ThesisComittees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
