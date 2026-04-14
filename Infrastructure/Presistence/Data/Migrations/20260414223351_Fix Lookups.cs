using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "ValueEn",
                value: "Teaching Assistant");

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500047"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "دكتور.", "Doctor of Philosophy." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500048"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "أستاذ دكتور.", "Professor." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500049"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "أستاذ.", "Professor." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500050"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "مدرس دكتور.", "Assistant Professor" });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500051"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "مهندس.", "Engineer." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"),
                column: "ValueEn",
                value: "Demonstrator");

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500047"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "د.", "Dr." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500048"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "أ.د.", "Prof. Dr." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500049"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "أ.", "Prof." });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500050"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "م.د.", "Assistant Lecturer" });

            migrationBuilder.UpdateData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500051"),
                columns: new[] { "ValueAr", "ValueEn" },
                values: new object[] { "م.", "Eng." });
        }
    }
}
