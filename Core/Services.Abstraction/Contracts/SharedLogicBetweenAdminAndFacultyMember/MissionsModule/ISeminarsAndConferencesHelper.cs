using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public interface ISeminarsAndConferencesHelper
    {
        Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
            SeminarsAndConferncesSpecificationParameters parameters,
            string facultyMemberEmail);

        Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id);

        Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string facultyMemberEmail);

        Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto);

        Task DeleteSeminarOrConferenceAsync(int id);
    }
}
