namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class ContributionsModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(

            #region Contribution Type

                    new Lookup
                    {
                        Id = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                        Type = LookupTypes.ContributionTypes.ToString(),
                        Key = "CONTRIBUTION",
                        ValueAr = "تبرعات",
                        ValueEn = "Donations",
                        SortOrder = 1,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("40404040-4040-4040-4040-404040404041"),
                        Type = LookupTypes.ContributionTypes.ToString(),
                        Key = "CONTRIBUTION",
                        ValueAr = "اتفاقيات",
                        ValueEn = "Agreements",
                        SortOrder = 2,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("40404040-4040-4040-4040-404040404042"),
                        Type = LookupTypes.ContributionTypes.ToString(),
                        Key = "CONTRIBUTION",
                        ValueAr = "نشاط طلابي",
                        ValueEn = "Student activity",
                        SortOrder = 3,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("40404040-4040-4040-4040-404040404043"),
                        Type = LookupTypes.ContributionTypes.ToString(),
                        Key = "CONTRIBUTION",
                        ValueAr = "اخرى",
                        ValueEn = "Other",
                        SortOrder = 4,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    }
                    #endregion

            );
        }
    }
}
