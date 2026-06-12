using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingFacultyMemberRelationShipWithCV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SavedCVPreferences_FacultyMemberId",
                table: "SavedCVPreferences",
                column: "FacultyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedCVPreferences_FacultyMembers_FacultyMemberId",
                table: "SavedCVPreferences",
                column: "FacultyMemberId",
                principalTable: "FacultyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedCVPreferences_FacultyMembers_FacultyMemberId",
                table: "SavedCVPreferences");

            migrationBuilder.DropIndex(
                name: "IX_SavedCVPreferences_FacultyMemberId",
                table: "SavedCVPreferences");
        }
    }
}
