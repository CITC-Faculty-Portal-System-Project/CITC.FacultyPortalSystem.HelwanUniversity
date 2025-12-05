using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingResearchesAndHigherStudiesModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOI = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PubYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PubDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoOfCititations = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_ExternalResearches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalSystemResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOI = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkWithOtherResearch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ResearchLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MagazineOrConference = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PublisherType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchDerivedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
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
                    table.PrimaryKey("PK_InternalSystemResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalSystemResearches_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalSystemResearchesContributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsFromHelwanUniversity = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsTheMajorResearcher = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_InternalSystemResearchesContributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Researchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORCID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ScholarProfileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AcademicName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OrganisationalDomain = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrganisationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalNumberOfCitiations = table.Column<int>(type: "int", nullable: false),
                    NumberOfCitiationsInLastFiveYears = table.Column<int>(type: "int", nullable: false),
                    Hindex = table.Column<int>(type: "int", nullable: false),
                    HindexInLastFiveYears = table.Column<int>(type: "int", nullable: false),
                    I10index = table.Column<int>(type: "int", nullable: false),
                    I10index5y = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Researchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Researchers_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supervisings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FacultyMemberRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SupervisionFormationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DiscussionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GrantingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UniversityOrFaculty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
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
                    table.PrimaryKey("PK_Supervisings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisings_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supervisings_Lookups_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    JobLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Authority = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_Supervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisors_Lookups_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Theses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InternalGradeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SupervisionConfirmationDate = table.Column<DateOnly>(type: "date", nullable: true),
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
                    table.PrimaryKey("PK_Theses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theses_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Theses_Lookups_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchContributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberOrcid = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MemberPositionInSearch = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MemberAcademicName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchContributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchContributions_ExternalResearches_ExternalResearchId",
                        column: x => x.ExternalResearchId,
                        principalTable: "ExternalResearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchesIndices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatForm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchesIndices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchesIndices_ExternalResearches_ExternalResearchId",
                        column: x => x.ExternalResearchId,
                        principalTable: "ExternalResearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalSystemResearchesContributorsResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalSystemResearchId = table.Column<int>(type: "int", nullable: false),
                    InternalSystemResearchContributorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InternalSystemResearchesContributorsResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalSystemResearchesContributorsResearches_InternalSystemResearchesContributors_InternalSystemResearchContributorId",
                        column: x => x.InternalSystemResearchContributorId,
                        principalTable: "InternalSystemResearchesContributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalSystemResearchesContributorsResearches_InternalSystemResearches_InternalSystemResearchId",
                        column: x => x.InternalSystemResearchId,
                        principalTable: "InternalSystemResearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchersInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchersInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchersInterests_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchersResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchersResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchersResearches_ExternalResearches_ExternalResearchId",
                        column: x => x.ExternalResearchId,
                        principalTable: "ExternalResearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearchersResearches_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchesCites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NoOfCitations = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchesCites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchesCites_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorThesesSupervisings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    ThesesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SupervisorThesesSupervisings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorThesesSupervisings_Supervisors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorThesesSupervisings_Theses_ThesesId",
                        column: x => x.ThesesId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalSystemResearches_FacultyMemberId",
                table: "InternalSystemResearches",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalSystemResearches_Year",
                table: "InternalSystemResearches",
                column: "Year");

            migrationBuilder.CreateIndex(
                name: "IX_InternalSystemResearchesContributors_Name",
                table: "InternalSystemResearchesContributors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InternalSystemResearchesContributorsResearches_InternalSystemResearchContributorId_InternalSystemResearchId",
                table: "InternalSystemResearchesContributorsResearches",
                columns: new[] { "InternalSystemResearchContributorId", "InternalSystemResearchId" });

            migrationBuilder.CreateIndex(
                name: "IX_InternalSystemResearchesContributorsResearches_InternalSystemResearchId",
                table: "InternalSystemResearchesContributorsResearches",
                column: "InternalSystemResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchContributions_ExternalResearchId",
                table: "ResearchContributions",
                column: "ExternalResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchContributions_MemberAcademicName",
                table: "ResearchContributions",
                column: "MemberAcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_AcademicName",
                table: "Researchers",
                column: "AcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_FacultyMemberId",
                table: "Researchers",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersInterests_Name",
                table: "ResearchersInterests",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersInterests_ResearcherId",
                table: "ResearchersInterests",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ExternalResearchId",
                table: "ResearchersResearches",
                column: "ExternalResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches",
                columns: new[] { "ResearcherId", "ExternalResearchId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchesCites_ResearcherId",
                table: "ResearchesCites",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchesIndices_ExternalResearchId",
                table: "ResearchesIndices",
                column: "ExternalResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchesIndices_PlatForm",
                table: "ResearchesIndices",
                column: "PlatForm");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_FacultyMemberId",
                table: "Supervisings",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_GradeId",
                table: "Supervisings",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_StudentName",
                table: "Supervisings",
                column: "StudentName");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisings_Title",
                table: "Supervisings",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_JobLevelId",
                table: "Supervisors",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_Name",
                table: "Supervisors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorThesesSupervisings_SupervisorId_ThesesId",
                table: "SupervisorThesesSupervisings",
                columns: new[] { "SupervisorId", "ThesesId" });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorThesesSupervisings_ThesesId",
                table: "SupervisorThesesSupervisings",
                column: "ThesesId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_FacultyMemberId",
                table: "Theses",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_GradeId",
                table: "Theses",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_Title",
                table: "Theses",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalSystemResearchesContributorsResearches");

            migrationBuilder.DropTable(
                name: "ResearchContributions");

            migrationBuilder.DropTable(
                name: "ResearchersInterests");

            migrationBuilder.DropTable(
                name: "ResearchersResearches");

            migrationBuilder.DropTable(
                name: "ResearchesCites");

            migrationBuilder.DropTable(
                name: "ResearchesIndices");

            migrationBuilder.DropTable(
                name: "Supervisings");

            migrationBuilder.DropTable(
                name: "SupervisorThesesSupervisings");

            migrationBuilder.DropTable(
                name: "InternalSystemResearchesContributors");

            migrationBuilder.DropTable(
                name: "InternalSystemResearches");

            migrationBuilder.DropTable(
                name: "Researchers");

            migrationBuilder.DropTable(
                name: "ExternalResearches");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.DropTable(
                name: "Theses");
        }
    }
}
