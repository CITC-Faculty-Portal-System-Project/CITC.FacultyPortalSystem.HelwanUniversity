
namespace Presistence.Data.Configurations
{
    public class LookupConfigurations : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.ToTable("Lookups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ValueAr)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.ValueEn)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.HasIndex(x => new { x.Type, x.Key }).IsUnique();

        }
    }
}
