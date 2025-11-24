namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class ConferencesAndSeminarsModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {

            builder.HasData(

            #region Partcipation Role

                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555551"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "المخطط للمؤتمر",
                    ValueEn = "Conference planner",
                    SortOrder = 1,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555552"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "المراجع الرئيسي",
                    ValueEn = "Main reviewer",
                    SortOrder = 2,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555553"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "المتحدث",
                    ValueEn = "Speaker",
                    SortOrder = 3,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555554"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "مقدم البحث",
                    ValueEn = "Research presenter",
                    SortOrder = 4,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "حضر فقط",
                    ValueEn = "Just attended",
                    SortOrder = 5,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555556"),
                    Type = LookupTypes.SmemiarParticipationType.ToString(),
                    Key = "PARTCIPATION",
                    ValueAr = "اخرى",
                    ValueEn = "Other",
                    SortOrder = 6,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                }
                #endregion

             );
        }
    }
}
