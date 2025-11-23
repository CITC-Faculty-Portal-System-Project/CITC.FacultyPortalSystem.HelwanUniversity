namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class RewardsModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(

            #region Awards

                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة الدولة التقديرية",
                       ValueEn = "State Appreciation Award",
                       SortOrder = 1,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303031"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة الدولة للتفوق العلم",
                       ValueEn = "State Award for Scientific Excellence",
                       SortOrder = 2,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303032"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة الدولة التشجيعية",
                       ValueEn = "State Incentive Award",
                       SortOrder = 3,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303033"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة النيل التشجيعية",
                       ValueEn = "Nile Encouragement Award",
                       SortOrder = 4,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303034"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة الجامعة التقديرية",
                       ValueEn = "University Appreciation Award",
                       SortOrder = 5,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303035"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة المنصورة الطبية",
                       ValueEn = "Mansoura Medical Award",
                       SortOrder = 6,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303036"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة احسن رسالة دكتوراه",
                       ValueEn = "Best PhD Dissertation Award",
                       SortOrder = 7,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },
                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303037"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة احسن رسالة ماجستير",
                       ValueEn = "Best Master's Thesis Award",
                       SortOrder = 8,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },

                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303038"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "جائزة عبد الحميد شومان",
                       ValueEn = "Abdul Hameed Shoman Award",
                       SortOrder = 9,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   },

                   new Lookup
                   {
                       Id = Guid.Parse("30303030-3030-3030-3030-303030303039"),
                       Type = LookupTypes.Rewards.ToString(),
                       Key = "REWARDS",
                       ValueAr = "اخرى",
                       ValueEn = "Other",
                       SortOrder = 10,
                       CreatedBy = "Helwan Faculty Portal System",
                       CreatedAt = new DateTime(2025, 11, 22)
                   }

                   #endregion

             );
        }
    }
}
