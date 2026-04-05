using Shared.Dtos.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IResearcherProfileService
    {
        Task<ResearcherProfileResponseDTO> GetResearcherProfile(Guid? facultyMemberId = null);

    }
}
