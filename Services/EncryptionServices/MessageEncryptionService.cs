using Microsoft.Extensions.Options;
using Services.Abstraction.EncryptionServices;
using Services.EncryptionServices.Configurations;
using Shared.Dtos.MessagingAndChattingModule;
using System.Security.Cryptography;
using System.Text;

namespace Services.EncryptionServices
{
    public class MessageEncryptionService(IOptions<MessageEncryption> _options) : IMessageEncryptionService
    {
        private const int NonceSize = 12;
        private const int TagSize = 16;

        public EncryptedMessageResult Encrypt(string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext))
                throw new ArgumentException("Message content cannot be empty.", nameof(plaintext));

            var keyVersion = _options.Value.CurrentKeyVersion;
            var key = GetKeyBytes(keyVersion);

            var nonce = RandomNumberGenerator.GetBytes(NonceSize);
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var cipherText = new byte[plaintextBytes.Length];
            var tag = new byte[TagSize];

            using var aes = new AesGcm(key, TagSize);
            aes.Encrypt(nonce, plaintextBytes, cipherText, tag);

            return new EncryptedMessageResult
            {
                CipherText = cipherText,
                Nonce = nonce,
                Tag = tag,
                KeyVersion = keyVersion,
                Algorithm = "AES-256-GCM"
            };
        }

        public string Decrypt(byte[] cipherText, byte[] nonce, byte[] tag, int keyVersion)
        {
            var key = GetKeyBytes(keyVersion);

            var plaintextBytes = new byte[cipherText.Length];

            using var aes = new AesGcm(key, TagSize);
            aes.Decrypt(nonce, cipherText, tag, plaintextBytes);

            return Encoding.UTF8.GetString(plaintextBytes);
        }

        private byte[] GetKeyBytes(int keyVersion)
        {
            if (!_options.Value.Keys.TryGetValue(keyVersion, out var base64Key))
                throw new InvalidOperationException($"Encryption key version '{keyVersion}' was not found.");

            byte[] key;
            try
            {
                key = Convert.FromBase64String(base64Key);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException($"Encryption key version '{keyVersion}' is not valid Base64.");
            }

            if (key.Length != 32)
                throw new InvalidOperationException(
                    $"Encryption key version '{keyVersion}' must decode to exactly 32 bytes for AES-256-GCM.");

            return key;
        }

    }
}
