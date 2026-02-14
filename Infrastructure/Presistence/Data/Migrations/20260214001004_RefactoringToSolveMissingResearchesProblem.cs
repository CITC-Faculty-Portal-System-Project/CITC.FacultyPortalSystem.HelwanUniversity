using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringToSolveMissingResearchesProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributorOrgansationId",
                table: "ResearchesContributions");

            migrationBuilder.DropColumn(
                name: "MemberScholarId",
                table: "ResearchesContributions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContributorOrgansationId",
                table: "ResearchesContributions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberScholarId",
                table: "ResearchesContributions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
