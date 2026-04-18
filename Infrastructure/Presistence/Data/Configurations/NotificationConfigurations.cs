namespace Presistence.Data.Configurations
{
	public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable("Notifications");

			builder.HasOne<FacultyMember>()
				.WithMany()
				.HasForeignKey(N => N.ReceiverId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
