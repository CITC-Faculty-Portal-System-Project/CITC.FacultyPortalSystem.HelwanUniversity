using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttachmentsRefactorInResearchesAndThesesforTestingPurposes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicQualifications_AttachmentsReferences_AttachmentId",
                table: "AcademicQualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_AttachmentsReferences_ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.DropTable(
                name: "ConferencesAndSeminarsAttachments");

            migrationBuilder.DropTable(
                name: "FacultyMemberAttachments");

            migrationBuilder.DropTable(
                name: "AttachmentsReferences");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.DropIndex(
                name: "IX_AcademicQualifications_AttachmentId",
                table: "AcademicQualifications");

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Researches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResearchAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResearchId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    HashAlg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nonce = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Tag = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    KeyRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrappedDek = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StorageProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemotePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchAttachments_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThesesAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThesisId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    HashAlg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nonce = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Tag = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    KeyRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrappedDek = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    StorageProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemotePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesesAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesesAttachments_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Researches_ThesisId",
                table: "Researches",
                column: "ThesisId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchAttachments_ResearchId",
                table: "ResearchAttachments",
                column: "ResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesesAttachments_ThesisId",
                table: "ThesesAttachments",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches");

            migrationBuilder.DropTable(
                name: "ResearchAttachments");

            migrationBuilder.DropTable(
                name: "ThesesAttachments");

            migrationBuilder.DropIndex(
                name: "IX_Researches_ThesisId",
                table: "Researches");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Researches");

            migrationBuilder.CreateTable(
                name: "AttachmentsReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashAlg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KeyRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nonce = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RemotePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    StorageProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    WrappedDek = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentsReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConferencesAndSeminarsAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConferenceOrSeminarId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminarsAttachments_ConferencesAndSeminars_ConferenceOrSeminarId",
                        column: x => x.ConferenceOrSeminarId,
                        principalTable: "ConferencesAndSeminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyMemberAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "IX_PersonalData_ProfilePictureId",
                table: "PersonalData",
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
                name: "IX_ConferencesAndSeminarsAttachments_AttachmentId_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments",
                columns: new[] { "AttachmentId", "ConferenceOrSeminarId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments",
                column: "ConferenceOrSeminarId");

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
                name: "FK_AcademicQualifications_AttachmentsReferences_AttachmentId",
                table: "AcademicQualifications",
                column: "AttachmentId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_AttachmentsReferences_ProfilePictureId",
                table: "PersonalData",
                column: "ProfilePictureId",
                principalTable: "AttachmentsReferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
