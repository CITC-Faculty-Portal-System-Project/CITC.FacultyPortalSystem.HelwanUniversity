using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface ISeminarsAndConferencesService
    {
        Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
         SeminarsAndConferncesSpecificationParameters parameters,
         string? facultyMemberEmail = null);

        Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(
            int id,
            string? facultyMemberEmail = null);

        Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string? facultyMemberEmail = null);

        Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto,
            string? facultyMemberEmail = null);

        Task DeleteSeminarOrConferenceAsync(
            int id,
            string? facultyMemberEmail = null);
    }
}
