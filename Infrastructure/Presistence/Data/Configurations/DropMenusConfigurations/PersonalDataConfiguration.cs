
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
            }

            #endregion

            );

        }
    }
}
