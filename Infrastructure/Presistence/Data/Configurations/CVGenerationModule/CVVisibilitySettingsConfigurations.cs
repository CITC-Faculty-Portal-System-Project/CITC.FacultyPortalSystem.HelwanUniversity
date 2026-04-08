using Domain.Entities.CVGenerationModule;

namespace Presistence.Data.Configurations.CVGenerationModule
{
    public class CVVisibilitySettingsConfigurations : IEntityTypeConfiguration<CVVisibilitySettings>
    {
        public void Configure(EntityTypeBuilder<CVVisibilitySettings> builder)
        {
            builder.Property(e => e.VisibilityJson).HasDefaultValue("{}");
            builder.Property(e => e.PublicVisableAttributesJson).HasDefaultValue("{}");
            builder.Property(e => e.isPublic)
                .HasDefaultValue(true);
        }
    }
}
