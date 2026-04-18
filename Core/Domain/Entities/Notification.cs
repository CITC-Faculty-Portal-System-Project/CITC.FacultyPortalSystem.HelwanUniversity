using Shared.Enums;

namespace Domain.Entities
{
	public class Notification : BaseEntity<Guid>
	{
		public string Source { get; set; } = string.Empty;
		public NotificationType Type { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public bool IsViewed { get; set; } = false;
		public bool IsRemoved { get; set; } = false;

		#region FK
		//Reference to FacultyMember Id
		public Guid ReceiverId { set; get; }
		#endregion
	}
}
