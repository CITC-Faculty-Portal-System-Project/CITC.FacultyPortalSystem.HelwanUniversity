namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IFTPFileStorageService
    {
        Task<string> UploadFileAsync(string remotePath, Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string remotePath);
        Task DeleteFileAsync(string remotePath);
        Task<bool> FileExistsAsync(string remotePath);

    }
}
