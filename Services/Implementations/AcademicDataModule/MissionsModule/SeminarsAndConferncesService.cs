using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
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
        IValidationService validationService)
                : BaseService<ConferencesAndSeminars, int>(unitOfWork, authenticationService, mapper, validationService), ISeminarsAndConferencesService
    {
        protected override string EntityName => "Seminars And Conferences";
        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAllAsync(new ConferncesAndSeminarsSpecification(parameters, currentUser.Email))
                ?? throw NotFound();

            var conferenceOrSeminarResult = Mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferenceOrSeminar);

            var currentPageCount = conferenceOrSeminar.Count();

            var totalCount = await Repo.CountAsync(new ConferncesAndSeminarsCountSpecification(parameters, currentUser.Email));

            return new PaginatedResult<ConferencesAndSeminarsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, conferenceOrSeminarResult);
        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id)
        {

            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = Mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
            conferenceOrSeminar.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(conferenceOrSeminar);
            await SaveChangesAsync();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(conferencesAndSeminarsUpdateDto, conferenceOrSeminar);

            Repo.Update(conferenceOrSeminar);
            await SaveChangesAsync();

            var conferenceOrSeminarResult = Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
            return conferenceOrSeminarResult;
        }

        public async Task DeleteSeminarOrConferenceAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, EntityName);

            conferenceOrSeminar.IsDeleted = true;
            Repo.Update(conferenceOrSeminar);

            await SaveChangesAsync();
        }
    }
}