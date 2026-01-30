using FluentFTP;
using FluentFTP.Helpers;
using FtpFileStorage.Configurations;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts.AttachmentsModule;

namespace FtpFileStorage.Implementation
{
    public class FTPFileStorageService : IFTPFileStorageService
    {
        private readonly IFTPClientFactory _fTPClientFactory;
        private readonly FtpsOptions _options;

        public FTPFileStorageService(
            IFTPClientFactory fTPClientFactory,
            IOptions<FtpsOptions> options)
        {
            _fTPClientFactory = fTPClientFactory;
            _options = options.Value;
        }

        public async Task DeleteFileAsync(string remotePath)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            var fullPath = _options.RootPath + remotePath;
            await client.DeleteFile(fullPath);
        }

        public async Task<Stream> DownloadFileAsync(string remotePath)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            var fullPath = _options.RootPath + remotePath;
            var memoryStream = new MemoryStream();

            var result = await client.DownloadStream(memoryStream, fullPath);
            if (result)
            {
                memoryStream.Position = 0;
                return memoryStream;
            }

            throw new IOException($"Failed to download file: {remotePath}");
        }

        public async Task<bool> FileExistsAsync(string remotePath)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            var fullPath = _options.RootPath + remotePath;
            return await client.FileExists(fullPath);
        }

        public async Task<bool> UploadFileAsync(string remotePath, Stream fileStream, string fileName)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            if (!remotePath.EndsWith("/"))
                remotePath += "/";

            var fullPath = _options.RootPath + remotePath + fileName;

            await client.CreateDirectory(_options.RootPath + remotePath, true);

            var status = await client.UploadStream(fileStream, fullPath, FtpRemoteExists.Overwrite, false);

            return status.IsSuccess();
        }
    }
}
