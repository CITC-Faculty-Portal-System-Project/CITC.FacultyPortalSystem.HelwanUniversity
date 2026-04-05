using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule
{
    public interface IGeneralExperiencesService
    {
        Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters,
            string? facultyMemberEmail = null);

        Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string? facultyMemberEmail = null);

        Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteGeneralExperienceAsync(
            int generalExperienceId,
            string? facultyMemberEmail = null);
    }
}
