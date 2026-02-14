namespace FtpFileStorage.Configurations
{
    public class FileStorageOptions
    {
        public string Provider { get; set; } = default!;
        public FtpsOptions Ftps { get; set; } = new();
    }
}
