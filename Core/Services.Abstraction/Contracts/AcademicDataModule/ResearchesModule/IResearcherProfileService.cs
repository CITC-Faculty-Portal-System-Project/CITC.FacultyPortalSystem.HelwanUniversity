using Shared.Dtos.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IResearcherProfileService
    {
        public Task<ResearcherProfileResponseDTO> GetResearcherProfile();

    }
}
