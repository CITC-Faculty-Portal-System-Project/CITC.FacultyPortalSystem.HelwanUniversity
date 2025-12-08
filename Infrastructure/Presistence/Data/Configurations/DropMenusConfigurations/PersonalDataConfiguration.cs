
namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class PersonalDataConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(
            
            #region UniversityDataSeeding

            new Lookup
                 {
                     Id = Guid.Parse("50505050-5050-5050-5050-505050500001"),
                     Type = LookupTypes.University.ToString(),
                     Key = "UNIVERSITY",
                     ValueAr = "جامعة القاهرة",
                     ValueEn = "Cairo University",
                     SortOrder = 1,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 12, 7)
                 },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500002"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة عين شمس",
                ValueEn = "Ain Shams University",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500003"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة حلوان",
                ValueEn = "Helwan University",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500004"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة الإسكندرية",
                ValueEn = "Alexandria University",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500005"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة المنصورة",
                ValueEn = "Mansoura University",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500006"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة طنطا",
                ValueEn = "Tanta University",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500007"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة أسيوط",
                ValueEn = "Assiut University",
                SortOrder = 7,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500008"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة الزقازيق",
                ValueEn = "Zagazig University",
                SortOrder = 8,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500009"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة السويس",
                ValueEn = "Suez University",
                SortOrder = 9,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500010"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة بورسعيد",
                ValueEn = "Port Said University",
                SortOrder = 10,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500011"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة الفيوم",
                ValueEn = "Fayoum University",
                SortOrder = 11,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500012"),
                Type = LookupTypes.University.ToString(),
                Key = "UNIVERSITY",
                ValueAr = "جامعة بنها",
                ValueEn = "Benha University",
                SortOrder = 12,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },




            #endregion

            #region GenderDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500013"),
                Type = LookupTypes.Gender.ToString(),
                Key = "GENDER",
                ValueAr = "ذكر",
                ValueEn = "Male",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500014"),
                Type = LookupTypes.Gender.ToString(),
                Key = "GENDER",
                ValueAr = "أنثى",
                ValueEn = "Female",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },

            #endregion

            #region BirthPlacesDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500016"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "القاهرة",
                ValueEn = "Cairo",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500017"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الجيزة",
                ValueEn = "Giza",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500018"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "القليوبية",
                ValueEn = "Qalyubia",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500019"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الإسكندرية",
                ValueEn = "Alexandria",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500020"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "البحيرة",
                ValueEn = "Beheira",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500021"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "مطروح",
                ValueEn = "Matrouh",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500022"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "كفر الشيخ",
                ValueEn = "Kafr El Sheikh",
                SortOrder = 7,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500023"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الدقهلية",
                ValueEn = "Dakahlia",
                SortOrder = 8,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500024"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "دمياط",
                ValueEn = "Damietta",
                SortOrder = 9,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500025"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الشرقية",
                ValueEn = "Sharqia",
                SortOrder = 10,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500026"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الغربية",
                ValueEn = "Gharbia",
                SortOrder = 11,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500027"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "المنوفية",
                ValueEn = "Monufia",
                SortOrder = 12,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500028"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "المنيا",
                ValueEn = "Minya",
                SortOrder = 13,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500029"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "بني سويف",
                ValueEn = "Beni Suef",
                SortOrder = 14,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500030"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الفيوم",
                ValueEn = "Fayoum",
                SortOrder = 15,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500031"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "أسيوط",
                ValueEn = "Assiut",
                SortOrder = 16,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500032"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "سوهاج",
                ValueEn = "Sohag",
                SortOrder = 17,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500033"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "قنا",
                ValueEn = "Qena",
                SortOrder = 18,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500034"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الأقصر",
                ValueEn = "Luxor",
                SortOrder = 19,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500035"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "أسوان",
                ValueEn = "Aswan",
                SortOrder = 20,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500036"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "البحر الأحمر",
                ValueEn = "Red Sea",
                SortOrder = 21,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500037"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "السويس",
                ValueEn = "Suez",
                SortOrder = 22,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500038"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الإسماعيلية",
                ValueEn = "Ismailia",
                SortOrder = 23,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500039"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "بورسعيد",
                ValueEn = "Port Said",
                SortOrder = 24,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500040"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "شمال سيناء",
                ValueEn = "North Sinai",
                SortOrder = 25,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500041"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "جنوب سيناء",
                ValueEn = "South Sinai",
                SortOrder = 26,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500042"),
                Type = LookupTypes.BirthPlace.ToString(),
                Key = "BIRTH_PLACE",
                ValueAr = "الوادي الجديد",
                ValueEn = "New Valley",
                SortOrder = 27,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },


            #endregion

            #region SocialStatusDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500043"),
                Type = LookupTypes.SocialStatus.ToString(),
                Key = "SOCIAL_STATUS",
                ValueAr = "أعزب",
                ValueEn = "Single",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500044"),
                Type = LookupTypes.SocialStatus.ToString(),
                Key = "SOCIAL_STATUS",
                ValueAr = "متزوج",
                ValueEn = "Married",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500045"),
                Type = LookupTypes.SocialStatus.ToString(),
                Key = "SOCIAL_STATUS",
                ValueAr = "مطلق",
                ValueEn = "Divorced",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500046"),
                Type = LookupTypes.SocialStatus.ToString(),
                Key = "SOCIAL_STATUS",
                ValueAr = "أرمل",
                ValueEn = "Widowed",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },

            #endregion

            #region TitlesDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500047"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "د.",
                ValueEn = "Dr.",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500048"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "أ.د.",
                ValueEn = "Prof. Dr.",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500049"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "أ.",
                ValueEn = "Prof.",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500050"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "م.د.",
                ValueEn = "Assistant Lecturer",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500051"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "م.",
                ValueEn = "Eng.",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500052"),
                Type = LookupTypes.Title.ToString(),
                Key = "TITLE",
                ValueAr = "بدون لقب",
                ValueEn = "No Title",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },

            #endregion

            #region FacultiesDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500060"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الهندسة - حلوان",
                ValueEn = "Faculty of Engineering - Helwan",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500061"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الهندسة بالمطرية",
                ValueEn = "Faculty of Engineering - Mataria",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500062"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الحاسبات والذكاء الاصطناعي",
                ValueEn = "Faculty of Computers and Artificial Intelligence",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500063"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التجارة وإدارة الأعمال",
                ValueEn = "Faculty of Commerce and Business Administration",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500064"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية السياحة والفنادق",
                ValueEn = "Faculty of Tourism and Hotels",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500065"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الفنون الجميلة",
                ValueEn = "Faculty of Fine Arts",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500066"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الفنون التطبيقية",
                ValueEn = "Faculty of Applied Arts",
                SortOrder = 7,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500067"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التربية",
                ValueEn = "Faculty of Education",
                SortOrder = 8,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500068"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التربية الفنية",
                ValueEn = "Faculty of Art Education",
                SortOrder = 9,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500069"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التربية الموسيقية",
                ValueEn = "Faculty of Music Education",
                SortOrder = 10,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500070"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الاقتصاد المنزلي",
                ValueEn = "Faculty of Home Economics",
                SortOrder = 11,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500071"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الآداب",
                ValueEn = "Faculty of Arts",
                SortOrder = 12,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500072"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية العلوم",
                ValueEn = "Faculty of Science",
                SortOrder = 13,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500073"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية الصيدلة",
                ValueEn = "Faculty of Pharmacy",
                SortOrder = 14,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500074"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التمريض",
                ValueEn = "Faculty of Nursing",
                SortOrder = 15,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500075"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التربية الرياضية بنين",
                ValueEn = "Faculty of Physical Education - Men",
                SortOrder = 16,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500076"),
                Type = LookupTypes.Faculty.ToString(),
                Key = "HELWAN_UNIVERSITY_FACULTIES",
                ValueAr = "كلية التربية الرياضية بنات",
                ValueEn = "Faculty of Physical Education - Women",
                SortOrder = 17,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },


            #endregion

            #region StudyFieldsSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500080"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "علوم البيانات",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500081"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "الذكاء الاصطناعي",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500082"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "هندسة البرمجيات",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500083"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "نظم المعلومات",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500084"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "علوم الحاسوب",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500085"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "الشبكات وأمن المعلومات",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500086"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "إدارة الأعمال",
                SortOrder = 7,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500087"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "المالية والمحاسبة",
                SortOrder = 8,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500088"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "التسويق الرقمي",
                SortOrder = 9,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500089"),
                Type = LookupTypes.StudyField.ToString(),
                Key = "STUDY_FIELDS",
                ValueAr = "الهندسة الكهربائية",
                SortOrder = 10,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },


            #endregion

            #region DepartmentsDataSeeding

            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500090"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم النظم الموزعة",
                SortOrder = 1,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500099"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم البرمجيات",
                SortOrder = 2,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500091"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم الذكاء الاصطناعي",
                SortOrder = 3,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500092"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم الشبكات وأمن المعلومات",
                SortOrder = 4,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500093"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم نظم المعلومات",
                SortOrder = 5,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500094"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم علوم البيانات",
                SortOrder = 6,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500095"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم هندسة البرمجيات",
                SortOrder = 7,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            },
            new Lookup
            {
                Id = Guid.Parse("50505050-5050-5050-5050-505050500096"),
                Type = LookupTypes.Department.ToString(),
                Key = "DEPARTMENTS",
                ValueAr = "قسم علوم الحاسوب",
                SortOrder = 8,
                CreatedBy = "Helwan Faculty Portal System",
                CreatedAt = new DateTime(2025, 12, 7)
            }


            #endregion

            );

        }
    }
}
