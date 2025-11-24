using Shared;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Abstraction.Contracts
{
    public interface IMissionsService
    {
        #region Scientific Missions
        Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(ScientificMissionSpecificationParamaters paramaters);
        public Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id);
        Task<ScientificMissionResponseDto> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto);
        Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto mission);
        public Task DeleteScientificMissionAsync(int id);
        #endregion

        #region Seminars And Conferences
        public Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters);
        public Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id);
        public Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto);
        public Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto);
        public Task DeleteSeminarOrConferenceAsync(int id);
        #endregion

        #region Training Programs
        public Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(TrainingProgramsSpecificationParameters parameters);
        public Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id);
        public Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(TrainingProgramsCreateDto trainingProgramsCreateDto);
        public Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(int id, TrainingProgramsUpdateDto trainingProgramsUpdateDto);
        public Task DeleteTrainingProgramAsync(int id);
        #endregion
    }
}
