using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule
{
    public interface ITeachingExperiencesHelper
    {
        Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
            TeachingExperiencesSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(int id);

        Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string facultyMemberEmail);

        Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto);

        Task DeleteTeachingExperienceAsync(int teachingExperienceId);
    }
}
