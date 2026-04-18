using Shared.Enums;

namespace Shared.Dtos
{
	public record NotificationDto
	{
		public string Source { get; set; } = string.Empty;
		public NotificationType Type { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public Guid ReceiverId { get; set; }
		public bool IsViewed { get; set; }
		public bool IsRemoved { get; set; }

	}
}
