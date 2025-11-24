using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfercesDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555551"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 1, "SmemiarParticipationType", null, null, "المخطط للمؤتمر", "Conference planner", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555552"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 2, "SmemiarParticipationType", null, null, "المراجع الرئيسي", "Main reviewer", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555553"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 3, "SmemiarParticipationType", null, null, "المتحدث", "Speaker", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555554"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 4, "SmemiarParticipationType", null, null, "مقدم البحث", "Research presenter", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 5, "SmemiarParticipationType", null, null, "حضر فقط", "Just attended", 0 },
                    { new Guid("55555555-5555-5555-5555-555555555556"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTCIPATION", 6, "SmemiarParticipationType", null, null, "اخرى", "Other", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555551"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555552"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555553"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555554"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555556"));
        }
    }
}
