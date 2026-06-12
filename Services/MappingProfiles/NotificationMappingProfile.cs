using Shared.Dtos.Notification;

namespace Services.MappingProfiles
{
    public class NotificationMappingProfile : Profile
	{
		public NotificationMappingProfile()
		{
			CreateMap<Notification, NotificationDTO>();
			CreateMap<NotificationDTO, Notification>()
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore());
		}
	}
}
