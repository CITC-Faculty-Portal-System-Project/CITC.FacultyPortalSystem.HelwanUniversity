using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class GeneralExperiencesModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContributionsToCommunityServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContributionTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateOfContribution = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ContributionsToCommunityServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributionsToCommunityServices_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributionsToUniversity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContributionTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TypeOfContributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfContribution = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_ContributionsToUniversity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributionsToUniversity_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributionsToUniversity_Lookups_TypeOfContributionId",
                        column: x => x.TypeOfContributionId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Authority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CountryOrCity = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_GeneralExperiences", x => x.Id);
                    table.CheckConstraint("CK_GeneralExp_Dates", "[EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_GeneralExperiences_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManifestationsOfScientificAppreciation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOfAppreciation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateOfAppreciation = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ManifestationsOfScientificAppreciation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManifestationsOfScientificAppreciation_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipationInQualityWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipationTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ParticipationInQualityWorks", x => x.Id);
                    table.CheckConstraint("CK_ParticipationInQualityWorks_Dates", "[EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_ParticipationInQualityWorks_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalOrInternational = table.Column<int>(type: "int", nullable: false),
                    NameOfPatent = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AccreditingAuthorityOrCountry = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ApplyingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AccreditationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_Patents", x => x.Id);
                    table.CheckConstraint("CK_Patents_Dates", "[AccreditationDate] >= [ApplyingDate]");
                    table.ForeignKey(
                        name: "FK_Patents_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrizesAndRewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwardingAuthority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateReceived = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_PrizesAndRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrizesAndRewards_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrizesAndRewards_Lookups_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScientificWritings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AuthorRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PublishingHouse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PublishingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ScientificWritings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificWritings_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScientificWritings_Lookups_AuthorRoleId",
                        column: x => x.AuthorRoleId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AcademicLevel = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UniversityOrFaculty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_TeachingExperiences", x => x.Id);
                    table.CheckConstraint("CK_TeachingExp_Dates", "[EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_TeachingExperiences_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_TrainingPrograms_Dates",
                table: "TrainingPrograms",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SciMissions_Dates",
                table: "ScientificMissions",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Projects_Dates",
                table: "Projects",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ConferencesAndSeminars_Dates",
                table: "ConferencesAndSeminars",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CommitteesAndAssociations_Dates",
                table: "CommitteesAndAssociations",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AdminPositions_Dates",
                table: "AdministrativePositions",
                sql: "[EndDate] >= [StartDate]");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToCommunityServices_ContributionTitle",
                table: "ContributionsToCommunityServices",
                column: "ContributionTitle");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToCommunityServices_DateOfContribution",
                table: "ContributionsToCommunityServices",
                column: "DateOfContribution");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToCommunityServices_FacultyMemberId",
                table: "ContributionsToCommunityServices",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToUniversity_ContributionTitle",
                table: "ContributionsToUniversity",
                column: "ContributionTitle");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToUniversity_DateOfContribution",
                table: "ContributionsToUniversity",
                column: "DateOfContribution");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToUniversity_FacultyMemberId",
                table: "ContributionsToUniversity",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributionsToUniversity_TypeOfContributionId",
                table: "ContributionsToUniversity",
                column: "TypeOfContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExperiences_Authority",
                table: "GeneralExperiences",
                column: "Authority");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExperiences_CountryOrCity",
                table: "GeneralExperiences",
                column: "CountryOrCity");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExperiences_ExperienceTitle",
                table: "GeneralExperiences",
                column: "ExperienceTitle");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExperiences_FacultyMemberId",
                table: "GeneralExperiences",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralExperiences_StartDate",
                table: "GeneralExperiences",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationsOfScientificAppreciation_DateOfAppreciation",
                table: "ManifestationsOfScientificAppreciation",
                column: "DateOfAppreciation");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationsOfScientificAppreciation_FacultyMemberId",
                table: "ManifestationsOfScientificAppreciation",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationsOfScientificAppreciation_IssuingAuthority",
                table: "ManifestationsOfScientificAppreciation",
                column: "IssuingAuthority");

            migrationBuilder.CreateIndex(
                name: "IX_ManifestationsOfScientificAppreciation_TitleOfAppreciation",
                table: "ManifestationsOfScientificAppreciation",
                column: "TitleOfAppreciation");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInQualityWorks_FacultyMemberId",
                table: "ParticipationInQualityWorks",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInQualityWorks_ParticipationTitle",
                table: "ParticipationInQualityWorks",
                column: "ParticipationTitle");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInQualityWorks_StartDate",
                table: "ParticipationInQualityWorks",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_AccreditationDate",
                table: "Patents",
                column: "AccreditationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_AccreditingAuthorityOrCountry",
                table: "Patents",
                column: "AccreditingAuthorityOrCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_ApplyingDate",
                table: "Patents",
                column: "ApplyingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_FacultyMemberId",
                table: "Patents",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_LocalOrInternational",
                table: "Patents",
                column: "LocalOrInternational");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_NameOfPatent",
                table: "Patents",
                column: "NameOfPatent");

            migrationBuilder.CreateIndex(
                name: "IX_PrizesAndRewards_AwardingAuthority",
                table: "PrizesAndRewards",
                column: "AwardingAuthority");

            migrationBuilder.CreateIndex(
                name: "IX_PrizesAndRewards_DateReceived",
                table: "PrizesAndRewards",
                column: "DateReceived");

            migrationBuilder.CreateIndex(
                name: "IX_PrizesAndRewards_FacultyMemberId",
                table: "PrizesAndRewards",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizesAndRewards_PrizeId",
                table: "PrizesAndRewards",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_AuthorRoleId",
                table: "ScientificWritings",
                column: "AuthorRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_FacultyMemberId",
                table: "ScientificWritings",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_ISBN",
                table: "ScientificWritings",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_PublishingDate",
                table: "ScientificWritings",
                column: "PublishingDate");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_PublishingHouse",
                table: "ScientificWritings",
                column: "PublishingHouse");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificWritings_Title",
                table: "ScientificWritings",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingExperiences_AcademicLevel",
                table: "TeachingExperiences",
                column: "AcademicLevel");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingExperiences_CourseName",
                table: "TeachingExperiences",
                column: "CourseName");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingExperiences_FacultyMemberId",
                table: "TeachingExperiences",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingExperiences_StartDate",
                table: "TeachingExperiences",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingExperiences_UniversityOrFaculty",
                table: "TeachingExperiences",
                column: "UniversityOrFaculty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContributionsToCommunityServices");

            migrationBuilder.DropTable(
                name: "ContributionsToUniversity");

            migrationBuilder.DropTable(
                name: "GeneralExperiences");

            migrationBuilder.DropTable(
                name: "ManifestationsOfScientificAppreciation");

            migrationBuilder.DropTable(
                name: "ParticipationInQualityWorks");

            migrationBuilder.DropTable(
                name: "Patents");

            migrationBuilder.DropTable(
                name: "PrizesAndRewards");

            migrationBuilder.DropTable(
                name: "ScientificWritings");

            migrationBuilder.DropTable(
                name: "TeachingExperiences");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TrainingPrograms_Dates",
                table: "TrainingPrograms");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SciMissions_Dates",
                table: "ScientificMissions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Projects_Dates",
                table: "Projects");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ConferencesAndSeminars_Dates",
                table: "ConferencesAndSeminars");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CommitteesAndAssociations_Dates",
                table: "CommitteesAndAssociations");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AdminPositions_Dates",
                table: "AdministrativePositions");
        }
    }
}
