using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMockUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lookups_Type_Key",
                table: "Lookups");

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BACHELOR", 1, "AcademicQualification", null, null, "ليسانس", "Bachelor's degree", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "BACHELOR", 2, "AcademicQualification", null, null, "بكالوريوس", "Bachelor's", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 3, "AcademicQualification", null, null, "دبلوم الدراسات العليا", "Postgraduate Diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 4, "AcademicQualification", null, null, "ماجستير", "Master Degree", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 5, "AcademicQualification", null, null, "كورسات مكافئة للماجستير", "Courses equivalent to a master's degree", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111116"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 6, "AcademicQualification", null, null, "الدكتوراه", "PHD", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111117"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 7, "AcademicQualification", null, null, "دكتوراة العلوم", "Ph.D. of Science", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111118"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 8, "AcademicQualification", null, null, "دكتوراة العلوم", "Ph.D. of Science", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111119"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 9, "AcademicQualification", null, null, "العالمية", "Global", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111120"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 10, "AcademicQualification", null, null, "الاجازة العالية", "Higher Degree Qualification", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111121"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 11, "AcademicQualification", null, null, "الزمالة", "Fellowship", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111122"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 12, "AcademicQualification", null, null, "دبلوم عام", "General diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111123"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 13, "AcademicQualification", null, null, "دبلوم خاص", "Special diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111124"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 14, "AcademicQualification", null, null, "دبلوم مهني", "Professional diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111125"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 15, "AcademicQualification", null, null, "دبلوم تفرغ", "Sabbatical diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111126"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 16, "AcademicQualification", null, null, "دبلوم تأهيلي", "Qualifying diploma", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111127"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HIGHER STUDIES", 17, "AcademicQualification", null, null, "الكانديدات", "Candidae", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EXCELLENT", 1, "AcademicGrade", null, null, "ممتاز مع مرتبة الشرف", "Excellent with honors", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EXCELLENT", 2, "AcademicGrade", null, null, "ممتاز", "Excellent", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "VERY GOOD", 3, "AcademicGrade", null, null, "جيد جدا مع مرتبة الشرف", "Very good with honors", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "VERY GOOD", 4, "AcademicGrade", null, null, "جيد جدا", "Very good", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "GOOD", 5, "AcademicGrade", null, null, "جيد", "Good", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222226"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "ACCEPTABLE", 6, "AcademicGrade", null, null, "مقبول", "Acceptable", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222227"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "FAIL", 7, "AcademicGrade", null, null, "راسب", "Fail", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333331"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MISSION", 1, "Dispatch", null, null, "بعثة داخلية", "Internal mission", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333332"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MISSION", 2, "Dispatch", null, null, "بعثة خارجية", "Foreign mission", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SUPERVISION", 3, "Dispatch", null, null, "اشراف مشترك", "Joint supervision", 0 },
                    { new Guid("33333333-3333-3333-3333-333333333334"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "SCHOLARSHIP", 4, "Dispatch", null, null, "منحة شخصية", "Personal scholarship", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444441"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 1, "EmploymentDegrees", null, null, "معيد", "Demonstrator", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444442"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 2, "EmploymentDegrees", null, null, "مدرس مساعد", "Assistant teacher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444443"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 3, "EmploymentDegrees", null, null, "مدرس", "Teacher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 4, "EmploymentDegrees", null, null, "استاذ مساعد", "Assistant professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444445"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 5, "EmploymentDegrees", null, null, "استاذ", "Professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444446"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 6, "EmploymentDegrees", null, null, "استاذ متفرغ", "Full-time professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444447"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 7, "EmploymentDegrees", null, null, "استاذ غير متفرغ", "Part-time professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444448"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 8, "EmploymentDegrees", null, null, "زميل", "Peer", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444449"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 9, "EmploymentDegrees", null, null, "استشاري مساعد", "Assistant consultant", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444450"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 10, "EmploymentDegrees", null, null, "استشاري", "Consultative", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444451"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 11, "EmploymentDegrees", null, null, "استاذ مساعد متفرغ", "Full-time Assistant Professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444452"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 12, "EmploymentDegrees", null, null, "مدرس متفرغ", "Full time teacher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444453"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 13, "EmploymentDegrees", null, null, "استاذ مساعد لقب علمي", "Assistant Professor (academic title)", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444454"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 14, "EmploymentDegrees", null, null, "استاذ لقب علمي", "Professor is a scientific title", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444455"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 15, "EmploymentDegrees", null, null, "مساعد باحث", "Research assistant", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444456"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 16, "EmploymentDegrees", null, null, "باحث مساعد", "Assistant researcher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444457"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 17, "EmploymentDegrees", null, null, "باحث", "Researcher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444458"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 18, "EmploymentDegrees", null, null, "باحث اول", "Senior researcher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444459"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 19, "EmploymentDegrees", null, null, "رئيس بحوث", "Head of Research", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444460"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 20, "EmploymentDegrees", null, null, "استاذ مساعد غير متفرغ", "Part-time Assistant Professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444461"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 21, "EmploymentDegrees", null, null, "زميل متفرغ", "Full-time colleague", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444462"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 22, "EmploymentDegrees", null, null, "مدرس غير متفرغ", "Part-time teacher", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444463"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 23, "EmploymentDegrees", null, null, "استاذ مشارك", "Associate Professor", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444464"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "EMPLOYEMENT", 24, "EmploymentDegrees", null, null, "اخرى", "Other", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111120"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111121"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111122"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111123"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111124"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111125"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111126"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111127"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222221"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222225"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222226"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222227"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333331"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333332"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444447"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444448"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444449"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444450"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444451"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444452"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444453"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444454"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444455"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444456"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444457"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444458"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444459"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444460"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444461"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444462"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444463"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444464"));

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_Type_Key",
                table: "Lookups",
                columns: new[] { "Type", "Key" },
                unique: true);
        }
    }
}
