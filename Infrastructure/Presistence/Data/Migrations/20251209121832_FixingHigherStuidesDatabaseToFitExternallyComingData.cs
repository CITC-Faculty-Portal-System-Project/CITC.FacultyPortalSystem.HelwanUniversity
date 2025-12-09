using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingHigherStuidesDatabaseToFitExternallyComingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorThesesSupervisings");

            migrationBuilder.AddColumn<int>(
                name: "ThesesId",
                table: "Supervisors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_ThesesId",
                table: "Supervisors",
                column: "ThesesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_Theses_ThesesId",
                table: "Supervisors",
                column: "ThesesId",
                principalTable: "Theses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_Theses_ThesesId",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisors_ThesesId",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "ThesesId",
                table: "Supervisors");

            migrationBuilder.CreateTable(
                name: "SupervisorThesesSupervisings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    ThesesId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_SupervisorThesesSupervisings_SupervisorId_ThesesId",
                table: "SupervisorThesesSupervisings",
                columns: new[] { "SupervisorId", "ThesesId" });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorThesesSupervisings_ThesesId",
                table: "SupervisorThesesSupervisings",
                column: "ThesesId");
        }
    }
}
