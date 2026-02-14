namespace Shared.Dtos.AttachmentsModule
{
    public record EncryptedResult
    (
        byte[] CipherData,
        string Hash,
        byte[] Nonce,
        byte[] Tag,
        string KeyRef,
        byte[] WrappedDek

    );
}
