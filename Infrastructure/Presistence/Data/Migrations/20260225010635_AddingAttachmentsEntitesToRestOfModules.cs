using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingAttachmentsEntitesToRestOfModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "PersonalData");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Theses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Supervisings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Researches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ResearchDerivedFrom",
                table: "Researches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherType",
                table: "Researches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PublicationType",
                table: "Researches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JournalOrConfernce",
                table: "Researches",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "AcademicQualificationAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualificationId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AcademicQualificationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicQualificationAttachments_AcademicQualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "AcademicQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferencesAndSeminarsAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConferenceOrSeminarId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ConferencesAndSeminarsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminarsAttachments_ConferencesAndSeminars_ConferenceOrSeminarId",
                        column: x => x.ConferenceOrSeminarId,
                        principalTable: "ConferencesAndSeminars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManifestationsOfScientificAppreciationAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManifestationOfScientificAppreciationId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ManifestationsOfScientificAppreciationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManifestationsOfScientificAppreciationAttachments_ManifestationsOfScientificAppreciation_ManifestationOfScientificAppreciati~",
                        column: x => x.ManifestationOfScientificAppreciationId,
                        principalTable: "ManifestationsOfScientificAppreciation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentsAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatentId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PatentsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatentsAttachments_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrizesAndAwardsAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrizeAndAwardId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PrizesAndAwardsAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrizesAndAwardsAttachments_PrizesAndRewards_PrizeAndAwardId",
                        column: x => x.PrizeAndAwardId,
                        principalTable: "PrizesAndRewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalDataId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ProfilePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePictures_PersonalData_PersonalDataId",
                        column: x => x.PersonalDataId,
                        principalTable: "PersonalData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Theses_EnrollmentDate",
                table: "Theses",
                column: "EnrollmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_RegistrationDate",
                table: "Theses",
                column: "RegistrationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_Type",
                table: "Theses",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_DiscussionDate",
                table: "Supervisings",
                column: "DiscussionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_GrantingDate",
                table: "Supervisings",
                column: "GrantingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_RegistrationDate",
                table: "Supervisings",
                column: "RegistrationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_SupervisionFormationDate",
                table: "Supervisings",
                column: "SupervisionFormationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_Type",
                table: "Supervisings",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_JournalOrConfernce",
                table: "Researches",
                column: "JournalOrConfernce");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_PublicationType",
                table: "Researches",
                column: "PublicationType");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_PublisherType",
                table: "Researches",
                column: "PublisherType");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_PubYear",
                table: "Researches",
                column: "PubYear");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_ResearchDerivedFrom",
                table: "Researches",
                column: "ResearchDerivedFrom");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_Source",
                table: "Researches",
                column: "Source");

            migrationBuilder.CreateIndex(
                name: "IX_Researches_Title",
                table: "Researches",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualificationAttachments_QualificationId",
                table: "AcademicQualificationAttachments",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminarsAttachments_ConferenceOrSeminarId",
                table: "ConferencesAndSeminarsAttachments",
                column: "ConferenceOrSeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationsOfScientificAppreciationAttachments_ManifestationOfScientificAppreciationId",
                table: "ManifestationsOfScientificAppreciationAttachments",
                column: "ManifestationOfScientificAppreciationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentsAttachments_PatentId",
                table: "PatentsAttachments",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizesAndAwardsAttachments_PrizeAndAwardId",
                table: "PrizesAndAwardsAttachments",
                column: "PrizeAndAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePictures_PersonalDataId",
                table: "ProfilePictures",
                column: "PersonalDataId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicQualificationAttachments");

            migrationBuilder.DropTable(
                name: "ConferencesAndSeminarsAttachments");

            migrationBuilder.DropTable(
                name: "ManifestationsOfScientificAppreciationAttachments");

            migrationBuilder.DropTable(
                name: "PatentsAttachments");

            migrationBuilder.DropTable(
                name: "PrizesAndAwardsAttachments");

            migrationBuilder.DropTable(
                name: "ProfilePictures");

            migrationBuilder.DropIndex(
                name: "IX_Theses_EnrollmentDate",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_RegistrationDate",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_Type",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_DiscussionDate",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_GrantingDate",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_RegistrationDate",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_SupervisionFormationDate",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Supervisings_Type",
                table: "Supervisings");

            migrationBuilder.DropIndex(
                name: "IX_Researches_JournalOrConfernce",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_PublicationType",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_PublisherType",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_PubYear",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_ResearchDerivedFrom",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_Source",
                table: "Researches");

            migrationBuilder.DropIndex(
                name: "IX_Researches_Title",
                table: "Researches");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Theses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Supervisings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Researches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ResearchDerivedFrom",
                table: "Researches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherType",
                table: "Researches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PublicationType",
                table: "Researches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "JournalOrConfernce",
                table: "Researches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "PersonalData",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
