using Services.Abstraction.EncryptionServices;
using Shared.Dtos.AttachmentsModule;
using System.Security.Cryptography;
using System.Text;

namespace Services.EncryptionServices
{
    public class AttachmentEncryptionService : IAttachmentEncryptionService
    {
        private const int KeySize = 32;
        private const int NonceSize = 12;
        private const int TagSize = 16;

        #region Encryption Helpers


        private static async Task<byte[]> ReadAllBytesAsync(Stream stream)
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return ms.ToArray();
        }

        private static byte[] GenerateDek()
            => RandomNumberGenerator.GetBytes(32);

        private static byte[] GenerateNonce()
            => RandomNumberGenerator.GetBytes(12);

        private static (byte[] Cipher, byte[] Tag) EncryptAesGcm(
            byte[] plain,
            byte[] dek,
            byte[] nonce)
        {
            var cipher = new byte[plain.Length];
            var tag = new byte[16];

            using var aes = new AesGcm(dek);
            aes.Encrypt(nonce, plain, cipher, tag);

            return (cipher, tag);
        }

        private static string ComputeSha256(byte[] data)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(data));
        }




        #endregion

        #region Dcryption Helpers

        private static void ValidateFileCryptoMetadata(AttachmentReferenceDTO attachment, int cipherLength)
        {
            if (attachment.WrappedDek is null || attachment.WrappedDek.Length != 32)
                throw new CryptographicException("Invalid DEK.");

            if (attachment.Nonce is null || attachment.Nonce.Length != 12)
                throw new CryptographicException("Invalid nonce.");

            if (attachment.Tag is null || attachment.Tag.Length != 16)
                throw new CryptographicException("Invalid authentication tag.");

            if (cipherLength <= 0)
                throw new CryptographicException("Invalid cipher data.");
        }

        private static byte[] DecryptAesGcm(
            byte[] cipher,
            byte[] dek,
            byte[] nonce,
            byte[] tag)
        {
            var plain = new byte[cipher.Length];

            // Explicitly specify expected tag size (usually 16 bytes)
            using var aes = new AesGcm(dek, tagSizeInBytes: 16);

            aes.Decrypt(nonce, cipher, tag, plain);

            return plain;
        }


        private static void VerifyIntegrity(byte[] plain, string expectedHash)
        {
            var currentHash = ComputeSha256(plain);

            if (!string.Equals(currentHash, expectedHash, StringComparison.Ordinal))
                throw new CryptographicException("File integrity check failed.");
        }

        #endregion

        public Task<byte[]> DecryptAsync(byte[] cipherData, AttachmentReferenceDTO attachment)
        {
            ValidateFileCryptoMetadata(attachment, cipherData.Length);

            var plain = DecryptAesGcm(
                cipherData,
                attachment.WrappedDek!,
                attachment.Nonce,
                attachment.Tag
            );

            VerifyIntegrity(plain, attachment.Hash);

            return Task.FromResult(plain);
        }

        public async Task<EncryptedResult> EncryptAsync(Stream plainFile)
        {
            var plainBytes = await ReadAllBytesAsync(plainFile);

            var dek = GenerateDek();
            var nonce = GenerateNonce();

            var (cipher, tag) = EncryptAesGcm(plainBytes, dek, nonce);
            var hash = ComputeSha256(plainBytes);

            return new EncryptedResult(
                CipherData: cipher,
                Hash: hash,
                Nonce: nonce,
                Tag: tag,
                KeyRef: "LocalKey",
                WrappedDek: dek
            );
        }

    }
}
