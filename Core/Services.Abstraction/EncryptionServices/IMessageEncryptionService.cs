using Shared.Dtos.MessagingAndChattingModule;

namespace Services.Abstraction.EncryptionServices
{
    public interface IMessageEncryptionService
    {
        EncryptedMessageResult Encrypt(string content);
        string Decrypt(byte[] cipherText, byte[] nonce, byte[] tag, int keyVersion);
    }
}
