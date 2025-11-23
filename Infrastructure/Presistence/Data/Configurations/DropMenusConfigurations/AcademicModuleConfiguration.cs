namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class AcademicModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(

            #region Academic Qualifications

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "BACHELOR",
                   ValueAr = "ليسانس",
                   ValueEn = "Bachelor's degree",
                   SortOrder = 1,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "BACHELOR",
                   ValueAr = "بكالوريوس",
                   ValueEn = "Bachelor's",
                   SortOrder = 2,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم الدراسات العليا",
                   ValueEn = "Postgraduate Diploma",
                   SortOrder = 3,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "ماجستير",
                   ValueEn = "Master Degree",
                   SortOrder = 4,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "كورسات مكافئة للماجستير",
                   ValueEn = "Courses equivalent to a master's degree",
                   SortOrder = 5,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "الدكتوراه",
                   ValueEn = "PHD",
                   SortOrder = 6,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },

              new Lookup
              {
                  Id = Guid.Parse("11111111-1111-1111-1111-111111111117"),
                  Type = LookupTypes.AcademicQualification.ToString(),
                  Key = "HIGHER STUDIES",
                  ValueAr = "دكتوراة العلوم",
                  ValueEn = "Ph.D. of Science",
                  SortOrder = 7,
                  CreatedBy = "Helwan Faculty Portal System",
                  CreatedAt = new DateTime(2025, 11, 22)

              },

                new Lookup
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111118"),
                    Type = LookupTypes.AcademicQualification.ToString(),
                    Key = "HIGHER STUDIES",
                    ValueAr = "دكتوراة العلوم",
                    ValueEn = "Ph.D. of Science",
                    SortOrder = 8,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)

                },

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111119"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "العالمية",
                   ValueEn = "Global",
                   SortOrder = 9,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

                new Lookup
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111120"),
                    Type = LookupTypes.AcademicQualification.ToString(),
                    Key = "HIGHER STUDIES",
                    ValueAr = "الاجازة العالية",
                    ValueEn = "Higher Degree Qualification",
                    SortOrder = 10,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)

                },

                new Lookup
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                    Type = LookupTypes.AcademicQualification.ToString(),
                    Key = "HIGHER STUDIES",
                    ValueAr = "الزمالة",
                    ValueEn = "Fellowship",
                    SortOrder = 11,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)

                },

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم عام",
                   ValueEn = "General diploma",
                   SortOrder = 12,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111123"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم خاص",
                   ValueEn = "Special diploma",
                   SortOrder = 13,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111124"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم مهني",
                   ValueEn = "Professional diploma",
                   SortOrder = 14,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111125"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم تفرغ",
                   ValueEn = "Sabbatical diploma",
                   SortOrder = 15,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },


               new Lookup
               {
                   Id = Guid.Parse("11111111-1111-1111-1111-111111111126"),
                   Type = LookupTypes.AcademicQualification.ToString(),
                   Key = "HIGHER STUDIES",
                   ValueAr = "دبلوم تأهيلي",
                   ValueEn = "Qualifying diploma",
                   SortOrder = 16,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

                new Lookup
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111127"),
                    Type = LookupTypes.AcademicQualification.ToString(),
                    Key = "HIGHER STUDIES",
                    ValueAr = "الكانديدات",
                    ValueEn = "Candidae",
                    SortOrder = 17,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)

                },

            #endregion

            #region Academic Grade

               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222221"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "EXCELLENT",
                   ValueAr = "ممتاز مع مرتبة الشرف",
                   ValueEn = "Excellent with honors",
                   SortOrder = 1,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "EXCELLENT",
                   ValueAr = "ممتاز",
                   ValueEn = "Excellent",
                   SortOrder = 2,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222223"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "VERY GOOD",
                   ValueAr = "جيد جدا مع مرتبة الشرف",
                   ValueEn = "Very good with honors",
                   SortOrder = 3,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },
               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222224"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "VERY GOOD",
                   ValueAr = "جيد جدا",
                   ValueEn = "Very good",
                   SortOrder = 4,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222225"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "GOOD",
                   ValueAr = "جيد",
                   ValueEn = "Good",
                   SortOrder = 5,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222226"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "ACCEPTABLE",
                   ValueAr = "مقبول",
                   ValueEn = "Acceptable",
                   SortOrder = 6,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

               new Lookup
               {
                   Id = Guid.Parse("22222222-2222-2222-2222-222222222227"),
                   Type = LookupTypes.AcademicGrade.ToString(),
                   Key = "FAIL",
                   ValueAr = "راسب",
                   ValueEn = "Fail",
                   SortOrder = 7,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)

               },

            #endregion

            #region Dispatch

               new Lookup
               {
                   Id = Guid.Parse("33333333-3333-3333-3333-333333333331"),
                   Type = LookupTypes.Dispatch.ToString(),
                   Key = "MISSION",
                   ValueAr = "بعثة داخلية",
                   ValueEn = "Internal mission",
                   SortOrder = 1,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },


               new Lookup
               {
                   Id = Guid.Parse("33333333-3333-3333-3333-333333333332"),
                   Type = LookupTypes.Dispatch.ToString(),
                   Key = "MISSION",
                   ValueAr = "بعثة خارجية",
                   ValueEn = "Foreign mission",
                   SortOrder = 2,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                   Type = LookupTypes.Dispatch.ToString(),
                   Key = "SUPERVISION",
                   ValueAr = "اشراف مشترك",
                   ValueEn = "Joint supervision",
                   SortOrder = 3,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },

               new Lookup
               {
                   Id = Guid.Parse("33333333-3333-3333-3333-333333333334"),
                   Type = LookupTypes.Dispatch.ToString(),
                   Key = "SCHOLARSHIP",
                   ValueAr = "منحة شخصية",
                   ValueEn = "Personal scholarship",
                   SortOrder = 4,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },

            #endregion

            #region Employment Degrees

                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444441"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "معيد",
                    ValueEn = "Demonstrator",
                    SortOrder = 1,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },

                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444442"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "مدرس مساعد",
                    ValueEn = "Assistant teacher",
                    SortOrder = 2,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444443"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "مدرس",
                    ValueEn = "Teacher",
                    SortOrder = 3,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ مساعد",
                    ValueEn = "Assistant professor",
                    SortOrder = 4,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },

                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444445"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ",
                    ValueEn = "Professor",
                    SortOrder = 5,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444446"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ متفرغ",
                    ValueEn = "Full-time professor",
                    SortOrder = 6,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444447"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ غير متفرغ",
                    ValueEn = "Part-time professor",
                    SortOrder = 7,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444448"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "زميل",
                    ValueEn = "Peer",
                    SortOrder = 8,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444449"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استشاري مساعد",
                    ValueEn = "Assistant consultant",
                    SortOrder = 9,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444450"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استشاري",
                    ValueEn = "Consultative",
                    SortOrder = 10,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444451"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ مساعد متفرغ",
                    ValueEn = "Full-time Assistant Professor",
                    SortOrder = 11,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444452"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "مدرس متفرغ",
                    ValueEn = "Full time teacher",
                    SortOrder = 12,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444453"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ مساعد لقب علمي",
                    ValueEn = "Assistant Professor (academic title)",
                    SortOrder = 13,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444454"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ لقب علمي",
                    ValueEn = "Professor is a scientific title",
                    SortOrder = 14,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444455"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "مساعد باحث",
                    ValueEn = "Research assistant",
                    SortOrder = 15,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444456"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "باحث مساعد",
                    ValueEn = "Assistant researcher",
                    SortOrder = 16,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444457"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "باحث",
                    ValueEn = "Researcher",
                    SortOrder = 17,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444458"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "باحث اول",
                    ValueEn = "Senior researcher",
                    SortOrder = 18,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444459"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "رئيس بحوث",
                    ValueEn = "Head of Research",
                    SortOrder = 19,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444460"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ مساعد غير متفرغ",
                    ValueEn = "Part-time Assistant Professor",
                    SortOrder = 20,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444461"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "زميل متفرغ",
                    ValueEn = "Full-time colleague",
                    SortOrder = 21,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444462"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "مدرس غير متفرغ",
                    ValueEn = "Part-time teacher",
                    SortOrder = 22,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444463"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "استاذ مشارك",
                    ValueEn = "Associate Professor",
                    SortOrder = 23,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444464"),
                    Type = LookupTypes.EmploymentDegrees.ToString(),
                    Key = "EMPLOYEMENT",
                    ValueAr = "اخرى",
                    ValueEn = "Other",
                    SortOrder = 24,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                }

                #endregion

            );

        }
    }
}
