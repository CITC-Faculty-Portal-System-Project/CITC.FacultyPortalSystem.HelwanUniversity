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

        #region Helpers

        private static string CombineFtpPath(string dir, string fileName)
        {
            dir = (dir ?? "").Replace("\\", "/").Trim('/');
            fileName = (fileName ?? "").Replace("\\", "/").Trim('/');

            if (dir.EndsWith(fileName, StringComparison.OrdinalIgnoreCase))
                return "/" + dir; 

            return "/" + $"{dir}/{fileName}";
        }

        #endregion

        public async Task DeleteFileAsync(string remotePath)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            var fullPath = remotePath;
            await client.DeleteFile(fullPath);
        }

        public async Task<Stream> DownloadFileAsync(string remotePath)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            var fullPath = remotePath;
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

        public async Task<string> UploadFileAsync(string remotePath, Stream fileStream, string fileName)
        {
            using var client = await _fTPClientFactory.CreateConnectedAsync();

            remotePath = (remotePath ?? "").Replace("\\", "/").Trim('/');
            fileName = (fileName ?? "").Replace("\\", "/").Trim('/');

           
            var dirForFtp = _options.RootPath + "/" + remotePath;                 
            var fileForFtp = $"{dirForFtp}/{fileName}"; 

            await client.CreateDirectory(dirForFtp, true);

            if (fileStream.CanSeek) fileStream.Position = 0;

            var status = await client.UploadStream(
                fileStream,
                fileForFtp,
                FtpRemoteExists.Overwrite,
                createRemoteDir: false
            );

            if (status != FtpStatus.Success)
                throw new Exception($"FTP upload failed: {status}");
            return dirForFtp + "/";
        }

    }
}
