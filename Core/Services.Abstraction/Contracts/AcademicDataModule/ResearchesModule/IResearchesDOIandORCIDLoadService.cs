using Shared.Dtos.ResearchesModule;
using Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IResearchesDOIandORCIDLoadService
    {
        Task<DOIResponseDTO> GetByDoiAsync(string doi, CancellationToken ct = default);
        Task<ResearcherDataGetByORCIDResponseDTO?> GetContributorNameByORCIDAsync(string orcid, CancellationToken ct = default);

    }
}
