using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProjectsAndComiteesDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 1, "MagazineParticipationRole", null, null, "رئيس تحرير", "Editor-in-Chief", 0 },
                    { new Guid("10101010-1010-1010-1010-101010101011"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 2, "MagazineParticipationRole", null, null, "مدير تحرير", "Editorial Director", 0 },
                    { new Guid("10101010-1010-1010-1010-101010101012"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 3, "MagazineParticipationRole", null, null, "نائب تحرير", "Deputy editor", 0 },
                    { new Guid("10101010-1010-1010-1010-101010101013"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 4, "MagazineParticipationRole", null, null, "عضو", "Member", 0 },
                    { new Guid("10101010-1010-1010-1010-101010101014"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 5, "MagazineParticipationRole", null, null, "محرر", "Editor", 0 },
                    { new Guid("10101010-1010-1010-1010-101010101015"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "MAGAZINEPARTICIPATIONROLE", 6, "MagazineParticipationRole", null, null, "محكم", "ReFree", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666661"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 1, "ComiteeParticipationDegree", null, null, "رئيس مجلس الادارة", "Chairman of the Board of Directors", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 2, "ComiteeParticipationDegree", null, null, "رئيس اللجنة", "Chairman of the Committee", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666663"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 3, "ComiteeParticipationDegree", null, null, "مدير", "Boss", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666664"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 4, "ComiteeParticipationDegree", null, null, "منسق", "Coordinator", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666665"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 5, "ComiteeParticipationDegree", null, null, "مقرر", "Decidor", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 6, "ComiteeParticipationDegree", null, null, "مشرف", "Supervisor", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666667"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 7, "ComiteeParticipationDegree", null, null, "استشاري", "Consultative", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666668"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 8, "ComiteeParticipationDegree", null, null, "سكرتير", "Secretary", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666669"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 9, "ComiteeParticipationDegree", null, null, "مراجع", "Revisor", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666670"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 10, "ComiteeParticipationDegree", null, null, "عضو مجلس ادارة", "Member of the Board of Directors", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666671"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 11, "ComiteeParticipationDegree", null, null, "عضو مجلس تحرير", "Editorial board member", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666672"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 12, "ComiteeParticipationDegree", null, null, "عضو مؤسس", "Founding member", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666673"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 13, "ComiteeParticipationDegree", null, null, "عضو عامل", "Active member", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666674"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 14, "ComiteeParticipationDegree", null, null, "عضو", "Member", 0 },
                    { new Guid("66666666-6666-6666-6666-666666666675"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PARTICIPATIONTYPES", 15, "ComiteeParticipationDegree", null, null, "متحكم", "Controller", 0 },
                    { new Guid("77777777-7777-7777-7777-777777777771"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "COMITEETYPES", 1, "TypeofComitee", null, null, "لجان علمية", "Scientific committees", 0 },
                    { new Guid("77777777-7777-7777-7777-777777777772"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "COMITEETYPES", 2, "TypeofComitee", null, null, "جمعيات", "Associations", 0 },
                    { new Guid("77777777-7777-7777-7777-777777777773"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "COMITEETYPES", 3, "TypeofComitee", null, null, "لجان", "Committees", 0 },
                    { new Guid("77777777-7777-7777-7777-777777777774"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "COMITEETYPES", 4, "TypeofComitee", null, null, "اخرى", "Other", 0 },
                    { new Guid("88888888-8888-8888-8888-888888888881"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTTYPES", 1, "ProjectType", null, null, "بحثي", "Research", 0 },
                    { new Guid("88888888-8888-8888-8888-888888888882"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTTYPES", 2, "ProjectType", null, null, "هندسي", "Geometric", 0 },
                    { new Guid("88888888-8888-8888-8888-888888888883"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTTYPES", 3, "ProjectType", null, null, "جودة", "Quality", 0 },
                    { new Guid("88888888-8888-8888-8888-888888888884"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTTYPES", 4, "ProjectType", null, null, "خارجي", "External", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999091"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 1, "ProjectRole", null, null, "مدير مشروع", "Project manager", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999092"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 2, "ProjectRole", null, null, "مدير تنفيذي", "Executive Director", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999093"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 3, "ProjectRole", null, null, "نائب مدير تنفيذي", "Deputy Executive Director", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999094"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 4, "ProjectRole", null, null, "باحث رئيسي", "Principal researcher", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999095"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 5, "ProjectRole", null, null, "باحث مشارك", "Contributer researcher", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999096"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 6, "ProjectRole", null, null, "مستشار", "Consultant", 0 },
                    { new Guid("99999999-9999-9999-9999-999999999097"), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "PROJECTROLES", 7, "ProjectRole", null, null, "متحكم", "Controller", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101011"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101012"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101013"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101014"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101015"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666661"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666662"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666663"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666664"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666665"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666667"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666668"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666669"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666670"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666671"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666672"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666673"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666674"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666675"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777774"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888881"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888882"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888883"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888884"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999091"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999092"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999093"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999094"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999095"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999096"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999097"));
        }
    }
}
