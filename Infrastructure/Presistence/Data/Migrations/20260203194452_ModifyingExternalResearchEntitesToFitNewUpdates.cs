using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingExternalResearchEntitesToFitNewUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResearchersInterests");

            migrationBuilder.DropTable(
                name: "ResearchesCites");

            migrationBuilder.DropTable(
                name: "ResearchesIndices");

            migrationBuilder.DropIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches");

            migrationBuilder.DropColumn(
                name: "MemberOrcid",
                table: "ResearchContributions");

            migrationBuilder.DropColumn(
                name: "MemberPositionInSearch",
                table: "ResearchContributions");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "ExternalResearches",
                newName: "ResearchLink");

            migrationBuilder.AddColumn<string>(
                name: "ScholarProfileImageURL",
                table: "Researchers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PubDate",
                table: "ExternalResearches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Journal",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoOfPages",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelatedResearchLink",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Volume",
                table: "ExternalResearches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalResearchCites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalResearchId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ExternalResearchCites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalResearchCites_ExternalResearches_ExternalResearchId",
                        column: x => x.ExternalResearchId,
                        principalTable: "ExternalResearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchersCites",
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
                    table.PrimaryKey("PK_ResearchersCites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchersCites_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScientificInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_ScientificInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherInterest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearcherInterest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearcherInterest_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearcherInterest_ScientificInterests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "ScientificInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches",
                columns: new[] { "ResearcherId", "ExternalResearchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalResearchCites_ExternalResearchId",
                table: "ExternalResearchCites",
                column: "ExternalResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherInterest_InterestId",
                table: "ResearcherInterest",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherInterest_ResearcherId_InterestId",
                table: "ResearcherInterest",
                columns: new[] { "ResearcherId", "InterestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersCites_ResearcherId",
                table: "ResearchersCites",
                column: "ResearcherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalResearchCites");

            migrationBuilder.DropTable(
                name: "ResearcherInterest");

            migrationBuilder.DropTable(
                name: "ResearchersCites");

            migrationBuilder.DropTable(
                name: "ScientificInterests");

            migrationBuilder.DropIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches");

            migrationBuilder.DropColumn(
                name: "ScholarProfileImageURL",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "ExternalResearches");

            migrationBuilder.DropColumn(
                name: "Journal",
                table: "ExternalResearches");

            migrationBuilder.DropColumn(
                name: "NoOfPages",
                table: "ExternalResearches");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "ExternalResearches");

            migrationBuilder.DropColumn(
                name: "RelatedResearchLink",
                table: "ExternalResearches");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "ExternalResearches");

            migrationBuilder.RenameColumn(
                name: "ResearchLink",
                table: "ExternalResearches",
                newName: "Link");

            migrationBuilder.AddColumn<string>(
                name: "MemberOrcid",
                table: "ResearchContributions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemberPositionInSearch",
                table: "ResearchContributions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PubDate",
                table: "ExternalResearches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ResearchersInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ResearchesCites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NoOfCitations = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNo = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                name: "ResearchesIndices",
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
                    PlatForm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersResearches_ResearcherId_ExternalResearchId",
                table: "ResearchersResearches",
                columns: new[] { "ResearcherId", "ExternalResearchId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersInterests_Name",
                table: "ResearchersInterests",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersInterests_ResearcherId",
                table: "ResearchersInterests",
                column: "ResearcherId");

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
        }
    }
}
