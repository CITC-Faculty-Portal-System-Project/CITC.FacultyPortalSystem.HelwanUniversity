using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacultyMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalNumber = table.Column<string>(type: "NVARCHAR(14)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
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
                    table.PrimaryKey("PK_FacultyMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ValueAr = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ValueEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_AdministrativePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativePositions_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPhoneNumber = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    WorkPhoneNumber = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    HomePhoneNumber = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    OfficialEmail = table.Column<string>(type: "NVARCHAR(150)", nullable: false),
                    PersonalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeEmail = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    FaxNumber = table.Column<string>(type: "NVARCHAR(150)", nullable: true),
                    Address = table.Column<string>(type: "NVARCHAR(75)", nullable: true),
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
                    table.PrimaryKey("PK_ContactData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactData_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORCID = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    EKB = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    ResearcherId = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    ResearcherGate = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    AcademiaEdu = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
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
                    table.PrimaryKey("PK_IdentificationCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationCards_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewingArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOfArticle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Authority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ReviewingDate = table.Column<DateOnly>(type: "date", nullable: false),
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
                    table.PrimaryKey("PK_ReviewingArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewingArticles_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScientificMissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UniversityOrFaculty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CountryOrCity = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ScientificMissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificMissions_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedIn = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    Instagram = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    PersonalWebsite = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    GoogleScholar = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    Scopus = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    Facebook = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    X = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
                    YouTube = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
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
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedia_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParticipationType = table.Column<int>(type: "int", nullable: false),
                    TrainingProgramName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganizingAuthority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPrograms_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DispatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniversityOrFaculty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CountryOrCity = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfObtainingTheQualification = table.Column<DateOnly>(type: "date", nullable: false),
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
                    table.PrimaryKey("PK_AcademicQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicQualifications_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicQualifications_Lookups_DispatchId",
                        column: x => x.DispatchId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicQualifications_Lookups_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicQualifications_Lookups_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommitteesAndAssociations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfCommitteeOrAssociation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TypeOfCommitteeOrAssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DegreeOfSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_CommitteesAndAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteesAndAssociations_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommitteesAndAssociations_Lookups_DegreeOfSubscriptionId",
                        column: x => x.DegreeOfSubscriptionId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommitteesAndAssociations_Lookups_TypeOfCommitteeOrAssociationId",
                        column: x => x.TypeOfCommitteeOrAssociationId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConferencesAndSeminars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LocalOrInternational = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RoleOfParticipationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizingAuthority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Website = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_ConferencesAndSeminars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminars_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConferencesAndSeminars_Lookups_RoleOfParticipationId",
                        column: x => x.RoleOfParticipationId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobRankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfJobRank = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_JobRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRanks_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRanks_Lookups_JobRankId",
                        column: x => x.JobRankId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParticipationInMagazines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfMagazine = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    WebsiteOfMagazine = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    TypeOfParticipationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ParticipationInMagazines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipationInMagazines_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipationInMagazines_Lookups_TypeOfParticipationId",
                        column: x => x.TypeOfParticipationId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaritalStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "Date", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UniversityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralSpecialization = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AccurateSpecialization = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NameInComposition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompositionTopics = table.Column<string>(type: "NVARCHAR(Max)", nullable: true),
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
                    table.PrimaryKey("PK_PersonalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalData_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_AuthorityId",
                        column: x => x.AuthorityId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalData_Lookups_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalOrInternational = table.Column<int>(type: "int", nullable: false),
                    NameOfProject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TypeOfProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancingAuthority = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Lookups_ParticipationRoleId",
                        column: x => x.ParticipationRoleId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Lookups_TypeOfProjectId",
                        column: x => x.TypeOfProjectId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_CountryOrCity",
                table: "AcademicQualifications",
                column: "CountryOrCity");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_DateOfObtainingTheQualification",
                table: "AcademicQualifications",
                column: "DateOfObtainingTheQualification");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_DispatchId",
                table: "AcademicQualifications",
                column: "DispatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_FacultyMemberId",
                table: "AcademicQualifications",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_GradeId",
                table: "AcademicQualifications",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicQualifications_QualificationId",
                table: "AcademicQualifications",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativePositions_FacultyMemberId",
                table: "AdministrativePositions",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativePositions_Position",
                table: "AdministrativePositions",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativePositions_StartDate",
                table: "AdministrativePositions",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteesAndAssociations_DegreeOfSubscriptionId",
                table: "CommitteesAndAssociations",
                column: "DegreeOfSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteesAndAssociations_FacultyMemberId",
                table: "CommitteesAndAssociations",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteesAndAssociations_NameOfCommitteeOrAssociation",
                table: "CommitteesAndAssociations",
                column: "NameOfCommitteeOrAssociation");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteesAndAssociations_StartDate",
                table: "CommitteesAndAssociations",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteesAndAssociations_TypeOfCommitteeOrAssociationId",
                table: "CommitteesAndAssociations",
                column: "TypeOfCommitteeOrAssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminars_FacultyMemberId",
                table: "ConferencesAndSeminars",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminars_Name",
                table: "ConferencesAndSeminars",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminars_RoleOfParticipationId",
                table: "ConferencesAndSeminars",
                column: "RoleOfParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferencesAndSeminars_StartDate",
                table: "ConferencesAndSeminars",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_ContactData_FacultyMemberId",
                table: "ContactData",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactData_MainPhoneNumber",
                table: "ContactData",
                column: "MainPhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ContactData_OfficialEmail",
                table: "ContactData",
                column: "OfficialEmail");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_Email",
                table: "FacultyMembers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_Name",
                table: "FacultyMembers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_NationalNumber",
                table: "FacultyMembers",
                column: "NationalNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationCards_FacultyMemberId",
                table: "IdentificationCards",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRanks_DateOfJobRank",
                table: "JobRanks",
                column: "DateOfJobRank");

            migrationBuilder.CreateIndex(
                name: "IX_JobRanks_FacultyMemberId",
                table: "JobRanks",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRanks_JobRankId",
                table: "JobRanks",
                column: "JobRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_Type_Key",
                table: "Lookups",
                columns: new[] { "Type", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInMagazines_FacultyMemberId",
                table: "ParticipationInMagazines",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInMagazines_NameOfMagazine",
                table: "ParticipationInMagazines",
                column: "NameOfMagazine");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipationInMagazines_TypeOfParticipationId",
                table: "ParticipationInMagazines",
                column: "TypeOfParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_AuthorityId",
                table: "PersonalData",
                column: "AuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_DepartmentId",
                table: "PersonalData",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_FacultyMemberId",
                table: "PersonalData",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_FieldId",
                table: "PersonalData",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_GenderId",
                table: "PersonalData",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_MaritalStatusId",
                table: "PersonalData",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_TitleId",
                table: "PersonalData",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_UniversityId",
                table: "PersonalData",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FacultyMemberId",
                table: "Projects",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_NameOfProject",
                table: "Projects",
                column: "NameOfProject");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParticipationRoleId",
                table: "Projects",
                column: "ParticipationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StartDate",
                table: "Projects",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TypeOfProjectId",
                table: "Projects",
                column: "TypeOfProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewingArticles_FacultyMemberId",
                table: "ReviewingArticles",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewingArticles_ReviewingDate",
                table: "ReviewingArticles",
                column: "ReviewingDate");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewingArticles_TitleOfArticle",
                table: "ReviewingArticles",
                column: "TitleOfArticle");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificMissions_CountryOrCity",
                table: "ScientificMissions",
                column: "CountryOrCity");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificMissions_FacultyMemberId",
                table: "ScientificMissions",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificMissions_MissionName",
                table: "ScientificMissions",
                column: "MissionName");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificMissions_StartDate",
                table: "ScientificMissions",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_FacultyMemberId",
                table: "SocialMedia",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_FacultyMemberId",
                table: "TrainingPrograms",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_StartDate",
                table: "TrainingPrograms",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingProgramName",
                table: "TrainingPrograms",
                column: "TrainingProgramName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicQualifications");

            migrationBuilder.DropTable(
                name: "AdministrativePositions");

            migrationBuilder.DropTable(
                name: "CommitteesAndAssociations");

            migrationBuilder.DropTable(
                name: "ConferencesAndSeminars");

            migrationBuilder.DropTable(
                name: "ContactData");

            migrationBuilder.DropTable(
                name: "IdentificationCards");

            migrationBuilder.DropTable(
                name: "JobRanks");

            migrationBuilder.DropTable(
                name: "ParticipationInMagazines");

            migrationBuilder.DropTable(
                name: "PersonalData");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ReviewingArticles");

            migrationBuilder.DropTable(
                name: "ScientificMissions");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "Lookups");

            migrationBuilder.DropTable(
                name: "FacultyMembers");
        }
    }
}
