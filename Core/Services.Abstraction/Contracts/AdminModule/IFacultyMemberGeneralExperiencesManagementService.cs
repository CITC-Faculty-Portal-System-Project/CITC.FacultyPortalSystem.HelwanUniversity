using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberGeneralExperiencesManagementService
    {
        Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetFacultyMemberGeneralExperiencesAsync(
           GeneralExperiencesSpecificationParameters parameters,
           string facultyMemberEmail);

        Task<GeneralExperiencesResponseDTO> GetFacultyMemberGeneralExperienceByIdAsync(int id);

        Task<GeneralExperiencesResponseDTO> CreateFacultyMemberGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string facultyMemberEmail);

        Task<GeneralExperiencesResponseDTO> UpdateFacultyMemberGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto);

        Task DeleteFacultyMemberGeneralExperienceAsync(int generalExperienceId);
    }
}
