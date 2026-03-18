namespace Shared.Dtos.MessagingAndChattingModule
{
    public record EncryptedMessageResult
    {
        public int ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public string RecieverUsername { get; set; } = string.Empty;

        public byte[] CipherText { get; init; } = default!;
        public byte[] Nonce { get; init; } = default!;
        public byte[] Tag { get; init; } = default!;
        public int KeyVersion { get; init; }
        public string Algorithm { get; init; } = "AES-256-GCM";

    }
}
