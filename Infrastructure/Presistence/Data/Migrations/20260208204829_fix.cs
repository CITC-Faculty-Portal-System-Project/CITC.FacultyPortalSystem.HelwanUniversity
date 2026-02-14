using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Researches");

            migrationBuilder.AddColumn<string>(
                name: "ContributorOrgansationId",
                table: "ResearchesContributions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "ResearchesContributions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContributorOrgansationId",
                table: "ResearchesContributions");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "ResearchesContributions");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Researches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
