using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingAttachmentsToFacultyMemberAndMissionsAndAcademicProgressionModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "FacultyMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "AcademicQualifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConferencesAndSeminarsAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConferenceOrSeminarId = table.Column<int>(type: "int", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferencesAndSeminarsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminarsAttachments_AttachmentsReferences_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "AttachmentsReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminarsAttachments_ConferencesAndSeminars_ConferenceOrSeminarId",
                        column: x => x.ConferenceOrSeminarId,
                        principalTable: "ConferencesAndSeminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_ProfilePictureId",
                table: "FacultyMembers",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_AttachmentId",
                table: "AcademicQualifications",
                column: "AttachmentId",
                unique: true,
                filter: "[AttachmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId",
                table: "ConferencesAndSeminarsAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments",
                column: "ConferenceOrSeminarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicQualifications_AttachmentsReferences_AttachmentId",
                table: "AcademicQualifications",
                column: "AttachmentId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyMembers_AttachmentsReferences_ProfilePictureId",
                table: "FacultyMembers",
                column: "ProfilePictureId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicQualifications_AttachmentsReferences_AttachmentId",
                table: "AcademicQualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyMembers_AttachmentsReferences_ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.DropTable(
                name: "ConferencesAndSeminarsAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FacultyMembers_ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicQualifications_AttachmentId",
                table: "AcademicQualifications");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "FacultyMembers");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "AcademicQualifications");
        }
    }
}
