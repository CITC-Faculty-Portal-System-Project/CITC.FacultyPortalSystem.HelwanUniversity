using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentsToFacultyMemberAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferencesAndSeminarsAttachments_AttachmentsReferences_AttachmentId",
                table: "ConferencesAndSeminarsAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId",
                table: "ConferencesAndSeminarsAttachments");

            migrationBuilder.CreateTable(
                name: "FacultyMemberAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_FacultyMemberAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyMemberAttachments_AttachmentsReferences_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "AttachmentsReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacultyMemberAttachments_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments",
                columns: new[] { "AttachmentId", "ConferenceOrSeminarId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMemberAttachments_AttachmentId",
                table: "FacultyMemberAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMemberAttachments_FacultyMemberId_AttachmentId",
                table: "FacultyMemberAttachments",
                columns: new[] { "FacultyMemberId", "AttachmentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConferencesAndSeminarsAttachments_AttachmentsReferences_AttachmentId",
                table: "ConferencesAndSeminarsAttachments",
                column: "AttachmentId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferencesAndSeminarsAttachments_AttachmentsReferences_AttachmentId",
                table: "ConferencesAndSeminarsAttachments");

            migrationBuilder.DropTable(
                name: "FacultyMemberAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId",
                table: "ConferencesAndSeminarsAttachments",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferencesAndSeminarsAttachments_AttachmentsReferences_AttachmentId",
                table: "ConferencesAndSeminarsAttachments",
                column: "AttachmentId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
