using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class MergingInternalAndExternalResearchesAsReturnedDataAreTheSame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchContributions_ExternalResearches_ExternalResearchId",
                table: "ResearchContributions");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearcherInterest_Researchers_ResearcherId",
                table: "ResearcherInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearcherInterest_ScientificInterests_InterestId",
                table: "ResearcherInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchersCites_Researchers_ResearcherId",
                table: "ResearchersCites");

            migrationBuilder.DropTable(
                name: "ExternalResearchCites");

            migrationBuilder.DropTable(
                name: "InternalSystemResearchesContributorsResearches");

            migrationBuilder.DropTable(
                name: "ResearchersResearches");

            migrationBuilder.DropTable(
                name: "InternalSystemResearchesContributors");

            migrationBuilder.DropTable(
                name: "InternalSystemResearches");

            migrationBuilder.DropTable(
                name: "ExternalResearches");

            migrationBuilder.DropTable(
                name: "Researchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearcherInterest",
                table: "ResearcherInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearchContributions",
                table: "ResearchContributions");

            migrationBuilder.DropIndex(
                name: "IX_ResearchContributions_MemberAcademicName",
                table: "ResearchContributions");

            migrationBuilder.RenameTable(
                name: "ResearcherInterest",
                newName: "ResearchersInterests");

            migrationBuilder.RenameTable(
                name: "ResearchContributions",
                newName: "ResearchesContributions");

            migrationBuilder.RenameIndex(
                name: "IX_ResearcherInterest_ResearcherId_InterestId",
                table: "ResearchersInterests",
                newName: "IX_ResearchersInterests_ResearcherId_InterestId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearcherInterest_InterestId",
                table: "ResearchersInterests",
                newName: "IX_ResearchersInterests_InterestId");

            migrationBuilder.RenameColumn(
                name: "ExternalResearchId",
                table: "ResearchesContributions",
                newName: "ResearchId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchContributions_ExternalResearchId",
                table: "ResearchesContributions",
                newName: "IX_ResearchesContributions_ResearchId");

            migrationBuilder.AddColumn<Guid>(
                name: "ContributorId",
                table: "ResearchesContributions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContributorType",
                table: "ResearchesContributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTheMajorResearcher",
                table: "ResearchesContributions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearchesContributions",
                table: "ResearchesContributions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ResearchersProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORCID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ScholarProfileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ScholarProfileImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ResearchersProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchersProfiles_FacultyMembers_FacultyMemberId",
                        column: x => x.FacultyMemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Researches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOI = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    RelatedResearchLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ResearchLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JournalOrConfernce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfPages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PubYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchDerivedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_Researches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchsCites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearchId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NumberOfCites = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearchsCites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchsCites_Researches_ResearchId",
                        column: x => x.ResearchId,
                        principalTable: "Researches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchesContributions_ContributorId_ResearchId",
                table: "ResearchesContributions",
                columns: new[] { "ContributorId", "ResearchId" },
                unique: true,
                filter: "[ContributorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersProfiles_AcademicName",
                table: "ResearchersProfiles",
                column: "AcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersProfiles_FacultyMemberId",
                table: "ResearchersProfiles",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchsCites_ResearchId",
                table: "ResearchsCites",
                column: "ResearchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchersCites_ResearchersProfiles_ResearcherId",
                table: "ResearchersCites",
                column: "ResearcherId",
                principalTable: "ResearchersProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchersInterests_ResearchersProfiles_ResearcherId",
                table: "ResearchersInterests",
                column: "ResearcherId",
                principalTable: "ResearchersProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchersInterests_ScientificInterests_InterestId",
                table: "ResearchersInterests",
                column: "InterestId",
                principalTable: "ScientificInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchesContributions_FacultyMembers_ContributorId",
                table: "ResearchesContributions",
                column: "ContributorId",
                principalTable: "FacultyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchesContributions_Researches_ResearchId",
                table: "ResearchesContributions",
                column: "ResearchId",
                principalTable: "Researches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchersCites_ResearchersProfiles_ResearcherId",
                table: "ResearchersCites");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchersInterests_ResearchersProfiles_ResearcherId",
                table: "ResearchersInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchersInterests_ScientificInterests_InterestId",
                table: "ResearchersInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchesContributions_FacultyMembers_ContributorId",
                table: "ResearchesContributions");

            migrationBuilder.DropForeignKey(
                name: "FK_ResearchesContributions_Researches_ResearchId",
                table: "ResearchesContributions");

            migrationBuilder.DropTable(
                name: "ResearchersProfiles");

            migrationBuilder.DropTable(
                name: "ResearchsCites");

            migrationBuilder.DropTable(
                name: "Researches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearchesContributions",
                table: "ResearchesContributions");

            migrationBuilder.DropIndex(
                name: "IX_ResearchesContributions_ContributorId_ResearchId",
                table: "ResearchesContributions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests");

            migrationBuilder.DropColumn(
                name: "ContributorId",
                table: "ResearchesContributions");

            migrationBuilder.DropColumn(
                name: "ContributorType",
                table: "ResearchesContributions");

            migrationBuilder.DropColumn(
                name: "IsTheMajorResearcher",
                table: "ResearchesContributions");

            migrationBuilder.RenameTable(
                name: "ResearchesContributions",
                newName: "ResearchContributions");

            migrationBuilder.RenameTable(
                name: "ResearchersInterests",
                newName: "ResearcherInterest");

            migrationBuilder.RenameColumn(
                name: "ResearchId",
                table: "ResearchContributions",
                newName: "ExternalResearchId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchesContributions_ResearchId",
                table: "ResearchContributions",
                newName: "IX_ResearchContributions_ExternalResearchId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchersInterests_ResearcherId_InterestId",
                table: "ResearcherInterest",
                newName: "IX_ResearcherInterest_ResearcherId_InterestId");

            migrationBuilder.RenameIndex(
                name: "IX_ResearchersInterests_InterestId",
                table: "ResearcherInterest",
                newName: "IX_ResearcherInterest_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearchContributions",
                table: "ResearchContributions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearcherInterest",
                table: "ResearcherInterest",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExternalResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOI = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Journal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfCititations = table.Column<int>(type: "int", nullable: false),
                    NoOfPages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PubDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PubYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RelatedResearchLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOI = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkWithOtherResearch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MagazineOrConference = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    PublicationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PublisherType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchDerivedFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResearchLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFromHelwanUniversity = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsTheMajorResearcher = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    FacultyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hindex = table.Column<int>(type: "int", nullable: false),
                    HindexInLastFiveYears = table.Column<int>(type: "int", nullable: false),
                    I10index = table.Column<int>(type: "int", nullable: false),
                    I10index5y = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberOfCitiationsInLastFiveYears = table.Column<int>(type: "int", nullable: false),
                    ORCID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrganisationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganisationalDomain = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ScholarProfileImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScholarProfileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TotalNumberOfCitiations = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ExternalResearchCites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfCites = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalResearchCites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalResearchCites_ExternalResearches_ExternalResearchId",
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
                    InternalSystemResearchContributorId = table.Column<int>(type: "int", nullable: false),
                    InternalSystemResearchId = table.Column<int>(type: "int", nullable: false),
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
                name: "ResearchersResearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_ResearchContributions_MemberAcademicName",
                table: "ResearchContributions",
                column: "MemberAcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalResearchCites_ExternalResearchId",
                table: "ExternalResearchCites",
                column: "ExternalResearchId");

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
                name: "IX_Researchers_AcademicName",
                table: "Researchers",
                column: "AcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_FacultyMemberId",
                table: "Researchers",
                column: "FacultyMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ExternalResearchId",
                table: "ResearchersResearches",
                column: "ExternalResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches",
                columns: new[] { "ResearcherId", "ExternalResearchId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchContributions_ExternalResearches_ExternalResearchId",
                table: "ResearchContributions",
                column: "ExternalResearchId",
                principalTable: "ExternalResearches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearcherInterest_Researchers_ResearcherId",
                table: "ResearcherInterest",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearcherInterest_ScientificInterests_InterestId",
                table: "ResearcherInterest",
                column: "InterestId",
                principalTable: "ScientificInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchersCites_Researchers_ResearcherId",
                table: "ResearchersCites",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
