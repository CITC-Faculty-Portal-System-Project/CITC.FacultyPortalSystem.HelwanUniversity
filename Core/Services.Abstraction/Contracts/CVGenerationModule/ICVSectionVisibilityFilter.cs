using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;

namespace Services.Abstraction.Contracts.CVGenerationModule
{
    public interface ICVSectionVisibilityFilter
    {
        void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic = false);
    }
}
