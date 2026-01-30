using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IEncryptionService
    {
        Task<EncryptedResult> EncryptAsync(Stream plainFile);
        Task<byte[]> DecryptAsync(byte[] cipherData, AttachmentReferenceDTO attachment);

    }
}
