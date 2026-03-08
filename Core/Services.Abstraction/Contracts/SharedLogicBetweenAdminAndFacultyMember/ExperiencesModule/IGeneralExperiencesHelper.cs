using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule
{
    public interface IGeneralExperiencesHelper
    {
        Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(int id);

        Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string facultyMemberEmail);

        Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto);

        Task DeleteGeneralExperienceAsync(int generalExperienceId);
    }
}
