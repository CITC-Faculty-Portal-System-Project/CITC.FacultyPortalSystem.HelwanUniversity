using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVGenerationService
    {
        public Task<CVResponseDTO> GetCVAsync();
        public Task<CVVisibilitySettingResponseDTO> ManageCVVisibilityAsync(CVVisibilityConfig config);
    }
}
