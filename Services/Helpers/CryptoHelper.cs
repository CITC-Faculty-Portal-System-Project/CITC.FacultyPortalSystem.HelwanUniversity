using System.Security.Cryptography;

namespace Services.Helpers
{
    internal static class CryptoHelper
    {
        private const int KeySize = 32;
        private const int NonceSize = 12;
        private const int TagSize = 16;

        public static byte[] GenerateNonce() => RandomNumberGenerator.GetBytes(NonceSize);

        public static byte[] GenerateKey() => RandomNumberGenerator.GetBytes(KeySize);

        public static (byte[] Cipher, byte[] Tag) EncryptAesGcm(byte[] plain, byte[] key, byte[] nonce)
        {
            ArgumentNullException.ThrowIfNull(plain);
            ValidateKey(key);
            ValidateNonce(nonce);

            var cipher = new byte[plain.Length];
            var tag = new byte[TagSize];

            using var aes = new AesGcm(key, TagSize);
            aes.Encrypt(nonce, plain, cipher, tag);

            return (cipher, tag);
        }

        public static byte[] DecryptAesGcm(byte[] cipher, byte[] key, byte[] nonce, byte[] tag)
        {
            ArgumentNullException.ThrowIfNull(cipher);
            ValidateKey(key);
            ValidateNonce(nonce);
            ValidateTag(tag);

            var plain = new byte[cipher.Length];

            using var aes = new AesGcm(key, TagSize);
            aes.Decrypt(nonce, cipher, tag, plain);

            return plain;
        }

        public static string ComputeSha256(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);

            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(data));
        }

        private static void ValidateKey(byte[] key)
        {
            if (key.Length != KeySize)
                throw new CryptographicException("AES-256-GCM key must be exactly 32 bytes.");
        }

        private static void ValidateNonce(byte[] nonce)
        {
            if (nonce.Length != NonceSize)
                throw new CryptographicException("AES-GCM nonce must be exactly 12 bytes.");
        }

        private static void ValidateTag(byte[] tag)
        {
            if (tag.Length != TagSize)
                throw new CryptographicException("AES-GCM tag must be exactly 16 bytes.");
        }
    }
}