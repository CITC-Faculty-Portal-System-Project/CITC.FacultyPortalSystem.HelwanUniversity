using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberGeneralExperiencesManagementService(IGeneralExperiencesHelper _helper)
        : IFacultyMemberGeneralExperiencesManagementService
    {

        public Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetFacultyMemberGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllGeneralExperiencesAsync(parameters, facultyMemberEmail);

        public Task<GeneralExperiencesResponseDTO> GetFacultyMemberGeneralExperienceByIdAsync(int id)
            => _helper.GetGeneralExperienceByIdAsync(id);

        public Task<GeneralExperiencesResponseDTO> CreateFacultyMemberGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string facultyMemberEmail)
            => _helper.CreateGeneralExperienceAsync(generalExperienceCreateDto, facultyMemberEmail);

        public Task<GeneralExperiencesResponseDTO> UpdateFacultyMemberGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
            => _helper.UpdateGeneralExperienceAsync(generalExperienceId, generalExperienceUpdateDto);

        public Task DeleteFacultyMemberGeneralExperienceAsync(int generalExperienceId)
            => _helper.DeleteGeneralExperienceAsync(generalExperienceId);
    }
}
