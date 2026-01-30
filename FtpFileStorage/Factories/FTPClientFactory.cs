using FluentFTP;
using FtpFileStorage.Configurations;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts.AttachmentsModule;
using System.Net;

namespace FtpFileStorage.Factories
{
    public sealed class FTPClientFactory : IFTPClientFactory
    {

        private readonly FtpsOptions _options;

        public FTPClientFactory(IOptions<FtpsOptions> options)
        {
            _options = options.Value;
        }

        public async Task<AsyncFtpClient> CreateConnectedAsync(CancellationToken ct = default)
        {
            var client = new AsyncFtpClient(
                _options.Host,
                new NetworkCredential(_options.Username, _options.Password),
                _options.Port);

            client.Config.EncryptionMode = _options.EncryptionMode;
            client.Config.DataConnectionType = _options.DataConnectionType;

            client.Config.ValidateAnyCertificate = _options.ValidateAnyCertificate;

            client.Config.ConnectTimeout = _options.ConnectTimeoutMs;
            client.Config.ReadTimeout = _options.ReadTimeoutMs;
            client.Config.DataConnectionConnectTimeout = _options.DataConnectTimeoutMs;
            client.Config.DataConnectionReadTimeout = _options.DataReadTimeoutMs;

            await client.Connect(ct);
            return client;
        }
    }
}
