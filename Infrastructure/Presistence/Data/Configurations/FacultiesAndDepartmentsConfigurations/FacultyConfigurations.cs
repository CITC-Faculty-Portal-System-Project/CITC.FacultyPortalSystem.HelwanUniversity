using Domain.Entities.UniversityFacultiesAndDepartments;

namespace Presistence.Data.Configurations.FacultiesAndDepartmentsConfigurations
{
    public class FacultyConfigurations : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            #region AddingIndcies

            builder.HasIndex(f => f.Id);
            builder.HasIndex(f => f.NameAR);
            builder.HasIndex(f => f.NameEN);

            #endregion

            #region ConfiguringRelations

            builder.HasMany(f => f.Departments)
                .WithOne(d => d.Faculty)
                .HasForeignKey(f => f.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.FacultyMembersPersonalData)
                .WithOne(fm => fm.Faculty)
                .HasForeignKey(fm => fm.FacultyId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region DataSeeding

            builder.HasData(
                new Faculty
                {
                    Id = 1,
                    NameAR = "كلية التربية",
                    NameEN = "Faculty of Education"
                },
                new Faculty
                {
                    Id = 2,
                    NameAR = "كلية الاقتصاد المنزلي",
                    NameEN = "Faculty of Home Economics"
                },
                new Faculty
                {
                    Id = 3,
                    NameAR = "كلية الاداب",
                    NameEN = "Faculty of Arts"
                },
                new Faculty
                {
                    Id = 4,
                    NameAR = "كلية الخدمة الاجتماعية",
                    NameEN = "Faculty of Social Work"
                },
                new Faculty
                {
                    Id = 5,
                    NameAR = "كلية الحاسبات والذكاء الاصطناعي",
                    NameEN = "Faculty of Computer Science and Artificial Intelligence"
                },
                new Faculty
                {
                    Id = 6,
                    NameAR = "كلية التمريض",
                    NameEN = "Faculty of Nursing"
                },
                new Faculty
                {
                    Id = 7,
                    NameAR = "كلية العلوم",
                    NameEN = "Faculty of Science"
                },
                new Faculty
                {
                    Id = 8,
                    NameAR = "كلية الفنون الجميلة",
                    NameEN = "Faculty of Fine Arts"
                },
                new Faculty
                {
                    Id = 9,
                    NameAR = "كلية الفنون التطبيقية",
                    NameEN = "Faculty of Applied Arts"
                },
                new Faculty
                {
                    Id = 10,
                    NameAR = "كلية التجارة وادارة الاعمال",
                    NameEN = "Faculty of Commerce and Business Administration"
                },
                new Faculty
                {
                    Id = 11,
                    NameAR = "(كلية الهندسة (حلوان",
                    NameEN = "Faculty of Engineering (Helwan)"
                },
                new Faculty
                {
                    Id = 12,
                    NameAR = "(كلية الهندسة (المطرية",
                    NameEN = "Faculty of Engineering (Mataria)"
                },
                new Faculty
                {
                    Id = 13,
                    NameAR = "المعهد القومي للملكية الفكرية",
                    NameEN = "National Institute of Intellectual Property"
                },
                new Faculty
                {
                    Id = 14,
                    NameAR = "كلية التربية الفنية",
                    NameEN = "Faculty of Art Education"
                },
                new Faculty
                {
                    Id = 15,
                    NameAR = "(كلية علوم الرياضة (بنات",
                    NameEN = "Faculty of Sports Science (Girls)"
                },
                new Faculty
                {
                    Id = 16,
                    NameAR = "(كلية علوم الرياضة (بنين",
                    NameEN = "Faculty of Sports Science (Boys)"
                },
                new Faculty
                {
                    Id = 17,
                    NameAR = "كلية التربية الموسيقية",
                    NameEN = "Faculty of Music Education"
                },
                new Faculty
                {
                    Id = 18,
                    NameAR = "كلية السياحة والفنادق",
                    NameEN = "Faculty of Tourism and Hotels"
                },
                new Faculty
                {
                    Id = 19,
                    NameAR = "كلية الطب",
                    NameEN = "Faculty of Medicine"
                },
                new Faculty
                {
                    Id = 20,
                    NameAR = "كلية الصيدلة",
                    NameEN = "Faculty of Pharmacy"
                },
                new Faculty
                {
                    Id = 21,
                    NameAR = "كلية الحقوق",
                    NameEN = "Faculty of Law"
                },
                new Faculty
                {
                    Id = 22,
                    NameAR = "كلية التكنولوجيا والتعليم",
                    NameEN = "Faculty of Technology and Education"
                },
                new Faculty
                {
                    Id = 23,
                    NameAR = "معهد التمريض",
                    NameEN = "Technical Institute of Nursing"
                },
                new Faculty
                {
                    Id = 24,
                    NameAR = "كلية علوم التغذية",
                    NameEN = "Faculty of Nutrition Sciences"
                }
            );

            #endregion

        }
    }
}
