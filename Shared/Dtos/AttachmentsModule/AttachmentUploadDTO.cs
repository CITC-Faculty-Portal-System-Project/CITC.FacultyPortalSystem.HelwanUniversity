using Microsoft.AspNetCore.Http;

namespace Shared.Dtos.AttachmentsModule
{
    public record AttachmentUploadDTO
    {
        public required IFormFile File { get; set; }
        public required EncryptedResult Encrypted { get; set; }
        public string Creator { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;
    }
}
