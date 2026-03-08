using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberTeachingExperiencesManagementService(ITeachingExperiencesHelper _helper) 
        : IFacultyMemberTeachingExperiencesManagementService
    {
        public Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetFacultyMemberTeachingExperiencesAsync(
        TeachingExperiencesSpecificationParameters parameters,
        string facultyMemberEmail)
        => _helper.GetAllTeachingExperiencesAsync(parameters, facultyMemberEmail);

        public Task<TeachingExperiencesResponseDTO> GetFacultyMemberTeachingExperienceByIdAsync(int id)
            => _helper.GetTeachingExperienceByIdAsync(id);

        public Task<TeachingExperiencesResponseDTO> CreateFacultyMemberTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string facultyMemberEmail)
            => _helper.CreateTeachingExperienceAsync(teachingExperienceCreateDto, facultyMemberEmail);

        public Task<TeachingExperiencesResponseDTO> UpdateFacultyMemberTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
            => _helper.UpdateTeachingExperienceAsync(teachingExperienceId, teachingExperienceUpdateDto);

        public Task DeleteFacultyMemberTeachingExperienceAsync(int teachingExperienceId)
            => _helper.DeleteTeachingExperienceAsync(teachingExperienceId);
    }
}
