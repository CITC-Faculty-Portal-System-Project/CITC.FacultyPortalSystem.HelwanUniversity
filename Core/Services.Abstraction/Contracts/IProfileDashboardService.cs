using Shared.Dtos.FacultyMemberDataModule;

namespace Services.Abstraction.Contracts
{
    public interface IProfileDashboardService
    {
        public Task<SkillsDTO> UpdateSkillAsync(SkillsDTO skillsDTO);
        public Task<BioSummaryDTO> UpdateBioSummaryAsync(BioSummaryDTO bioSummaryDTO);
        public Task<ProfileDashboardResponseDTO> GetProfileDashboardAsync();
    }
}
