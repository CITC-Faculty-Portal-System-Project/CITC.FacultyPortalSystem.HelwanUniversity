using Shared.Common;
using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.EncryptionServices
{
    public interface IAttachmentEncryptionService
    {
        Task<EncryptedResult> EncryptAsync(Stream plainFile);
        Task<byte[]> DecryptAsync(byte[] cipherData, AttachmentReferenceDTO attachment);


    }
}
