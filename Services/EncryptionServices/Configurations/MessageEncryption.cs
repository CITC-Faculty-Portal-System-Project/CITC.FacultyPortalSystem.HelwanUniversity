namespace Services.EncryptionServices.Configurations
{
    public class MessageEncryption
    {
        public int CurrentKeyVersion { get; set; }
        public Dictionary<int, string> Keys { get; set; } = new();
    }
}
