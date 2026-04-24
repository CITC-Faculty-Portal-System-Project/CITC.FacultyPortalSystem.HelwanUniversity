using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FacultiesAndDepartmentsDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_Lookups_DepartmentId",
                table: "PersonalData");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_DepartmentId",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "PersonalData");

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "PersonalData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "PersonalData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "IsDeleted", "NameAR", "NameEN", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التربية", "Faculty of Education", null, null, 0 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الاقتصاد المنزلي", "Faculty of Home Economics", null, null, 0 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الاداب", "Faculty of Arts", null, null, 0 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الخدمة الاجتماعية", "Faculty of Social Work", null, null, 0 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الحاسبات والذكاء الاصطناعي", "Faculty of Computer Science and Artificial Intelligence", null, null, 0 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التمريض", "Faculty of Nursing", null, null, 0 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية العلوم", "Faculty of Science", null, null, 0 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الفنون الجميلة", "Faculty of Fine Arts", null, null, 0 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الفنون التطبيقية", "Faculty of Applied Arts", null, null, 0 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التجارة وادارة الاعمال", "Faculty of Commerce and Business Administration", null, null, 0 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "(كلية الهندسة (حلوان", "Faculty of Engineering (Helwan)", null, null, 0 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "(كلية الهندسة (المطرية", "Faculty of Engineering (Mataria)", null, null, 0 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "المعهد القومي للملكية الفكرية", "National Institute of Intellectual Property", null, null, 0 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التربية الفنية", "Faculty of Art Education", null, null, 0 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "(كلية علوم الرياضة (بنات", "Faculty of Sports Science (Girls)", null, null, 0 },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "(كلية علوم الرياضة (بنين", "Faculty of Sports Science (Boys)", null, null, 0 },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التربية الموسيقية", "Faculty of Music Education", null, null, 0 },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية السياحة والفنادق", "Faculty of Tourism and Hotels", null, null, 0 },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الطب", "Faculty of Medicine", null, null, 0 },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الصيدلة", "Faculty of Pharmacy", null, null, 0 },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية الحقوق", "Faculty of Law", null, null, 0 },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية التكنولوجيا والتعليم", "Faculty of Technology and Education", null, null, 0 },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "معهد التمريض", "Technical Institute of Nursing", null, null, 0 },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, false, "كلية علوم التغذية", "Faculty of Nutrition Sciences", null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeletionReason", "FacultyId", "IsDeleted", "NameAR", "NameEN", "UpdatedAt", "UpdatedBy", "VersionNo" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم المناهج وطرق التدريس", "Department of Curriculum and Instruction", null, null, 0 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم تكنولوجيا التعليم", "Department of Educational Technology", null, null, 0 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم التعليم الصناعي", "Department of Industrial Education", null, null, 0 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم علم النفس التربوي", "Department of Educational Psychology", null, null, 0 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم رياض الاطفال", "Early Childhood Education Department", null, null, 0 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم الصحة النفسية", "Department of Mental Health", null, null, 0 },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم التربية الخاصة", "Department of Special Education", null, null, 0 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم أصول التربية", "Department of Fundamentals of Education", null, null, 0 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 1, false, "قسم التربية المقارنة والادارة التربوية", "Comparative Education and Educational Administration", null, null, 0 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "قسم التغذية وعلوم الأطعمة", "Department of Nutrition and Food Sciences", null, null, 0 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "قسم ادارة مؤسسات الأسرة والطفولة", "Department of Family and Child Management", null, null, 0 },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "قسم الملابس والنسيج", "Department of Clothing and Textiles", null, null, 0 },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "قسم الصناعات الجلدية", "Department of Leather Industries", null, null, 0 },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "برنامج التدريس لذوي الاحتياجات الخاصة", "Special Education Teaching Program", null, null, 0 },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "برنامج التغذية العلاجية", "Therapeutic Nutrition Program", null, null, 0 },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 2, false, "برنامج تكنولوجيا تصنيع الملابس", "Garment Manufacturing Technology Program", null, null, 0 },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم الجغرافيا ونظم المعلومات الجغرافية", "Department of Geography and Geographic Information Systems", null, null, 0 },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة العربية", "Department of Arabic Language", null, null, 0 },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم التاريخ", "Department of History", null, null, 0 },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم علوم المسرح", "Department of Theatre Arts", null, null, 0 },
                    { 21, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم علم الاجتماع", "Department of Sociology", null, null, 0 },
                    { 22, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الصينية وادابها", "Department of Chinese Language and Literature", null, null, 0 },
                    { 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم المكتبات والمعلومات", "Department of Library and Information Science", null, null, 0 },
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة العبرية وادابها", "Department of Hebrew Language and Literature", null, null, 0 },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الألمانية وادابها", "Department of German Language and Literature", null, null, 0 },
                    { 26, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الفرنسية وادابها", "Department of French Language and Literature", null, null, 0 },
                    { 27, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الانجليزية وادابها", "Department of English Language and Literature", null, null, 0 },
                    { 28, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الايطالية وادابها", "Department of Italian Language and Literature", null, null, 0 },
                    { 29, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم علم النفس", "Department of Psychology", null, null, 0 },
                    { 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الاسبانية وادابها", "Department of Spanish Language and Literature", null, null, 0 },
                    { 31, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغات الشرقية", "Department of Eastern Languages", null, null, 0 },
                    { 32, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم اللغة الاعلام", "Department of Media", null, null, 0 },
                    { 33, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم الفلسفة", "Department of Philosophy", null, null, 0 },
                    { 34, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "قسم الاثار والحضارة", "Department of Archaeology and Civilization", null, null, 0 },
                    { 35, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "برنامج النقوش والمخطوطات", "Program of Inscriptions and Manuscripts", null, null, 0 },
                    { 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "برنامج الدبلوم المهاري والتطبيقي لنظم المعلومات الجغرافية", "Applied GIS Professional Diploma Program", null, null, 0 },
                    { 37, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "برنامج الجيوماتكس ونظم المعلومات الجغرافية", "Geomatics and GIS Program", null, null, 0 },
                    { 38, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "برنامج الترجمة الفورية والتحريرية", "Interpreting and Translation Program", null, null, 0 },
                    { 39, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 3, false, "برنامج اللغة الفرنسية", "French Language and Translation Program", null, null, 0 },
                    { 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "قسم العمل مع الأفراد والأسر", "Department of Individual and Family Work", null, null, 0 },
                    { 41, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "قسم العمل مع الجماعات", "Department of Group Work", null, null, 0 },
                    { 42, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "قسم العمل مع المنظمات والمجتمعات", "Department of Community and Organizational Work", null, null, 0 },
                    { 43, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "قسم التخطيط الاجتماعي", "Department of Social Planning", null, null, 0 },
                    { 44, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "قسم مجالات الخدمة الاجتماعية", "Department of Social Work Fields", null, null, 0 },
                    { 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "برنامج بكالوريوس الخدمة الاجتماعية", "Bachelor's Program in Social Work", null, null, 0 },
                    { 46, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "برنامج الماجستير المهني في الاستثمار الاجتماعي", "Professional Master's Program in Social Investment", null, null, 0 },
                    { 47, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "الماجستير المهني في تخطيط وتقويم البرامج والمشروعات الاجتماعية", "Professional Master's Program in Planning and Evaluation of Social Programs and Projects", null, null, 0 },
                    { 48, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 4, false, "الماجستير المهني في تصميم وممارسة العلاج الجماعي", "Professional Master's Program in Group Therapy Design and Practice", null, null, 0 },
                    { 49, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "تكنولوجيا المعلومات", "Information Technology", null, null, 0 },
                    { 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "علوم الحاسب", "Computer Science", null, null, 0 },
                    { 51, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "نظم المعلومات", "Information Systems", null, null, 0 },
                    { 52, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "الذكاء الاصطناعي", "Artificial Intelligence", null, null, 0 },
                    { 53, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "هندسة البرمجيات", "Software Engieering", null, null, 0 },
                    { 54, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "المعلوماتية الطبية", "Bio Informatics", null, null, 0 },
                    { 55, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "مرحلة الدبلوم", "Diploma", null, null, 0 },
                    { 56, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "مرحلة الماجستير", "Master", null, null, 0 },
                    { 57, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 5, false, "مرحلة الدكتوراه", "PhD", null, null, 0 },
                    { 58, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم تمريض صحة البالغين", "Adult Health Nursing Department", null, null, 0 },
                    { 59, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم تمريض صحة الطفل", "Pediatric health Nursing Department", null, null, 0 },
                    { 60, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم تمريض صحة الام وحديثي الولادة", "Maternal and Newborn Health Nursing Department", null, null, 0 },
                    { 61, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم تمريض الصحة النفسية والعقلية", "Department of Mental Health Nursing", null, null, 0 },
                    { 62, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم تمريض صحة المجتمع", "Department of Community Health Nursing", null, null, 0 },
                    { 63, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "قسم ادارة التمريض", "Department of Nursing Administration", null, null, 0 },
                    { 64, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "المعهد الفني للتمريض", "Technical Institute of Nursing", null, null, 0 },
                    { 65, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "برنامج البكالوريوس في علوم التمريض", "Bachelor's Program in Nursing", null, null, 0 },
                    { 66, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 6, false, "(برنامج البكالوريوس في علوم التمريض (المكثف", "Intensive Bachelor's Program in Nursing", null, null, 0 },
                    { 67, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الرياضيات", "Mathematics", null, null, 0 },
                    { 68, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الفيزياء", "Physics", null, null, 0 },
                    { 69, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الكيمياء", "Chemistry", null, null, 0 },
                    { 70, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "علم الحيوان والحشرات", "Zoology and Entomology", null, null, 0 },
                    { 71, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "النبات والميكروبيولوجي", "Botany and Microbiology", null, null, 0 },
                    { 72, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الجيولوجيا", "Geology", null, null, 0 },
                    { 73, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء الغير عضوية", "Inorganic Chemistry Program", null, null, 0 },
                    { 74, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء العضوية", "Organic Chemistry Program", null, null, 0 },
                    { 75, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء التحليلية", "Analytical Chemistry Program", null, null, 0 },
                    { 76, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء الفيزيائية", "Physical Chemistry Program", null, null, 0 },
                    { 77, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء الحيوية", "Biochemistry Program", null, null, 0 },
                    { 78, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الكيمياء التطبيقية", "Applied Chemistry Program", null, null, 0 },
                    { 79, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج التكنولوجيا الحيوية والبيولوجيا الجزئية", "Biotechnology and Molecular Biology Program", null, null, 0 },
                    { 80, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج علم الحيوان", "Zoology Program", null, null, 0 },
                    { 81, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج علم الحيوان والكيمياء", "Zoology and Chemistry Program", null, null, 0 },
                    { 82, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الفيزياء", "Physics Program", null, null, 0 },
                    { 83, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الفيزياء الحيوية الطبية", "Medical Biophysics Program", null, null, 0 },
                    { 84, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج علوم الفضاء", "Space Science Program", null, null, 0 },
                    { 85, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الرياضيات", "Mathematics Program", null, null, 0 },
                    { 86, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الرياضيات والحاسب", "Mathematics and Computer Science Program", null, null, 0 },
                    { 87, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الاحصاء والحاسب", "Statistics and Computer Science Program", null, null, 0 },
                    { 88, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج التكنولوجيا الحيوية الجزئية", "Statistics Program", null, null, 0 },
                    { 89, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج الوراثة والمناعة التطبيقية", "Applied Genetics and Immunology Program", null, null, 0 },
                    { 90, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "برنامج البترول والمعادن", "Petroleum and Minerals Program", null, null, 0 },
                    { 91, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "دبلومة الفسيولوجي والتحاليل الطبية", "Diploma in Physiology and Medical Analysis", null, null, 0 },
                    { 92, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "شعبة الفسيولوجي والبيئة", "Physiology and Environment", null, null, 0 },
                    { 93, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "شعبة اللافقاريات والطفيليات والمناعة", "Invertebrates, Parasitology, and Immunology", null, null, 0 },
                    { 94, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "شعبة الخلية والأنسجة والوراثة", "Cell, Tissue, and Genetics", null, null, 0 },
                    { 95, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "شعبة التشريح المقارن والأجنة", "Comparative Anatomy and Embryology", null, null, 0 },
                    { 96, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "شعبة الحشرات", "Entomology", null, null, 0 },
                    { 97, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "فيزياء الجوامد التطبيقية", "Applied Solid State Physics", null, null, 0 },
                    { 98, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "فيزياء الالكترونيات", "Electronics Physics", null, null, 0 },
                    { 99, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "البصريات والليزر والأطياف الذرية", "Optics, Laser, and Atomic Spectroscopy", null, null, 0 },
                    { 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الفيزياء النووية التطبيقية", "Applied Nuclear Physics", null, null, 0 },
                    { 101, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الفيزياء الاشعاعية التطبيقية", "Applied Radiation Physics", null, null, 0 },
                    { 102, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الفيزياء الحيوية الطبية", "Medical Biophysics", null, null, 0 },
                    { 103, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "فيزياء الفلك وعلوم الفضاء", "Astronomy and Space Sciences", null, null, 0 },
                    { 104, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الفيزياء النظرية", "Theoretical Physics", null, null, 0 },
                    { 105, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الرياضيات البحتة", "Pure Mathematics", null, null, 0 },
                    { 106, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الرياضيات التطبيقية", "Applied Mathematics", null, null, 0 },
                    { 107, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الاحصاء", "Statistics", null, null, 0 },
                    { 108, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "علوم الحاسب", "Computer Science", null, null, 0 },
                    { 109, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "دكتوراه التكنولوجيا الحيوية الجزئية", "PhD in Molecular Biotechnology", null, null, 0 },
                    { 110, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "ماجستير التكنولوجيا الحيوية الجزئية", "Master in Molecular Biotechnology", null, null, 0 },
                    { 111, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "ماجستير الفيزياء الطبية التطبيقية", "Master in Applied Medical Physics", null, null, 0 },
                    { 112, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الدبلوم المهني في تحاليل الكيمياء الحيوية", "Professional Diploma in Biochemical Analysis", null, null, 0 },
                    { 113, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الدبلوم المهني في الميكروبيولوجيا التطبيقية", "Professional Diploma in Applied Microbiology", null, null, 0 },
                    { 114, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 7, false, "الدبلوم المهني في الفسيولوجي والتحاليل المعملية", "Professional Diploma in Physiology and Laboratory Analysis", null, null, 0 },
                    { 115, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم النحت", "Sculpture Department", null, null, 0 },
                    { 116, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم العمارة", "Architecture Department", null, null, 0 },
                    { 117, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم الجرافيك", "Graphic Design Department", null, null, 0 },
                    { 118, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم التصوير", "Photography Department", null, null, 0 },
                    { 119, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم الديكور", "Department of Decoration", null, null, 0 },
                    { 120, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 8, false, "قسم تاريخ الفن", "Art History Department", null, null, 0 },
                    { 121, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "الفوتوغرافيا والسينما والتليفزيون", "Photography, Cinema, and Television Department", null, null, 0 },
                    { 122, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الملابس الجاهزة", "Ready-made Clothing Department", null, null, 0 },
                    { 123, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم طباعة المسنوجات والصباغة والتجهيز", "Textile Printing, Dyeing, and Finishing Department", null, null, 0 },
                    { 124, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الطباعة والنشر والتغليف", "Printing, Publishing, and Packaging Department", null, null, 0 },
                    { 125, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الاعلان", "Advertising Department", null, null, 0 },
                    { 126, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم التصميم الداخلي والاثاث", "Interior Design and Furniture Department", null, null, 0 },
                    { 127, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم التصميم الصناعي", "Industrial Design Department", null, null, 0 },
                    { 128, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الاثاث والانشاءات المعدنية", "Furniture and Metal Constructions Department", null, null, 0 },
                    { 129, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم المنتجات المعدنية والحلي", "Department of Metal Products and Jewelry", null, null, 0 },
                    { 130, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الخزف", "Ceramics Department", null, null, 0 },
                    { 131, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الزجاج", "Glass Department", null, null, 0 },
                    { 132, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الزخرفة", "Decoration Department", null, null, 0 },
                    { 133, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم النحت والتشكيل المعماري", "Sculpture and Architectural Formation Department", null, null, 0 },
                    { 134, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم طباعة المنسوجات والصباغة والتجهيز", "Textile Printing, Dyeing, and Finishing Department", null, null, 0 },
                    { 135, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم الوسائط المطبوعة", "Print Media Department", null, null, 0 },
                    { 136, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم علوم التغليف", "Packaging Science Department", null, null, 0 },
                    { 137, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم علوم تصميم الاثاث", "Furniture Design Science Department", null, null, 0 },
                    { 138, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 9, false, "قسم تصميم وتشكيل الزجاج في العمارة", "Glass Design and Formation in Architecture Department", null, null, 0 },
                    { 139, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم المحاسبة", "Accounting Department", null, null, 0 },
                    { 140, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم الاقتصاد والتجارة الخارجية", "Department of Economics and Foreign Trade", null, null, 0 },
                    { 141, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم ادارة الاعمال", "Business Administration Department", null, null, 0 },
                    { 142, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم الرياضة والتأمين والاحصاء", "Department of Mathematics, Insurance, and Statistics", null, null, 0 },
                    { 143, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم العلوم السياسية", "Department of Political Science", null, null, 0 },
                    { 144, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم نظم المعلومات", "Department of Information Systems", null, null, 0 },
                    { 145, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "شعبة اللغات", "Language Division", null, null, 0 },
                    { 146, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم نظم معلومات الاعمال", "Business Information Systems Department", null, null, 0 },
                    { 147, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "قسم الاسواق والمنشأت المالية", "Financial Markets and Institutions Department", null, null, 0 },
                    { 148, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "الدراسات العليا الاكاديمية", "Academic Graduate Studies", null, null, 0 },
                    { 149, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 10, false, "ادارة المستشفيات واقتصاديات الصحة", "Hospital Management and Health Economics", null, null, 0 },
                    { 150, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "قسم هندسة الالكترونيات والاتصالات", "Department of Electronics and Communications Engineering", null, null, 0 },
                    { 151, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "قسم هندسة الحاسبات والنظم", "Computer Engineering Department", null, null, 0 },
                    { 152, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "قسم هندسة القوى الكهربائية والآلات", "Department of Electrical Power and Machines Engineering", null, null, 0 },
                    { 153, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "قسم الهندسة الميكانيكية", "Mechanical Engineering Department", null, null, 0 },
                    { 154, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "قسم الهندسة الحيوية الطبية", "Biomedical and Medical Engineering Department", null, null, 0 },
                    { 155, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "برنامج هندسة الانتاج", "Production Engineering Program", null, null, 0 },
                    { 156, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "برنامج الهندسة الصناعية", "Industrial Engineering Program", null, null, 0 },
                    { 157, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "برنامج هندسة الميكاترونيك", "Mechatronics Engineering Program", null, null, 0 },
                    { 158, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "برنامج هندسة الاتصالات والمعلومات", "Telecommunications and Information Engineering Program", null, null, 0 },
                    { 159, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 11, false, "برنامج هندسة القوى والوقاية الكهربية", "Electrical Power and Protection Engineering Program", null, null, 0 },
                    { 160, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم الهندسة المدنية", "Civil Engineering Department", null, null, 0 },
                    { 161, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم هندسة القوى الميكانيكية", "Mechanical Power Engineering Department", null, null, 0 },
                    { 162, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم السيارات والجرارات", "Architectural Engineering Department", null, null, 0 },
                    { 163, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم الهندسة المعمارية", "Architectural Engineering Department", null, null, 0 },
                    { 164, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم التصميم الميكانيكي", "Mechanical Design Engineering Department", null, null, 0 },
                    { 165, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "قسم الفيزيقيا والرياضيات", "Physics and Mathematics Department", null, null, 0 },
                    { 166, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "برنامج هندسة الطاقة", "Energy Engineering Program", null, null, 0 },
                    { 167, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "برنامج الهندسة الانشائية", "Construction Engineering Program", null, null, 0 },
                    { 168, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "برنامج العمارة بالتكنولوجيا الرقمية", "Digital Architecture Program", null, null, 0 },
                    { 169, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "برنامج هندسة الميكاترونيات بالسيارات", "Automotive Mechatronics Engineering Program", null, null, 0 },
                    { 170, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 12, false, "برنامج ادارة المشروعات والتشييد", "Project Management and Construction Program", null, null, 0 },
                    { 171, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 13, false, "قسم الملكية الصناعية", "Industrial Property Department", null, null, 0 },
                    { 172, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 13, false, "قسم الملكية الادبية والفنية", "Project Management and Construction Department", null, null, 0 },
                    { 173, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم التصميمات الزخرفية", "Decorative Designs Department", null, null, 0 },
                    { 174, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم الرسم والتصوير", "Drawing and Painting Department", null, null, 0 },
                    { 175, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم النقد والتذوق الفني", "Art Criticism and Appreciation Department", null, null, 0 },
                    { 176, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم علوم التربية الفنية", "Art Education Department", null, null, 0 },
                    { 177, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم التعبير المجسم", "Sculptural Expression Department", null, null, 0 },
                    { 178, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 14, false, "قسم الاشغال الفنية والتراث الشعبي", "Art and Folk Heritage Department", null, null, 0 },
                    { 179, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم العلوم التربوية والنفسية والاجتماعية الرياضية", "Educational, Psychological, and Social Sciences Department", null, null, 0 },
                    { 180, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم العلوم الحيوية والصحة الرياضية", "Biological and Sports Health Sciences Department", null, null, 0 },
                    { 181, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم المناهج وطرق تدريس التربية البدنية", "Curriculum and Methods of Physical Education Department", null, null, 0 },
                    { 182, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم علوم الادارة الرياضية والترويح", "Sports Management and Recreation Department", null, null, 0 },
                    { 183, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات المنازلات والرياضات الفردية", "Theories and Applications of Individual Sports Department", null, null, 0 },
                    { 184, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات الرياضات المائية", "Theories and Applications of Aquatic Sports Department", null, null, 0 },
                    { 185, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات الرياضات الجماعية وألعاب المضرب", "Theories and Applications of Team Sports and Racket Games Department", null, null, 0 },
                    { 186, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات العاب القوى", "Theories and Applications of Athletics Department", null, null, 0 },
                    { 187, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات التعبير الحركي والايقاع الحركي", "Theories and Applications of Motor Expression and Rhythmic Movement Department", null, null, 0 },
                    { 188, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 15, false, "قسم نظريات وتطبيقات التمرينات والجمباز", "Theories and Applications of Gymnastics and Exercises Department", null, null, 0 },
                    { 189, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم الادارة الرياضية", "Sports Management Department", null, null, 0 },
                    { 190, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم الترويح الرياضي", "Sports Recreation Department", null, null, 0 },
                    { 191, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم المناهج وطرق التدريس", "Curriculum and Teaching Methods Department", null, null, 0 },
                    { 192, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم علوم الحركة الرياضية", "Sports Movement Sciences Department", null, null, 0 },
                    { 193, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم علوم الصحة الرياضية", "Sports Health Sciences Department", null, null, 0 },
                    { 194, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم علوم النفس الرياضي", "Sports Psychology Sciences Department", null, null, 0 },
                    { 195, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم تدريب الرياضات الاساسية", "Basic Sports Training Department", null, null, 0 },
                    { 196, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم تدريب الرياضات الفردية", "Individual Sports Training Department", null, null, 0 },
                    { 197, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم تدريب الرياضات الجماعية", "Team Sports Training Department", null, null, 0 },
                    { 198, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم رياضة كبار السن", "Elderly Sports Department", null, null, 0 },
                    { 199, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 16, false, "قسم التربية الرياضية المعدلة", "Department of Adapted Physical Education", null, null, 0 },
                    { 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 17, false, "قسم النظريات والتأليف", "Theories and Composition Department", null, null, 0 },
                    { 201, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 17, false, "قسم الموسيقى العربية", "Arabic Music Department", null, null, 0 },
                    { 202, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 17, false, "قسم البيانو والمصاحبة", "Piano and Accompaniment Department", null, null, 0 },
                    { 203, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 17, false, "قسم الاداء", "Performance Department", null, null, 0 },
                    { 204, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 17, false, "قسم العلوم الموسيقية التربوية", "Department of Music Education", null, null, 0 },
                    { 205, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 18, false, "قسم الدراسات السياحية", "Tourism Studies Department", null, null, 0 },
                    { 206, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 18, false, "قسم الدراسات الفندقية", "Hotel Studies Department", null, null, 0 },
                    { 207, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 18, false, "قسم الارشاد السياحي", "Tourism Guidance Department", null, null, 0 },
                    { 208, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 18, false, "قسم ادارة المطاعم", "Restaurant Management Department", null, null, 0 },
                    { 209, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 24, false, "قسم علوم الأغذية", "Food Science Department", null, null, 0 },
                    { 210, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 24, false, "قسم التغذية العلاجية", "Therapeutic Nutrition Department", null, null, 0 },
                    { 211, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 24, false, "قسم تغذية المجتمع", "Community Nutrition Department", null, null, 0 },
                    { 212, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, "قسم تكنولوجيا الميكانيكا", "Mechanical Technology Department", null, null, 0 },
                    { 213, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, "قسم تكنولوجيا الالكترونيات والاتصالات", "Electronics and Communications Technology Department", null, null, 0 },
                    { 214, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, "قسم تكنولوجيا التشييد والبناء", "Construction Technology Department", null, null, 0 },
                    { 215, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, " قسم تكنولوجيا السيارات", "Automotive Technology Department", null, null, 0 },
                    { 216, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, "قسم العلوم التربوية والنفسية", "Educational and Psychological Sciences Department", null, null, 0 },
                    { 217, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 22, false, "قسم المناهج وطرق التدريس", "Curriculum and Instruction Department", null, null, 0 },
                    { 218, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون الجنائي", "Criminal Law", null, null, 0 },
                    { 219, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون التجاري", "Commercial Law", null, null, 0 },
                    { 220, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "قانون العمل والتشريعات الاجتماعية", "Labor and Social Legislation Law", null, null, 0 },
                    { 221, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "الشريعة الإسلامية", "Islamic Sharia", null, null, 0 },
                    { 222, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون المدني", "Civil Law", null, null, 0 },
                    { 223, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون الدولي العام", "Public International Law", null, null, 0 },
                    { 224, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "فلسفة القانون وتاريخه", "Philosophy and History of Law", null, null, 0 },
                    { 225, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "الاقتصاد", "Economics", null, null, 0 },
                    { 226, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون العام", "Public Law", null, null, 0 },
                    { 227, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "قانون المرافعات", "Procedural Law", null, null, 0 },
                    { 228, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "القانون الدولي الخاص", "Private International Law", null, null, 0 },
                    { 229, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "برنامج الدراسات القانونية باللغة الفرنسية", "Legal Studies Program in French", null, null, 0 },
                    { 230, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم التشريح وعلم الأجنة", "Department of Anatomy and Embryology", null, null, 0 },
                    { 231, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الفسيولوجيا الطبية", "Department of Medical Physiology", null, null, 0 },
                    { 232, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الكيمياء الحيوية الطبية والبيولوجيا الجزئية", "Department of Medical Biochemistry and Molecular Biology", null, null, 0 },
                    { 233, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الهستولوجي", "Department of Histology", null, null, 0 },
                    { 234, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الباثولوجيا", "Department of Pathology", null, null, 0 },
                    { 235, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الباثولوجيا الإكلينيكية والكيميائية", "Department of Clinical and Chemical Pathology", null, null, 0 },
                    { 236, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة المسالك البولية", "Department of Urology", null, null, 0 },
                    { 237, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة التجميل", "Department of Plastic Surgery", null, null, 0 },
                    { 238, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب الحالات الحرجة والطوارئ", "Department of Critical Care and Emergency Medicine", null, null, 0 },
                    { 239, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الفارماكولوجيا الطبية", "Department of Medical Pharmacology", null, null, 0 },
                    { 240, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الميكروبيولوجيا الطبية والمناعة", "Department of Medical Microbiology and Immunology", null, null, 0 },
                    { 241, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الطفيليات الطبية", "Department of Medical Parasitology", null, null, 0 },
                    { 242, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب الأسرة", "Department of Family Medicine", null, null, 0 },
                    { 243, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب المجتمع والبيئة وطب الصناعات", "Department of Community, Environmental and Occupational Medicine", null, null, 0 },
                    { 244, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم التخدير والعناية المركزة وعلاج الألم", "Department of Anesthesia, Intensive Care and Pain Management", null, null, 0 },
                    { 245, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم التوليد وأمراض النساء", "Department of Obstetrics and Gynecology", null, null, 0 },
                    { 246, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة العظام", "Department of Orthopedic Surgery", null, null, 0 },
                    { 247, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة الأوعية الدموية", "Department of Vascular Surgery", null, null, 0 },
                    { 248, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الطب الشرعي والسموم الإكلينيكية", "Department of Forensic Medicine and Clinical Toxicology", null, null, 0 },
                    { 249, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم أمراض الباطنة العامة", "Department of General Internal Medicine", null, null, 0 },
                    { 250, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأطفال", "Department of Pediatrics", null, null, 0 },
                    { 251, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الجراحة العامة", "Department of General Surgery", null, null, 0 },
                    { 252, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأمراض الصدرية", "Department of Chest Diseases", null, null, 0 },
                    { 253, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأشعة التشخيصية والعلاجية", "Department of Diagnostic and Therapeutic Radiology", null, null, 0 },
                    { 254, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب وجراحة العيون", "Department of Ophthalmology", null, null, 0 },
                    { 255, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب المخ والأعصاب والطب النفسي", "Department of Neurology and Psychiatry", null, null, 0 },
                    { 256, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم طب وصحة المسنين", "Department of Geriatric Medicine", null, null, 0 },
                    { 257, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأمراض الجلدية والتناسلية والذكورة", "Department of Dermatology, Venereology and Andrology", null, null, 0 },
                    { 258, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة المخ والأعصاب", "Department of Neurosurgery", null, null, 0 },
                    { 259, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأمراض المتوطنة", "Department of Endemic Diseases", null, null, 0 },
                    { 260, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم أمراض القلب والأوعية الدموية", "Department of Cardiovascular Diseases", null, null, 0 },
                    { 261, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الطب الطبيعي والروماتيزم والتأهيل", "Department of Physical Medicine, Rheumatology and Rehabilitation", null, null, 0 },
                    { 262, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم علاج الأورام والطب النووي", "Department of Oncology and Nuclear Medicine", null, null, 0 },
                    { 263, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم الأنف والأذن والحنجرة", "Department of Ear, Nose and Throat", null, null, 0 },
                    { 264, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة القلب والصدر", "Department of Cardiothoracic Surgery", null, null, 0 },
                    { 265, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 20, false, "قسم جراحة الأطفال", "Department of Pediatric Surgery", null, null, 0 },
                    { 266, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم العقاقير", "Department of Pharmacognosy", null, null, 0 },
                    { 267, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الصيدلانيات والصيدلة الصناعية", "Department of Pharmaceutics and Industrial Pharmacy", null, null, 0 },
                    { 268, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم ممارسة الصيدلة", "Department of Pharmacy Practice", null, null, 0 },
                    { 269, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الأدوية والسموم", "Department of Pharmacology and Toxicology", null, null, 0 },
                    { 270, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الكيمياء الصيدلية", "Department of Pharmaceutical Chemistry", null, null, 0 },
                    { 271, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الكيمياء التحليلية الصيدلية", "Department of Pharmaceutical Analytical Chemistry", null, null, 0 },
                    { 272, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الكيمياء العضوية الصيدلية", "Department of Pharmaceutical Organic Chemistry", null, null, 0 },
                    { 273, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الكيمياء الحيوية والبيولوجيا الجزيئية", "Department of Biochemistry and Molecular Biology", null, null, 0 },
                    { 274, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 19, false, "قسم الميكروبيولوجيا والمناعة", "Department of Microbiology and Immunology", null, null, 0 },
                    { 278, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", null, null, null, 21, false, "برنامج الدراسات القانونية باللغة الانجليزية", "Legal Studies Program in English", null, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_DeptId",
                table: "PersonalData",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_FacultyId",
                table: "PersonalData",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyId",
                table: "Departments",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Id",
                table: "Departments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_NameAR",
                table: "Departments",
                column: "NameAR");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_NameEN",
                table: "Departments",
                column: "NameEN");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_Id",
                table: "Faculties",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_NameAR",
                table: "Faculties",
                column: "NameAR");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_NameEN",
                table: "Faculties",
                column: "NameEN");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_Departments_DeptId",
                table: "PersonalData",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_Faculties_FacultyId",
                table: "PersonalData",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_Departments_DeptId",
                table: "PersonalData");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalData_Faculties_FacultyId",
                table: "PersonalData");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_DeptId",
                table: "PersonalData");

            migrationBuilder.DropIndex(
                name: "IX_PersonalData_FacultyId",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "PersonalData");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "PersonalData",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PersonalData_DepartmentId",
                table: "PersonalData",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalData_Lookups_DepartmentId",
                table: "PersonalData",
                column: "DepartmentId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
