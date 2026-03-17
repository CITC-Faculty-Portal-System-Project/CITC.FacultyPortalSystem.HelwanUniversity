using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCoAuthorsToResearcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests");

            migrationBuilder.DropIndex(
                name: "IX_ResearchersInterests_ResearcherId_InterestId",
                table: "ResearchersInterests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResearchersInterests");

            migrationBuilder.AlterColumn<int>(
                name: "PubYear",
                table: "Researches",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests",
                columns: new[] { "ResearcherId", "InterestId" });

            migrationBuilder.CreateTable(
                name: "CoAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScholarProfileLink = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScholarProfileImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationalDomain = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_CoAuthor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearcherCoAuthor",
                columns: table => new
                {
                    ResearcherId = table.Column<int>(type: "int", nullable: false),
                    CoAuthorId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ResearcherCoAuthor", x => new { x.ResearcherId, x.CoAuthorId });
                    table.ForeignKey(
                        name: "FK_ResearcherCoAuthor_CoAuthor_CoAuthorId",
                        column: x => x.CoAuthorId,
                        principalTable: "CoAuthor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResearcherCoAuthor_ResearchersProfiles_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "ResearchersProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Researches_DOI",
                table: "Researches",
                column: "DOI",
                unique: true,
                filter: "[DOI] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CoAuthor_AcademicName",
                table: "CoAuthor",
                column: "AcademicName");

            migrationBuilder.CreateIndex(
                name: "IX_CoAuthor_OrganisationalDomain",
                table: "CoAuthor",
                column: "OrganisationalDomain");

            migrationBuilder.CreateIndex(
                name: "IX_CoAuthor_ScholarProfileLink",
                table: "CoAuthor",
                column: "ScholarProfileLink");

            migrationBuilder.CreateIndex(
                name: "IX_ResearcherCoAuthor_CoAuthorId",
                table: "ResearcherCoAuthor",
                column: "CoAuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResearcherCoAuthor");

            migrationBuilder.DropTable(
                name: "CoAuthor");

            migrationBuilder.DropIndex(
                name: "IX_Researches_DOI",
                table: "Researches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests");

            migrationBuilder.AlterColumn<string>(
                name: "PubYear",
                table: "Researches",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ResearchersInterests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResearchersInterests",
                table: "ResearchersInterests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchersInterests_ResearcherId_InterestId",
                table: "ResearchersInterests",
                columns: new[] { "ResearcherId", "InterestId" },
                unique: true);
        }
    }
}
