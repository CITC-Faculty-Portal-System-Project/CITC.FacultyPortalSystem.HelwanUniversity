using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingThesesSupervisingToFitConfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches");

            migrationBuilder.DropTable(
                name: "Supervisors");

            migrationBuilder.CreateTable(
                name: "ThesisComittees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    JobLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Authority = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_ThesisComittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThesisComittees_FacultyMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "FacultyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThesisComittees_Lookups_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThesisComittees_Theses_ThesesId",
                        column: x => x.ThesesId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThesisComittees_JobLevelId",
                table: "ThesisComittees",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisComittees_MemberId",
                table: "ThesisComittees",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisComittees_Name",
                table: "ThesisComittees",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisComittees_ThesesId",
                table: "ThesisComittees",
                column: "ThesesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches");

            migrationBuilder.DropTable(
                name: "ThesisComittees");

            migrationBuilder.CreateTable(
                name: "Supervisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThesesId = table.Column<int>(type: "int", nullable: false),
                    Authority = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Supervisors_Theses_ThesesId",
                        column: x => x.ThesesId,
                        principalTable: "Theses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_JobLevelId",
                table: "Supervisors",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_Name",
                table: "Supervisors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_ThesesId",
                table: "Supervisors",
                column: "ThesesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Researches_Theses_ThesisId",
                table: "Researches",
                column: "ThesisId",
                principalTable: "Theses",
                principalColumn: "Id");
        }
    }
}
