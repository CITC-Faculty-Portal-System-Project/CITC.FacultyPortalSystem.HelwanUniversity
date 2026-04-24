using Shared.Enums.NotificationModule;

namespace Shared.Dtos.Notification
{
    public record NotificationDTO
    {
        public string Source { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Guid ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
	}
}
