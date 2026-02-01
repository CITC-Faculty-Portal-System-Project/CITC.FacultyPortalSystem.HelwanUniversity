using Microsoft.AspNetCore.Http;
using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IProcessingService
    {
        Task<AttachmentReferenceDTO> ProcessAsync(
                IFormFile file,
                string remotePath,
                string creator);
    }
}
