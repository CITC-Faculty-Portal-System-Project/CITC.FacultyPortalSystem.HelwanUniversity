using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class CompostionsDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("20202020-2020-2020-2020-202020202020"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 1, "AuthorRole", null, null, "مؤلف", "Author", 0 },
                    { new Guid("20202020-2020-2020-2020-202020202021"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 2, "AuthorRole", null, null, "مترجم", "Trasnlator", 0 },
                    { new Guid("20202020-2020-2020-2020-202020202022"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 3, "AuthorRole", null, null, "مراجع", "Revisor", 0 },
                    { new Guid("20202020-2020-2020-2020-202020202023"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 4, "AuthorRole", null, null, "مترجم/مراجع", "Translator/Revisor", 0 },
                    { new Guid("20202020-2020-2020-2020-202020202024"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 5, "AuthorRole", null, null, "محرر كتاب", "Book editor", 0 },
                    { new Guid("20202020-2020-2020-2020-202020202025"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "AUTHORROLE", 6, "AuthorRole", null, null, "مؤلف فصل", "Chapter author", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202021"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202022"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202023"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202024"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202025"));
        }
    }
}
