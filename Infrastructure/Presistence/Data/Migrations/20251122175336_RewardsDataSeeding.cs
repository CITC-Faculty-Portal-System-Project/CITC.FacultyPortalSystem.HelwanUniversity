using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RewardsDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("30303030-3030-3030-3030-303030303030"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 1, "Rewards", null, null, "جائزة الدولة التقديرية", "State Appreciation Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303031"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 2, "Rewards", null, null, "جائزة الدولة للتفوق العلم", "State Award for Scientific Excellence", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303032"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 3, "Rewards", null, null, "جائزة الدولة التشجيعية", "State Incentive Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303033"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 4, "Rewards", null, null, "جائزة النيل التشجيعية", "Nile Encouragement Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303034"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 5, "Rewards", null, null, "جائزة الجامعة التقديرية", "University Appreciation Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303035"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 6, "Rewards", null, null, "جائزة المنصورة الطبية", "Mansoura Medical Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303036"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 7, "Rewards", null, null, "جائزة احسن رسالة دكتوراه", "Best PhD Dissertation Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303037"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 8, "Rewards", null, null, "جائزة احسن رسالة ماجستير", "Best Master's Thesis Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303038"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 9, "Rewards", null, null, "جائزة عبد الحميد شومان", "Abdul Hameed Shoman Award", 0 },
                    { new Guid("30303030-3030-3030-3030-303030303039"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "REWARDS", 10, "Rewards", null, null, "اخرى", "Other", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303030"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303031"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303032"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303033"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303034"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303035"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303036"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303037"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303038"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("30303030-3030-3030-3030-303030303039"));
        }
    }
}
