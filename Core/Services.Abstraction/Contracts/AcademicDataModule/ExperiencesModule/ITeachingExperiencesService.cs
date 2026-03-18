using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule
{
    public interface ITeachingExperiencesService
    {
        Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
        TeachingExperiencesSpecificationParameters parameters,
        string? facultyMemberEmail = null);

        Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string? facultyMemberEmail = null);

        Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteTeachingExperienceAsync(
            int teachingExperienceId,
            string? facultyMemberEmail = null);
    }
}
