using Domain.Entities.UniversityFacultiesAndDepartments;

namespace Presistence.Data.Configurations.FacultiesAndDepartmentsConfigurations
{
    public class DepartmentsConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            #region AddingIndcies

            builder.HasIndex(f => f.Id);
            builder.HasIndex(f => f.NameAR);
            builder.HasIndex(f => f.NameEN);

            #endregion

            #region ConfiguringRelations

            builder.HasOne(f => f.Faculty)
                .WithMany(d => d.Departments)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(d => d.FacultyMembers)
                .WithOne(f => f.Department)
                .HasForeignKey(f => f.DeptId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region DataSeeding

            builder.HasData(
                    new Department
                    {
                        Id = 1,
                        NameAR = "قسم المناهج وطرق التدريس",
                        NameEN = "Department of Curriculum and Instruction",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 2,
                        NameAR = "قسم تكنولوجيا التعليم",
                        NameEN = "Department of Educational Technology",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 3,
                        NameAR = "قسم التعليم الصناعي",
                        NameEN = "Department of Industrial Education",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 4,
                        NameAR = "قسم علم النفس التربوي",
                        NameEN = "Department of Educational Psychology",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 5,
                        NameAR = "قسم رياض الاطفال",
                        NameEN = "Early Childhood Education Department",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 6,
                        NameAR = "قسم الصحة النفسية",
                        NameEN = "Department of Mental Health",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 7,
                        NameAR = "قسم التربية الخاصة",
                        NameEN = "Department of Special Education",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 8,
                        NameAR = "قسم أصول التربية",
                        NameEN = "Department of Fundamentals of Education",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 9,
                        NameAR = "قسم التربية المقارنة والادارة التربوية",
                        NameEN = "Comparative Education and Educational Administration",
                        FacultyId = 1
                    },
                    new Department
                    {
                        Id = 10,
                        NameAR = "قسم التغذية وعلوم الأطعمة",
                        NameEN = "Department of Nutrition and Food Sciences",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 11,
                        NameAR = "قسم ادارة مؤسسات الأسرة والطفولة",
                        NameEN = "Department of Family and Child Management",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 12,
                        NameAR = "قسم الملابس والنسيج",
                        NameEN = "Department of Clothing and Textiles",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 13,
                        NameAR = "قسم الصناعات الجلدية",
                        NameEN = "Department of Leather Industries",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 14,
                        NameAR = "برنامج التدريس لذوي الاحتياجات الخاصة",
                        NameEN = "Special Education Teaching Program",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 15,
                        NameAR = "برنامج التغذية العلاجية",
                        NameEN = "Therapeutic Nutrition Program",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 16,
                        NameAR = "برنامج تكنولوجيا تصنيع الملابس",
                        NameEN = "Garment Manufacturing Technology Program",
                        FacultyId = 2
                    },
                    new Department
                    {
                        Id = 17,
                        NameAR = "قسم الجغرافيا ونظم المعلومات الجغرافية",
                        NameEN = "Department of Geography and Geographic Information Systems",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 18,
                        NameAR = "قسم اللغة العربية",
                        NameEN = "Department of Arabic Language",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 19,
                        NameAR = "قسم التاريخ",
                        NameEN = "Department of History",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 20,
                        NameAR = "قسم علوم المسرح",
                        NameEN = "Department of Theatre Arts",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 21,
                        NameAR = "قسم علم الاجتماع",
                        NameEN = "Department of Sociology",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 22,
                        NameAR = "قسم اللغة الصينية وادابها",
                        NameEN = "Department of Chinese Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 23,
                        NameAR = "قسم المكتبات والمعلومات",
                        NameEN = "Department of Library and Information Science",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 24,
                        NameAR = "قسم اللغة العبرية وادابها",
                        NameEN = "Department of Hebrew Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 25,
                        NameAR = "قسم اللغة الألمانية وادابها",
                        NameEN = "Department of German Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 26,
                        NameAR = "قسم اللغة الفرنسية وادابها",
                        NameEN = "Department of French Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 27,
                        NameAR = "قسم اللغة الانجليزية وادابها",
                        NameEN = "Department of English Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 28,
                        NameAR = "قسم اللغة الايطالية وادابها",
                        NameEN = "Department of Italian Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 29,
                        NameAR = "قسم علم النفس",
                        NameEN = "Department of Psychology",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 30,
                        NameAR = "قسم اللغة الاسبانية وادابها",
                        NameEN = "Department of Spanish Language and Literature",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 31,
                        NameAR = "قسم اللغات الشرقية",
                        NameEN = "Department of Eastern Languages",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 32,
                        NameAR = "قسم اللغة الاعلام",
                        NameEN = "Department of Media",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 33,
                        NameAR = "قسم الفلسفة",
                        NameEN = "Department of Philosophy",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 34,
                        NameAR = "قسم الاثار والحضارة",
                        NameEN = "Department of Archaeology and Civilization",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 35,
                        NameAR = "برنامج النقوش والمخطوطات",
                        NameEN = "Program of Inscriptions and Manuscripts",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 36,
                        NameAR = "برنامج الدبلوم المهاري والتطبيقي لنظم المعلومات الجغرافية",
                        NameEN = "Applied GIS Professional Diploma Program",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 37,
                        NameAR = "برنامج الجيوماتكس ونظم المعلومات الجغرافية",
                        NameEN = "Geomatics and GIS Program",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 38,
                        NameAR = "برنامج الترجمة الفورية والتحريرية",
                        NameEN = "Interpreting and Translation Program",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 39,
                        NameAR = "برنامج اللغة الفرنسية",
                        NameEN = "French Language and Translation Program",
                        FacultyId = 3
                    },
                    new Department
                    {
                        Id = 40,
                        NameAR = "قسم العمل مع الأفراد والأسر",
                        NameEN = "Department of Individual and Family Work",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 41,
                        NameAR = "قسم العمل مع الجماعات",
                        NameEN = "Department of Group Work",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 42,
                        NameAR = "قسم العمل مع المنظمات والمجتمعات",
                        NameEN = "Department of Community and Organizational Work",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 43,
                        NameAR = "قسم التخطيط الاجتماعي",
                        NameEN = "Department of Social Planning",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 44,
                        NameAR = "قسم مجالات الخدمة الاجتماعية",
                        NameEN = "Department of Social Work Fields",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 45,
                        NameAR = "برنامج بكالوريوس الخدمة الاجتماعية",
                        NameEN = "Bachelor's Program in Social Work",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 46,
                        NameAR = "برنامج الماجستير المهني في الاستثمار الاجتماعي",
                        NameEN = "Professional Master's Program in Social Investment",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 47,
                        NameAR = "الماجستير المهني في تخطيط وتقويم البرامج والمشروعات الاجتماعية",
                        NameEN = "Professional Master's Program in Planning and Evaluation of Social Programs and Projects",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 48,
                        NameAR = "الماجستير المهني في تصميم وممارسة العلاج الجماعي",
                        NameEN = "Professional Master's Program in Group Therapy Design and Practice",
                        FacultyId = 4
                    },
                    new Department
                    {
                        Id = 49,
                        NameAR = "تكنولوجيا المعلومات",
                        NameEN = "Information Technology",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 50,
                        NameAR = "علوم الحاسب",
                        NameEN = "Computer Science",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 51,
                        NameAR = "نظم المعلومات",
                        NameEN = "Information Systems",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 52,
                        NameAR = "الذكاء الاصطناعي",
                        NameEN = "Artificial Intelligence",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 53,
                        NameAR = "هندسة البرمجيات",
                        NameEN = "Software Engieering",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 54,
                        NameAR = "المعلوماتية الطبية",
                        NameEN = "Bio Informatics",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 55,
                        NameAR = "مرحلة الدبلوم",
                        NameEN = "Diploma",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 56,
                        NameAR = "مرحلة الماجستير",
                        NameEN = "Master",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 57,
                        NameAR = "مرحلة الدكتوراه",
                        NameEN = "PhD",
                        FacultyId = 5
                    },
                    new Department
                    {
                        Id = 58,
                        NameAR = "قسم تمريض صحة البالغين",
                        NameEN = "Adult Health Nursing Department",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 59,
                        NameAR = "قسم تمريض صحة الطفل",
                        NameEN = "Pediatric health Nursing Department",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 60,
                        NameAR = "قسم تمريض صحة الام وحديثي الولادة",
                        NameEN = "Maternal and Newborn Health Nursing Department",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 61,
                        NameAR = "قسم تمريض الصحة النفسية والعقلية",
                        NameEN = "Department of Mental Health Nursing",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 62,
                        NameAR = "قسم تمريض صحة المجتمع",
                        NameEN = "Department of Community Health Nursing",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 63,
                        NameAR = "قسم ادارة التمريض",
                        NameEN = "Department of Nursing Administration",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 64,
                        NameAR = "المعهد الفني للتمريض",
                        NameEN = "Technical Institute of Nursing",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 65,
                        NameAR = "برنامج البكالوريوس في علوم التمريض",
                        NameEN = "Bachelor's Program in Nursing",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 66,
                        NameAR = "(برنامج البكالوريوس في علوم التمريض (المكثف",
                        NameEN = "Intensive Bachelor's Program in Nursing",
                        FacultyId = 6
                    },
                    new Department
                    {
                        Id = 67,
                        NameAR = "الرياضيات",
                        NameEN = "Mathematics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 68,
                        NameAR = "الفيزياء",
                        NameEN = "Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 69,
                        NameAR = "الكيمياء",
                        NameEN = "Chemistry",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 70,
                        NameAR = "علم الحيوان والحشرات",
                        NameEN = "Zoology and Entomology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 71,
                        NameAR = "النبات والميكروبيولوجي",
                        NameEN = "Botany and Microbiology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 72,
                        NameAR = "الجيولوجيا",
                        NameEN = "Geology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 73,
                        NameAR = "برنامج الكيمياء الغير عضوية",
                        NameEN = "Inorganic Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 74,
                        NameAR = "برنامج الكيمياء العضوية",
                        NameEN = "Organic Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 75,
                        NameAR = "برنامج الكيمياء التحليلية",
                        NameEN = "Analytical Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 76,
                        NameAR = "برنامج الكيمياء الفيزيائية",
                        NameEN = "Physical Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 77,
                        NameAR = "برنامج الكيمياء الحيوية",
                        NameEN = "Biochemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 78,
                        NameAR = "برنامج الكيمياء التطبيقية",
                        NameEN = "Applied Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 79,
                        NameAR = "برنامج التكنولوجيا الحيوية والبيولوجيا الجزئية",
                        NameEN = "Biotechnology and Molecular Biology Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 80,
                        NameAR = "برنامج علم الحيوان",
                        NameEN = "Zoology Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 81,
                        NameAR = "برنامج علم الحيوان والكيمياء",
                        NameEN = "Zoology and Chemistry Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 82,
                        NameAR = "برنامج الفيزياء",
                        NameEN = "Physics Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 83,
                        NameAR = "برنامج الفيزياء الحيوية الطبية",
                        NameEN = "Medical Biophysics Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 84,
                        NameAR = "برنامج علوم الفضاء",
                        NameEN = "Space Science Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 85,
                        NameAR = "برنامج الرياضيات",
                        NameEN = "Mathematics Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 86,
                        NameAR = "برنامج الرياضيات والحاسب",
                        NameEN = "Mathematics and Computer Science Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 87,
                        NameAR = "برنامج الاحصاء والحاسب",
                        NameEN = "Statistics and Computer Science Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 88,
                        NameAR = "برنامج التكنولوجيا الحيوية الجزئية",
                        NameEN = "Statistics Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 89,
                        NameAR = "برنامج الوراثة والمناعة التطبيقية",
                        NameEN = "Applied Genetics and Immunology Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 90,
                        NameAR = "برنامج البترول والمعادن",
                        NameEN = "Petroleum and Minerals Program",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 91,
                        NameAR = "دبلومة الفسيولوجي والتحاليل الطبية",
                        NameEN = "Diploma in Physiology and Medical Analysis",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 92,
                        NameAR = "شعبة الفسيولوجي والبيئة",
                        NameEN = "Physiology and Environment",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 93,
                        NameAR = "شعبة اللافقاريات والطفيليات والمناعة",
                        NameEN = "Invertebrates, Parasitology, and Immunology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 94,
                        NameAR = "شعبة الخلية والأنسجة والوراثة",
                        NameEN = "Cell, Tissue, and Genetics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 95,
                        NameAR = "شعبة التشريح المقارن والأجنة",
                        NameEN = "Comparative Anatomy and Embryology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 96,
                        NameAR = "شعبة الحشرات",
                        NameEN = "Entomology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 97,
                        NameAR = "فيزياء الجوامد التطبيقية",
                        NameEN = "Applied Solid State Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 98,
                        NameAR = "فيزياء الالكترونيات",
                        NameEN = "Electronics Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 99,
                        NameAR = "البصريات والليزر والأطياف الذرية",
                        NameEN = "Optics, Laser, and Atomic Spectroscopy",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 100,
                        NameAR = "الفيزياء النووية التطبيقية",
                        NameEN = "Applied Nuclear Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 101,
                        NameAR = "الفيزياء الاشعاعية التطبيقية",
                        NameEN = "Applied Radiation Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 102,
                        NameAR = "الفيزياء الحيوية الطبية",
                        NameEN = "Medical Biophysics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 103,
                        NameAR = "فيزياء الفلك وعلوم الفضاء",
                        NameEN = "Astronomy and Space Sciences",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 104,
                        NameAR = "الفيزياء النظرية",
                        NameEN = "Theoretical Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 105,
                        NameAR = "الرياضيات البحتة",
                        NameEN = "Pure Mathematics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 106,
                        NameAR = "الرياضيات التطبيقية",
                        NameEN = "Applied Mathematics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 107,
                        NameAR = "الاحصاء",
                        NameEN = "Statistics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 108,
                        NameAR = "علوم الحاسب",
                        NameEN = "Computer Science",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 109,
                        NameAR = "دكتوراه التكنولوجيا الحيوية الجزئية",
                        NameEN = "PhD in Molecular Biotechnology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 110,
                        NameAR = "ماجستير التكنولوجيا الحيوية الجزئية",
                        NameEN = "Master in Molecular Biotechnology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 111,
                        NameAR = "ماجستير الفيزياء الطبية التطبيقية",
                        NameEN = "Master in Applied Medical Physics",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 112,
                        NameAR = "الدبلوم المهني في تحاليل الكيمياء الحيوية",
                        NameEN = "Professional Diploma in Biochemical Analysis",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 113,
                        NameAR = "الدبلوم المهني في الميكروبيولوجيا التطبيقية",
                        NameEN = "Professional Diploma in Applied Microbiology",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 114,
                        NameAR = "الدبلوم المهني في الفسيولوجي والتحاليل المعملية",
                        NameEN = "Professional Diploma in Physiology and Laboratory Analysis",
                        FacultyId = 7
                    },
                    new Department
                    {
                        Id = 115,
                        NameAR = "قسم النحت",
                        NameEN = "Sculpture Department",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 116,
                        NameAR = "قسم العمارة",
                        NameEN = "Architecture Department",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 117,
                        NameAR = "قسم الجرافيك",
                        NameEN = "Graphic Design Department",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 118,
                        NameAR = "قسم التصوير",
                        NameEN = "Photography Department",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 119,
                        NameAR = "قسم الديكور",
                        NameEN = "Department of Decoration",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 120,
                        NameAR = "قسم تاريخ الفن",
                        NameEN = "Art History Department",
                        FacultyId = 8
                    },
                    new Department
                    {
                        Id = 121,
                        NameAR = "الفوتوغرافيا والسينما والتليفزيون",
                        NameEN = "Photography, Cinema, and Television Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 122,
                        NameAR = "قسم الملابس الجاهزة",
                        NameEN = "Ready-made Clothing Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 123,
                        NameAR = "قسم طباعة المسنوجات والصباغة والتجهيز",
                        NameEN = "Textile Printing, Dyeing, and Finishing Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 124,
                        NameAR = "قسم الطباعة والنشر والتغليف",
                        NameEN = "Printing, Publishing, and Packaging Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 125,
                        NameAR = "قسم الاعلان",
                        NameEN = "Advertising Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 126,
                        NameAR = "قسم التصميم الداخلي والاثاث",
                        NameEN = "Interior Design and Furniture Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 127,
                        NameAR = "قسم التصميم الصناعي",
                        NameEN = "Industrial Design Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 128,
                        NameAR = "قسم الاثاث والانشاءات المعدنية",
                        NameEN = "Furniture and Metal Constructions Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 129,
                        NameAR = "قسم المنتجات المعدنية والحلي",
                        NameEN = "Department of Metal Products and Jewelry",
                        FacultyId = 9

                    },
                    new Department
                    {
                        Id = 130,
                        NameAR = "قسم الخزف",
                        NameEN = "Ceramics Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 131,
                        NameAR = "قسم الزجاج",
                        NameEN = "Glass Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 132,
                        NameAR = "قسم الزخرفة",
                        NameEN = "Decoration Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 133,
                        NameAR = "قسم النحت والتشكيل المعماري",
                        NameEN = "Sculpture and Architectural Formation Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 134,
                        NameAR = "قسم طباعة المنسوجات والصباغة والتجهيز",
                        NameEN = "Textile Printing, Dyeing, and Finishing Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 135,
                        NameAR = "قسم الوسائط المطبوعة",
                        NameEN = "Print Media Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 136,
                        NameAR = "قسم علوم التغليف",
                        NameEN = "Packaging Science Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 137,
                        NameAR = "قسم علوم تصميم الاثاث",
                        NameEN = "Furniture Design Science Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 138,
                        NameAR = "قسم تصميم وتشكيل الزجاج في العمارة",
                        NameEN = "Glass Design and Formation in Architecture Department",
                        FacultyId = 9
                    },
                    new Department
                    {
                        Id = 139,
                        NameAR = "قسم المحاسبة",
                        NameEN = "Accounting Department",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 140,
                        NameAR = "قسم الاقتصاد والتجارة الخارجية",
                        NameEN = "Department of Economics and Foreign Trade",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 141,
                        NameAR = "قسم ادارة الاعمال",
                        NameEN = "Business Administration Department",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 142,
                        NameAR = "قسم الرياضة والتأمين والاحصاء",
                        NameEN = "Department of Mathematics, Insurance, and Statistics",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 143,
                        NameAR = "قسم العلوم السياسية",
                        NameEN = "Department of Political Science",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 144,
                        NameAR = "قسم نظم المعلومات",
                        NameEN = "Department of Information Systems",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 145,
                        NameAR = "شعبة اللغات",
                        NameEN = "Language Division",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 146,
                        NameAR = "قسم نظم معلومات الاعمال",
                        NameEN = "Business Information Systems Department",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 147,
                        NameAR = "قسم الاسواق والمنشأت المالية",
                        NameEN = "Financial Markets and Institutions Department",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 148,
                        NameAR = "الدراسات العليا الاكاديمية",
                        NameEN = "Academic Graduate Studies",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 149,
                        NameAR = "ادارة المستشفيات واقتصاديات الصحة",
                        NameEN = "Hospital Management and Health Economics",
                        FacultyId = 10
                    },
                    new Department
                    {
                        Id = 150,
                        NameAR = "قسم هندسة الالكترونيات والاتصالات",
                        NameEN = "Department of Electronics and Communications Engineering",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 151,
                        NameAR = "قسم هندسة الحاسبات والنظم",
                        NameEN = "Computer Engineering Department",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 152,
                        NameAR = "قسم هندسة القوى الكهربائية والآلات",
                        NameEN = "Department of Electrical Power and Machines Engineering",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 153,
                        NameAR = "قسم الهندسة الميكانيكية",
                        NameEN = "Mechanical Engineering Department",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 154,
                        NameAR = "قسم الهندسة الحيوية الطبية",
                        NameEN = "Biomedical and Medical Engineering Department",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 155,
                        NameAR = "برنامج هندسة الانتاج",
                        NameEN = "Production Engineering Program",
                        FacultyId = 11

                    },
                    new Department
                    {
                        Id = 156,
                        NameAR = "برنامج الهندسة الصناعية",
                        NameEN = "Industrial Engineering Program",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 157,
                        NameAR = "برنامج هندسة الميكاترونيك",
                        NameEN = "Mechatronics Engineering Program",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 158,
                        NameAR = "برنامج هندسة الاتصالات والمعلومات",
                        NameEN = "Telecommunications and Information Engineering Program",
                        FacultyId = 11

                    },
                    new Department
                    {
                        Id = 159,
                        NameAR = "برنامج هندسة القوى والوقاية الكهربية",
                        NameEN = "Electrical Power and Protection Engineering Program",
                        FacultyId = 11
                    },
                    new Department
                    {
                        Id = 160,
                        NameAR = "قسم الهندسة المدنية",
                        NameEN = "Civil Engineering Department",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 161,
                        NameAR = "قسم هندسة القوى الميكانيكية",
                        NameEN = "Mechanical Power Engineering Department",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 162,
                        NameAR = "قسم السيارات والجرارات",
                        NameEN = "Architectural Engineering Department",
                        FacultyId = 12

                    },
                    new Department
                    {
                        Id = 163,
                        NameAR = "قسم الهندسة المعمارية",
                        NameEN = "Architectural Engineering Department",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 164,
                        NameAR = "قسم التصميم الميكانيكي",
                        NameEN = "Mechanical Design Engineering Department",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 165,
                        NameAR = "قسم الفيزيقيا والرياضيات",
                        NameEN = "Physics and Mathematics Department",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 166,
                        NameAR = "برنامج هندسة الطاقة",
                        NameEN = "Energy Engineering Program",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 167,
                        NameAR = "برنامج الهندسة الانشائية",
                        NameEN = "Construction Engineering Program",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 168,
                        NameAR = "برنامج العمارة بالتكنولوجيا الرقمية",
                        NameEN = "Digital Architecture Program",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 169,
                        NameAR = "برنامج هندسة الميكاترونيات بالسيارات",
                        NameEN = "Automotive Mechatronics Engineering Program",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 170,
                        NameAR = "برنامج ادارة المشروعات والتشييد",
                        NameEN = "Project Management and Construction Program",
                        FacultyId = 12
                    },
                    new Department
                    {
                        Id = 171,
                        NameAR = "قسم الملكية الصناعية",
                        NameEN = "Industrial Property Department",
                        FacultyId = 13
                    },
                    new Department
                    {
                        Id = 172,
                        NameAR = "قسم الملكية الادبية والفنية",
                        NameEN = "Project Management and Construction Department",
                        FacultyId = 13
                    },
                    new Department
                    {
                        Id = 173,
                        NameAR = "قسم التصميمات الزخرفية",
                        NameEN = "Decorative Designs Department",
                        FacultyId = 14

                    },
                    new Department
                    {
                        Id = 174,
                        NameAR = "قسم الرسم والتصوير",
                        NameEN = "Drawing and Painting Department",
                        FacultyId = 14
                    },
                    new Department
                    {
                        Id = 175,
                        NameAR = "قسم النقد والتذوق الفني",
                        NameEN = "Art Criticism and Appreciation Department",
                        FacultyId = 14
                    },
                    new Department
                    {
                        Id = 176,
                        NameAR = "قسم علوم التربية الفنية",
                        NameEN = "Art Education Department",
                        FacultyId = 14
                    },
                    new Department
                    {
                        Id = 177,
                        NameAR = "قسم التعبير المجسم",
                        NameEN = "Sculptural Expression Department",
                        FacultyId = 14
                    },
                    new Department
                    {
                        Id = 178,
                        NameAR = "قسم الاشغال الفنية والتراث الشعبي",
                        NameEN = "Art and Folk Heritage Department",
                        FacultyId = 14
                    },
                    new Department
                    {
                        Id = 179,
                        NameAR = "قسم العلوم التربوية والنفسية والاجتماعية الرياضية",
                        NameEN = "Educational, Psychological, and Social Sciences Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 180,
                        NameAR = "قسم العلوم الحيوية والصحة الرياضية",
                        NameEN = "Biological and Sports Health Sciences Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 181,
                        NameAR = "قسم المناهج وطرق تدريس التربية البدنية",
                        NameEN = "Curriculum and Methods of Physical Education Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 182,
                        NameAR = "قسم علوم الادارة الرياضية والترويح",
                        NameEN = "Sports Management and Recreation Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 183,
                        NameAR = "قسم نظريات وتطبيقات المنازلات والرياضات الفردية",
                        NameEN = "Theories and Applications of Individual Sports Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 184,
                        NameAR = "قسم نظريات وتطبيقات الرياضات المائية",
                        NameEN = "Theories and Applications of Aquatic Sports Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 185,
                        NameAR = "قسم نظريات وتطبيقات الرياضات الجماعية وألعاب المضرب",
                        NameEN = "Theories and Applications of Team Sports and Racket Games Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 186,
                        NameAR = "قسم نظريات وتطبيقات العاب القوى",
                        NameEN = "Theories and Applications of Athletics Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 187,
                        NameAR = "قسم نظريات وتطبيقات التعبير الحركي والايقاع الحركي",
                        NameEN = "Theories and Applications of Motor Expression and Rhythmic Movement Department",
                        FacultyId = 15

                    },
                    new Department
                    {
                        Id = 188,
                        NameAR = "قسم نظريات وتطبيقات التمرينات والجمباز",
                        NameEN = "Theories and Applications of Gymnastics and Exercises Department",
                        FacultyId = 15
                    },
                    new Department
                    {
                        Id = 189,
                        NameAR = "قسم الادارة الرياضية",
                        NameEN = "Sports Management Department",
                        FacultyId = 16
                    },
                    new Department
                    {
                        Id = 190,
                        NameAR = "قسم الترويح الرياضي",
                        NameEN = "Sports Recreation Department",
                        FacultyId = 16
                    },
                    new Department
                    {
                        Id = 191,
                        NameAR = "قسم المناهج وطرق التدريس",
                        NameEN = "Curriculum and Teaching Methods Department",
                        FacultyId = 16
                    },
                    new Department
                    {
                        Id = 192,
                        NameAR = "قسم علوم الحركة الرياضية",
                        NameEN = "Sports Movement Sciences Department",
                        FacultyId = 16
                    },
                     new Department
                     {
                         Id = 193,
                         NameAR = "قسم علوم الصحة الرياضية",
                         NameEN = "Sports Health Sciences Department",
                         FacultyId = 16
                     },
                     new Department
                     {
                         Id = 194,
                         NameAR = "قسم علوم النفس الرياضي",
                         NameEN = "Sports Psychology Sciences Department",
                         FacultyId = 16
                     },
                     new Department
                     {
                         Id = 195,
                         NameAR = "قسم تدريب الرياضات الاساسية",
                         NameEN = "Basic Sports Training Department",
                         FacultyId = 16
                     },
                     new Department
                     {
                         Id = 196,
                         NameAR = "قسم تدريب الرياضات الفردية",
                         NameEN = "Individual Sports Training Department",
                         FacultyId = 16
                     },
                        new Department
                        {
                            Id = 197,
                            NameAR = "قسم تدريب الرياضات الجماعية",
                            NameEN = "Team Sports Training Department",
                            FacultyId = 16
                        },
                        new Department
                        {
                            Id = 198,
                            NameAR = "قسم رياضة كبار السن",
                            NameEN = "Elderly Sports Department",
                            FacultyId = 16
                        },
                        new Department
                        {
                            Id = 199,
                            NameAR = "قسم التربية الرياضية المعدلة",
                            NameEN = "Department of Adapted Physical Education",
                            FacultyId = 16
                        },
                        new Department
                        {
                            Id = 200,
                            NameAR = "قسم النظريات والتأليف",
                            NameEN = "Theories and Composition Department",
                            FacultyId = 17
                        },
                        new Department
                        {
                            Id = 201,
                            NameAR = "قسم الموسيقى العربية",
                            NameEN = "Arabic Music Department",
                            FacultyId = 17
                        },
                        new Department
                        {
                            Id = 202,
                            NameAR = "قسم البيانو والمصاحبة",
                            NameEN = "Piano and Accompaniment Department",
                            FacultyId = 17
                        },
                        new Department
                        {
                            Id = 203,
                            NameAR = "قسم الاداء",
                            NameEN = "Performance Department",
                            FacultyId = 17
                        },
                        new Department
                        {
                            Id = 204,
                            NameAR = "قسم العلوم الموسيقية التربوية",
                            NameEN = "Department of Music Education",
                            FacultyId = 17
                        },
                        new Department
                        {
                            Id = 205,
                            NameAR = "قسم الدراسات السياحية",
                            NameEN = "Tourism Studies Department",
                            FacultyId = 18
                        },
                        new Department
                        {
                            Id = 206,
                            NameAR = "قسم الدراسات الفندقية",
                            NameEN = "Hotel Studies Department",
                            FacultyId = 18
                        },
                        new Department
                        {
                            Id = 207,
                            NameAR = "قسم الارشاد السياحي",
                            NameEN = "Tourism Guidance Department",
                            FacultyId = 18
                        },
                        new Department
                        {
                            Id = 208,
                            NameAR = "قسم ادارة المطاعم",
                            NameEN = "Restaurant Management Department",
                            FacultyId = 18
                        },
                        new Department
                        {
                            Id = 209,
                            NameAR = "قسم علوم الأغذية",
                            NameEN = "Food Science Department",
                            FacultyId = 24

                        },
                        new Department
                        {
                            Id = 210,
                            NameAR = "قسم التغذية العلاجية",
                            NameEN = "Therapeutic Nutrition Department",
                            FacultyId = 24
                        },
                        new Department
                        {
                            Id = 211,
                            NameAR = "قسم تغذية المجتمع",
                            NameEN = "Community Nutrition Department",
                            FacultyId = 24
                        },
                        new Department
                        {
                            Id = 212,
                            NameAR = "قسم تكنولوجيا الميكانيكا",
                            NameEN = "Mechanical Technology Department",
                            FacultyId = 22
                        },
                        new Department
                        {
                            Id = 213,
                            NameAR = "قسم تكنولوجيا الالكترونيات والاتصالات",
                            NameEN = "Electronics and Communications Technology Department",
                            FacultyId = 22
                        },
                        new Department
                        {
                            Id = 214,
                            NameAR = "قسم تكنولوجيا التشييد والبناء",
                            NameEN = "Construction Technology Department",
                            FacultyId = 22

                        },
                        new Department
                        {
                            Id = 215,
                            NameAR = " قسم تكنولوجيا السيارات",
                            NameEN = "Automotive Technology Department",
                            FacultyId = 22
                        },
                        new Department
                        {
                            Id = 216,
                            NameAR = "قسم العلوم التربوية والنفسية",
                            NameEN = "Educational and Psychological Sciences Department",
                            FacultyId = 22
                        },
                        new Department
                        {
                            Id = 217,
                            NameAR = "قسم المناهج وطرق التدريس",
                            NameEN = "Curriculum and Instruction Department",
                            FacultyId = 22
                        },
                        new Department
                        {
                            Id = 218,
                            NameAR = "القانون الجنائي",
                            NameEN = "Criminal Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 219,
                            NameAR = "القانون التجاري",
                            NameEN = "Commercial Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 220,
                            NameAR = "قانون العمل والتشريعات الاجتماعية",
                            NameEN = "Labor and Social Legislation Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 221,
                            NameAR = "الشريعة الإسلامية",
                            NameEN = "Islamic Sharia",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 222,
                            NameAR = "القانون المدني",
                            NameEN = "Civil Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 223,
                            NameAR = "القانون الدولي العام",
                            NameEN = "Public International Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 224,
                            NameAR = "فلسفة القانون وتاريخه",
                            NameEN = "Philosophy and History of Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 225,
                            NameAR = "الاقتصاد",
                            NameEN = "Economics",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 226,
                            NameAR = "القانون العام",
                            NameEN = "Public Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 227,
                            NameAR = "قانون المرافعات",
                            NameEN = "Procedural Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 228,
                            NameAR = "القانون الدولي الخاص",
                            NameEN = "Private International Law",
                            FacultyId = 21
                        },
                        new Department
                        {
                            Id = 278,
                            NameAR = "برنامج الدراسات القانونية باللغة الانجليزية",
                            NameEN = "Legal Studies Program in English",
                            FacultyId = 21

                        },
                        new Department
                        {
                            Id = 229,
                            NameAR = "برنامج الدراسات القانونية باللغة الفرنسية",
                            NameEN = "Legal Studies Program in French",
                            FacultyId = 21
                        },
                        new Department { Id = 230, NameAR = "قسم التشريح وعلم الأجنة", NameEN = "Department of Anatomy and Embryology", FacultyId = 20 },
                        new Department { Id = 231, NameAR = "قسم الفسيولوجيا الطبية", NameEN = "Department of Medical Physiology", FacultyId = 20 },
                        new Department { Id = 232, NameAR = "قسم الكيمياء الحيوية الطبية والبيولوجيا الجزئية", NameEN = "Department of Medical Biochemistry and Molecular Biology", FacultyId = 20 },
                        new Department { Id = 233, NameAR = "قسم الهستولوجي", NameEN = "Department of Histology", FacultyId = 20 },
                        new Department { Id = 234, NameAR = "قسم الباثولوجيا", NameEN = "Department of Pathology", FacultyId = 20 },
                        new Department { Id = 235, NameAR = "قسم الباثولوجيا الإكلينيكية والكيميائية", NameEN = "Department of Clinical and Chemical Pathology", FacultyId = 20 },
                        new Department { Id = 236, NameAR = "قسم جراحة المسالك البولية", NameEN = "Department of Urology", FacultyId = 20 },
                        new Department { Id = 237, NameAR = "قسم جراحة التجميل", NameEN = "Department of Plastic Surgery", FacultyId = 20 },
                        new Department { Id = 238, NameAR = "قسم طب الحالات الحرجة والطوارئ", NameEN = "Department of Critical Care and Emergency Medicine", FacultyId = 20 },
                        new Department { Id = 239, NameAR = "قسم الفارماكولوجيا الطبية", NameEN = "Department of Medical Pharmacology", FacultyId = 20 },
                        new Department { Id = 240, NameAR = "قسم الميكروبيولوجيا الطبية والمناعة", NameEN = "Department of Medical Microbiology and Immunology", FacultyId = 20 },
                        new Department { Id = 241, NameAR = "قسم الطفيليات الطبية", NameEN = "Department of Medical Parasitology", FacultyId = 20 },
                        new Department { Id = 242, NameAR = "قسم طب الأسرة", NameEN = "Department of Family Medicine", FacultyId = 20 },
                        new Department { Id = 243, NameAR = "قسم طب المجتمع والبيئة وطب الصناعات", NameEN = "Department of Community, Environmental and Occupational Medicine", FacultyId = 20 },
                        new Department { Id = 244, NameAR = "قسم التخدير والعناية المركزة وعلاج الألم", NameEN = "Department of Anesthesia, Intensive Care and Pain Management", FacultyId = 20 },
                        new Department { Id = 245, NameAR = "قسم التوليد وأمراض النساء", NameEN = "Department of Obstetrics and Gynecology", FacultyId = 20 },
                        new Department { Id = 246, NameAR = "قسم جراحة العظام", NameEN = "Department of Orthopedic Surgery", FacultyId = 20 },
                        new Department { Id = 247, NameAR = "قسم جراحة الأوعية الدموية", NameEN = "Department of Vascular Surgery", FacultyId = 20 },
                        new Department { Id = 248, NameAR = "قسم الطب الشرعي والسموم الإكلينيكية", NameEN = "Department of Forensic Medicine and Clinical Toxicology", FacultyId = 20 },
                        new Department { Id = 249, NameAR = "قسم أمراض الباطنة العامة", NameEN = "Department of General Internal Medicine", FacultyId = 20 },
                        new Department { Id = 250, NameAR = "قسم الأطفال", NameEN = "Department of Pediatrics", FacultyId = 20 },
                        new Department { Id = 251, NameAR = "قسم الجراحة العامة", NameEN = "Department of General Surgery", FacultyId = 20 },
                        new Department { Id = 252, NameAR = "قسم الأمراض الصدرية", NameEN = "Department of Chest Diseases", FacultyId = 20 },
                        new Department { Id = 253, NameAR = "قسم الأشعة التشخيصية والعلاجية", NameEN = "Department of Diagnostic and Therapeutic Radiology", FacultyId = 20 },
                        new Department { Id = 254, NameAR = "قسم طب وجراحة العيون", NameEN = "Department of Ophthalmology", FacultyId = 20 },
                        new Department { Id = 255, NameAR = "قسم طب المخ والأعصاب والطب النفسي", NameEN = "Department of Neurology and Psychiatry", FacultyId = 20 },
                        new Department { Id = 256, NameAR = "قسم طب وصحة المسنين", NameEN = "Department of Geriatric Medicine", FacultyId = 20 },
                        new Department { Id = 257, NameAR = "قسم الأمراض الجلدية والتناسلية والذكورة", NameEN = "Department of Dermatology, Venereology and Andrology", FacultyId = 20 },
                        new Department { Id = 258, NameAR = "قسم جراحة المخ والأعصاب", NameEN = "Department of Neurosurgery", FacultyId = 20 },
                        new Department { Id = 259, NameAR = "قسم الأمراض المتوطنة", NameEN = "Department of Endemic Diseases", FacultyId = 20 },
                        new Department { Id = 260, NameAR = "قسم أمراض القلب والأوعية الدموية", NameEN = "Department of Cardiovascular Diseases", FacultyId = 20 },
                        new Department { Id = 261, NameAR = "قسم الطب الطبيعي والروماتيزم والتأهيل", NameEN = "Department of Physical Medicine, Rheumatology and Rehabilitation", FacultyId = 20 },
                        new Department { Id = 262, NameAR = "قسم علاج الأورام والطب النووي", NameEN = "Department of Oncology and Nuclear Medicine", FacultyId = 20 },
                        new Department { Id = 263, NameAR = "قسم الأنف والأذن والحنجرة", NameEN = "Department of Ear, Nose and Throat", FacultyId = 20 },
                        new Department { Id = 264, NameAR = "قسم جراحة القلب والصدر", NameEN = "Department of Cardiothoracic Surgery", FacultyId = 20 },
                        new Department { Id = 265, NameAR = "قسم جراحة الأطفال", NameEN = "Department of Pediatric Surgery", FacultyId = 20 },
                        new Department { Id = 266, NameAR = "قسم العقاقير", NameEN = "Department of Pharmacognosy", FacultyId = 19 },
                        new Department { Id = 267, NameAR = "قسم الصيدلانيات والصيدلة الصناعية", NameEN = "Department of Pharmaceutics and Industrial Pharmacy", FacultyId = 19 },
                        new Department { Id = 268, NameAR = "قسم ممارسة الصيدلة", NameEN = "Department of Pharmacy Practice", FacultyId = 19 },
                        new Department { Id = 269, NameAR = "قسم الأدوية والسموم", NameEN = "Department of Pharmacology and Toxicology", FacultyId = 19 },
                        new Department { Id = 270, NameAR = "قسم الكيمياء الصيدلية", NameEN = "Department of Pharmaceutical Chemistry", FacultyId = 19 },
                        new Department { Id = 271, NameAR = "قسم الكيمياء التحليلية الصيدلية", NameEN = "Department of Pharmaceutical Analytical Chemistry", FacultyId = 19 },
                        new Department { Id = 272, NameAR = "قسم الكيمياء العضوية الصيدلية", NameEN = "Department of Pharmaceutical Organic Chemistry", FacultyId = 19 },
                        new Department { Id = 273, NameAR = "قسم الكيمياء الحيوية والبيولوجيا الجزيئية", NameEN = "Department of Biochemistry and Molecular Biology", FacultyId = 19 },
                        new Department { Id = 274, NameAR = "قسم الميكروبيولوجيا والمناعة", NameEN = "Department of Microbiology and Immunology", FacultyId = 19 }
                );

            #endregion
        }
    }
}
