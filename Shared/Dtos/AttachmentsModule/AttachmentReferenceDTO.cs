namespace Shared.Dtos.AttachmentsModule
{
    public record AttachmentReferenceDTO
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string HashAlg { get; set; } = "SHA-256";
        public string Hash { get; set; } = string.Empty;
        public byte[] Nonce { get; set; } = Array.Empty<byte>();
        public byte[] Tag { get; set; } = Array.Empty<byte>();
        public string KeyRef { get; set; } = string.Empty;
        public byte[] WrappedDek { get; set; } = Array.Empty<byte>();
        public string StorageProvider { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;

    }
}
