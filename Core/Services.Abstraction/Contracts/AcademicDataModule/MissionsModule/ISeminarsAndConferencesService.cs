using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.MissionsModule
{
    public interface ISeminarsAndConferencesService
    {
        public Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters);
        public Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id);
        public Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto);
        public Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto);
        public Task DeleteSeminarOrConferenceAsync(int id);
    }
}
