using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberSeminarsAndConferencesManagementService(ISeminarsAndConferencesHelper _helper)
        : IFacultyMemberSeminarsAndConferencesManagementService
    {
        public Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetFacultyMemberSeminarsAndConferencesAsync(
            SeminarsAndConferncesSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllSeminarsAndConferencesAsync(parameters, facultyMemberEmail);

        public Task<ConferencesAndSeminarsResponseDto> GetFacultyMemberSeminarOrConferenceByIdAsync(int id)
            => _helper.GetSeminarOrConferenceByIdAsync(id);

        public Task<ConferencesAndSeminarsResponseDto> CreateFacultyMemberSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string facultyMemberEmail)
            => _helper.CreateSeminarOrConferenceAsync(conferencesAndSeminarsCreateDto, facultyMemberEmail);

        public Task<ConferencesAndSeminarsResponseDto> UpdateFacultyMemberSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
            => _helper.UpdateSeminarOrConferenceAsync(id, conferencesAndSeminarsUpdateDto);

        public Task DeleteFacultyMemberSeminarOrConferenceAsync(int id)
            => _helper.DeleteSeminarOrConferenceAsync(id);
    }
}
