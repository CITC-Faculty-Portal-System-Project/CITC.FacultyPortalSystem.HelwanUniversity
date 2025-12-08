using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDataDropMenusSeedingAddingStudyFieldsAndSpecialisations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "Key", "SortOrder", "Type", "UpdatedAt", "UpdatedBy", "ValueAr", "ValueEn", "VersionNo" },
                values: new object[,]
                {
                    { new Guid("50505050-5050-5050-5050-505050500060"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 1, "Faculty", null, null, "كلية الهندسة - حلوان", "Faculty of Engineering - Helwan", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500061"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 2, "Faculty", null, null, "كلية الهندسة بالمطرية", "Faculty of Engineering - Mataria", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500062"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 3, "Faculty", null, null, "كلية الحاسبات والذكاء الاصطناعي", "Faculty of Computers and Artificial Intelligence", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500063"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 4, "Faculty", null, null, "كلية التجارة وإدارة الأعمال", "Faculty of Commerce and Business Administration", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500064"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 5, "Faculty", null, null, "كلية السياحة والفنادق", "Faculty of Tourism and Hotels", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500065"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 6, "Faculty", null, null, "كلية الفنون الجميلة", "Faculty of Fine Arts", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500066"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 7, "Faculty", null, null, "كلية الفنون التطبيقية", "Faculty of Applied Arts", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500067"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 8, "Faculty", null, null, "كلية التربية", "Faculty of Education", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500068"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 9, "Faculty", null, null, "كلية التربية الفنية", "Faculty of Art Education", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500069"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 10, "Faculty", null, null, "كلية التربية الموسيقية", "Faculty of Music Education", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500070"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 11, "Faculty", null, null, "كلية الاقتصاد المنزلي", "Faculty of Home Economics", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500071"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 12, "Faculty", null, null, "كلية الآداب", "Faculty of Arts", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500072"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 13, "Faculty", null, null, "كلية العلوم", "Faculty of Science", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500073"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 14, "Faculty", null, null, "كلية الصيدلة", "Faculty of Pharmacy", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500074"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 15, "Faculty", null, null, "كلية التمريض", "Faculty of Nursing", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500075"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 16, "Faculty", null, null, "كلية التربية الرياضية بنين", "Faculty of Physical Education - Men", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500076"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "HELWAN_UNIVERSITY_FACULTIES", 17, "Faculty", null, null, "كلية التربية الرياضية بنات", "Faculty of Physical Education - Women", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500080"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 1, "StudyField", null, null, "علوم البيانات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500081"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 2, "StudyField", null, null, "الذكاء الاصطناعي", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500082"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 3, "StudyField", null, null, "هندسة البرمجيات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500083"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 4, "StudyField", null, null, "نظم المعلومات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500084"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 5, "StudyField", null, null, "علوم الحاسوب", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500085"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 6, "StudyField", null, null, "الشبكات وأمن المعلومات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500086"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 7, "StudyField", null, null, "إدارة الأعمال", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500087"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 8, "StudyField", null, null, "المالية والمحاسبة", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500088"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 9, "StudyField", null, null, "التسويق الرقمي", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500089"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "STUDY_FIELDS", 10, "StudyField", null, null, "الهندسة الكهربائية", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500090"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 1, "Department", null, null, "قسم النظم الموزعة", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500091"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 3, "Department", null, null, "قسم الذكاء الاصطناعي", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500092"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 4, "Department", null, null, "قسم الشبكات وأمن المعلومات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500093"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 5, "Department", null, null, "قسم نظم المعلومات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500094"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 6, "Department", null, null, "قسم علوم البيانات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500095"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 7, "Department", null, null, "قسم هندسة البرمجيات", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500096"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 8, "Department", null, null, "قسم علوم الحاسوب", "", 0 },
                    { new Guid("50505050-5050-5050-5050-505050500099"), new DateTime(2025, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Helwan Faculty Portal System", null, null, null, false, "DEPARTMENTS", 2, "Department", null, null, "قسم البرمجيات", "", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500060"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500061"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500062"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500063"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500064"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500065"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500066"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500067"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500068"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500069"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500070"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500071"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500072"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500073"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500074"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500075"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500076"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500080"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500081"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500082"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500083"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500084"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500085"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500086"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500087"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500088"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500089"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500090"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500091"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500092"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500093"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500094"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500095"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500096"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("50505050-5050-5050-5050-505050500099"));
        }
    }
}
