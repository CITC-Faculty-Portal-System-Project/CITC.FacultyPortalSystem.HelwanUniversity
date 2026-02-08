using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule
{
    public interface ITeachingExperiencesService
    {
        public Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(TeachingExperiencesSpecificationParameters parameters);
        public Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(int id);
        public Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(TeachingExperiencesCreateDTO teachingExperienceCreateDto);
        public Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(int teachingExperienceId, TeachingExperiencesUpdateDTO teachingExperienceUpdateDto);
        public Task DeleteTeachingExperienceAsync(int teachingExperienceId);
    }
}
