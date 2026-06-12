using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVGenerationService
    {
        public Task<CVResponseDTO> GetCVAsync(Guid? facultyMemberId, bool isPublic = false);
        public Task<CVResponseDTO> GetPublicCVAsync(Guid id);
        public Task<CVVisibilitySettingResponseDTO> ManageCVVisibilityAsync(CVVisibilityConfig config);
        public Task<byte[]> GenerateCVPdfAsync(string templateName, Guid? facultyMemberId, bool isPublic = false);
        public Task<string> PreviewCVAsync(string templateName, bool isPublic = false);
        public Task<string> GetUserPrefferedTemplate(Guid? userId);
    }
}
