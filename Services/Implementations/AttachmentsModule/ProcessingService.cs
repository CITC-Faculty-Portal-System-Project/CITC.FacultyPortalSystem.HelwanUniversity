using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule;
using Shared.Dtos.AttachmentsModule;

namespace Services.Implementations.AttachmentsModule
{
    public class ProcessingService(IMapper _mapper , 
        IFTPFileStorageService _fTPFileStorageService , IEncryptionService _encryptionService) 
        : IProcessingService
    {
        public async Task<AttachmentReferenceDTO> ProcessAsync(IFormFile file, string remotePath, string creator)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var inputStream = file.OpenReadStream();
            var encrypted = await _encryptionService.EncryptAsync(inputStream);

            using var encryptedStream =
                new MemoryStream(encrypted.CipherData);

            var storedPath = await _fTPFileStorageService.UploadFileAsync(remotePath, encryptedStream, file.FileName);
         
            var uploadDto = new AttachmentUploadDTO
            {
                Encrypted = encrypted,
                File = file,
                RemotePath = storedPath,
                Creator = creator
            };

            var attachmentDTO = _mapper.Map<AttachmentReferenceDTO>(uploadDto);

            return attachmentDTO;
        }
    }
}
