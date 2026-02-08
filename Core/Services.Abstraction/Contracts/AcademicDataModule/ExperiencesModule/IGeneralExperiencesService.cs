using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule
{
    public interface IGeneralExperiencesService
    {
        public Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(GeneralExperiencesSpecificationParameters parameters);
        public Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(int id);
        public Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(GeneralExperiencesCreateDTO generalExperienceCreateDto);
        public Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(int generalExperienceId, GeneralExperiencesUpdateDTO generalExperienceUpdateDto);
        public Task DeleteGeneralExperienceAsync(int generalExperienceId);
    }
}
