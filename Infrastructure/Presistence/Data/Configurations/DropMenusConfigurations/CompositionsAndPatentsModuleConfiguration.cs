namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class CompositionsAndPatentsModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(

            #region Author Roles

                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "مؤلف",
                    ValueEn = "Author",
                    SortOrder = 1,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202021"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "مترجم",
                    ValueEn = "Trasnlator",
                    SortOrder = 2,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202022"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "مراجع",
                    ValueEn = "Revisor",
                    SortOrder = 3,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202023"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "مترجم/مراجع",
                    ValueEn = "Translator/Revisor",
                    SortOrder = 4,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202024"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "محرر كتاب",
                    ValueEn = "Book editor",
                    SortOrder = 5,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                },
                new Lookup
                {
                    Id = Guid.Parse("20202020-2020-2020-2020-202020202025"),
                    Type = LookupTypes.AuthorRole.ToString(),
                    Key = "AUTHORROLE",
                    ValueAr = "مؤلف فصل",
                    ValueEn = "Chapter author",
                    SortOrder = 6,
                    CreatedBy = "Helwan Faculty Portal System",
                    CreatedAt = new DateTime(2025, 11, 22)
                }

                #endregion

            );
        }
    }
}
