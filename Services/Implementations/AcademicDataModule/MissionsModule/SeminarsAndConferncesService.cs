using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.AcademicDataModule.MissionsModule
{
    public class SeminarsAndConferncesService(
       IUnitOfWork unitOfWork,
       IMapper mapper,
       IAuthenticationService authenticationService,
       ISeminarsAndConferencesHelper seminarsAndConferencesHelper)
       : BaseService<ConferencesAndSeminars, int>(unitOfWork, authenticationService, mapper),
         ISeminarsAndConferencesService
    {
        private readonly ISeminarsAndConferencesHelper _helper = seminarsAndConferencesHelper;

        protected override string EntityName => "Seminars And Conferences";

        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
            SeminarsAndConferncesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllSeminarsAndConferencesAsync(parameters, currentUser.Email);
        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetSeminarOrConferenceByIdAsync(id);
        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateSeminarOrConferenceAsync(
                conferencesAndSeminarsCreateDto,
                currentUser.Email);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateSeminarOrConferenceAsync(id, conferencesAndSeminarsUpdateDto);
        }

        public async Task DeleteSeminarOrConferenceAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteSeminarOrConferenceAsync(id);
        }
    }
}