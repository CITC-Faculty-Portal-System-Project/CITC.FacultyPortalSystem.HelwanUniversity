using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDataDropMenusSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("50505050-5050-5050-5050-505050500001"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 1, "University", null, null, "جامعة القاهرة", "Cairo University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500002"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 2, "University", null, null, "جامعة عين شمس", "Ain Shams University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500003"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 3, "University", null, null, "جامعة حلوان", "Helwan University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500004"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 4, "University", null, null, "جامعة الإسكندرية", "Alexandria University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500005"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 5, "University", null, null, "جامعة المنصورة", "Mansoura University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500006"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 6, "University", null, null, "جامعة طنطا", "Tanta University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500007"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 7, "University", null, null, "جامعة أسيوط", "Assiut University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500008"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 8, "University", null, null, "جامعة الزقازيق", "Zagazig University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500009"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 9, "University", null, null, "جامعة السويس", "Suez University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500010"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 10, "University", null, null, "جامعة بورسعيد", "Port Said University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500011"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 11, "University", null, null, "جامعة الفيوم", "Fayoum University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500012"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "UNIVERSITY", 12, "University", null, null, "جامعة بنها", "Benha University", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500013"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "GENDER", 1, "Gender", null, null, "ذكر", "Male", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500014"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "GENDER", 2, "Gender", null, null, "أنثى", "Female", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500016"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 1, "BirthPlace", null, null, "القاهرة", "Cairo", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500017"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 2, "BirthPlace", null, null, "الجيزة", "Giza", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500018"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 3, "BirthPlace", null, null, "القليوبية", "Qalyubia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500019"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 4, "BirthPlace", null, null, "الإسكندرية", "Alexandria", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500020"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 5, "BirthPlace", null, null, "البحيرة", "Beheira", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500021"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 6, "BirthPlace", null, null, "مطروح", "Matrouh", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500022"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 7, "BirthPlace", null, null, "كفر الشيخ", "Kafr El Sheikh", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500023"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 8, "BirthPlace", null, null, "الدقهلية", "Dakahlia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500024"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 9, "BirthPlace", null, null, "دمياط", "Damietta", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500025"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 10, "BirthPlace", null, null, "الشرقية", "Sharqia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500026"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 11, "BirthPlace", null, null, "الغربية", "Gharbia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500027"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 12, "BirthPlace", null, null, "المنوفية", "Monufia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500028"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 13, "BirthPlace", null, null, "المنيا", "Minya", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500029"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 14, "BirthPlace", null, null, "بني سويف", "Beni Suef", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500030"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 15, "BirthPlace", null, null, "الفيوم", "Fayoum", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500031"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 16, "BirthPlace", null, null, "أسيوط", "Assiut", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500032"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 17, "BirthPlace", null, null, "سوهاج", "Sohag", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500033"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 18, "BirthPlace", null, null, "قنا", "Qena", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500034"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 19, "BirthPlace", null, null, "الأقصر", "Luxor", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500035"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 20, "BirthPlace", null, null, "أسوان", "Aswan", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500036"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 21, "BirthPlace", null, null, "البحر الأحمر", "Red Sea", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500037"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 22, "BirthPlace", null, null, "السويس", "Suez", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500038"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 23, "BirthPlace", null, null, "الإسماعيلية", "Ismailia", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500039"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 24, "BirthPlace", null, null, "بورسعيد", "Port Said", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500040"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 25, "BirthPlace", null, null, "شمال سيناء", "North Sinai", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500041"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 26, "BirthPlace", null, null, "جنوب سيناء", "South Sinai", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500042"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BIRTH_PLACE", 27, "BirthPlace", null, null, "الوادي الجديد", "New Valley", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500043"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SOCIAL_STATUS", 1, "SocialStatus", null, null, "أعزب", "Single", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500044"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SOCIAL_STATUS", 2, "SocialStatus", null, null, "متزوج", "Married", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500045"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SOCIAL_STATUS", 3, "SocialStatus", null, null, "مطلق", "Divorced", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500046"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SOCIAL_STATUS", 4, "SocialStatus", null, null, "أرمل", "Widowed", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500047"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 1, "Title", null, null, "د.", "Dr.", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500048"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 2, "Title", null, null, "أ.د.", "Prof. Dr.", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500049"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 3, "Title", null, null, "أ.", "Prof.", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500050"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 4, "Title", null, null, "م.د.", "Assistant Lecturer", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500051"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 5, "Title", null, null, "م.", "Eng.", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500052"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "TITLE", 6, "Title", null, null, "بدون لقب", "No Title", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500001"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500002"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500003"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500004"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500005"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500006"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500007"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500008"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500009"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500010"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500011"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500012"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500013"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500014"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500016"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500017"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500018"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500019"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500020"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500021"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500022"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500023"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500024"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500025"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500026"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500027"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500028"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500029"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500030"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500031"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500032"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500033"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500034"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500035"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500036"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500037"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500038"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500039"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500040"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500041"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500042"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500043"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500044"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500045"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500046"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500047"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500048"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500049"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500050"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500051"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500052"));
        }
    }
}
