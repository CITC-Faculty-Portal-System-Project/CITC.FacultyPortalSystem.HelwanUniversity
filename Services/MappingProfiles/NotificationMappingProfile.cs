namespace Services.MappingProfiles
{
	public class NotificationMappingProfile : Profile
	{
		public NotificationMappingProfile()
		{
			CreateMap<Notification, NotificationDto>().ReverseMap();
		}
	}
}
