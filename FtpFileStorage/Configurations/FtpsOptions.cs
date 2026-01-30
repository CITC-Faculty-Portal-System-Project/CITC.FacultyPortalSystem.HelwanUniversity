using FluentFTP;

namespace FtpFileStorage.Configurations
{
    public class FtpsOptions
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RootPath { get; set; } = "/";

        public FtpEncryptionMode EncryptionMode { get; set; }
        public FtpDataConnectionType DataConnectionType { get; set; }


        public bool ValidateAnyCertificate { get; init; } = true;

        public int ConnectTimeoutMs { get; init; } = 15000;
        public int ReadTimeoutMs { get; init; } = 30000;
        public int DataConnectTimeoutMs { get; init; } = 15000;
        public int DataReadTimeoutMs { get; init; } = 30000;
    }
}
