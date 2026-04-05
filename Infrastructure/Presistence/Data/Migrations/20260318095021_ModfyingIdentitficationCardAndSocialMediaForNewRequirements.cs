using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModfyingIdentitficationCardAndSocialMediaForNewRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleScholar",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "Scopus",
                table: "SocialMedia");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PersonalData",
                newName: "NameEn");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "PersonalData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GoogleScholar",
                table: "IdentificationCards",
                type: "NVARCHAR(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scopus",
                table: "IdentificationCards",
                type: "NVARCHAR(Max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "GoogleScholar",
                table: "IdentificationCards");

            migrationBuilder.DropColumn(
                name: "Scopus",
                table: "IdentificationCards");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "PersonalData",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "GoogleScholar",
                table: "SocialMedia",
                type: "NVARCHAR(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scopus",
                table: "SocialMedia",
                type: "NVARCHAR(Max)",
                nullable: true);
        }
    }
}
