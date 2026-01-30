namespace Shared.Dtos.AttachmentsModule
{
    public record AttachmentDownloadDTO
    {
        public required byte[] AttachmentData { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
