using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVGenerationService
    {
        public Task<CVResponseDTO> GetCVAsync();
        public Task<CVResponseDTO> GetPublicCVAsync(Guid id);
        public Task<CVVisibilitySettingResponseDTO> ManageCVVisibilityAsync(CVVisibilityConfig config);
        public Task<byte[]> GenerateCVPdfAsync(string templateName);
        public Task<string> PreviewCVAsync(string templateName);
    }
}
