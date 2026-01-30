using FluentFTP;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IFTPClientFactory
    {
        Task<AsyncFtpClient> CreateConnectedAsync(CancellationToken ct = default);

    }
}
