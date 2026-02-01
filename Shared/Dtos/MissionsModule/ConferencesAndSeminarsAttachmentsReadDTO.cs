namespace Shared.Dtos.MissionsModule
{
    public record ConferencesAndSeminarsAttachmentsReadDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
