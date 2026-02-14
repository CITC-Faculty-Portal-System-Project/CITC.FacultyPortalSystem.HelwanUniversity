using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyMembers_AttachmentsReferences_ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.DropIndex(
                name: "IX_FacultyMembers_ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "PersonalData",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_ProfilePictureId",
                table: "PersonalData",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_AttachmentsReferences_ProfilePictureId",
                table: "PersonalData",
                column: "ProfilePictureId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_AttachmentsReferences_ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "FacultyMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_ProfilePictureId",
                table: "FacultyMembers",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyMembers_AttachmentsReferences_ProfilePictureId",
                table: "FacultyMembers",
                column: "ProfilePictureId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
