using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberTeachingExperiencesManagementService
    {
        Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetFacultyMemberTeachingExperiencesAsync(
          TeachingExperiencesSpecificationParameters parameters,
          string facultyMemberEmail);

        Task<TeachingExperiencesResponseDTO> GetFacultyMemberTeachingExperienceByIdAsync(int id);

        Task<TeachingExperiencesResponseDTO> CreateFacultyMemberTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string facultyMemberEmail);

        Task<TeachingExperiencesResponseDTO> UpdateFacultyMemberTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto);

        Task DeleteFacultyMemberTeachingExperienceAsync(int teachingExperienceId);
    }
}
