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
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<ConferencesAndSeminars, int>(unitOfWork, authenticationService, mapper),
         ISeminarsAndConferencesService
    {
        protected override string EntityName => "Seminars And Conferences";

        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
            SeminarsAndConferncesSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var conferencesAndSeminars = await Repo.GetAllAsync(
                new ConferncesAndSeminarsSpecification(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferencesAndSeminars);

            var totalCount = await Repo.CountAsync(
                new ConferncesAndSeminarsCountSpecification(parameters, email));

            return new PaginatedResult<ConferencesAndSeminarsResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var conferenceOrSeminar = await Repo.GetAsync(
                new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                conferenceOrSeminar.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var conferenceOrSeminar = Mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
            conferenceOrSeminar.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(conferenceOrSeminar);
            await SaveChangesAsync();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto,
            string? facultyMemberEmail = null)
        {
            var conferenceOrSeminar = await Repo.GetAsync(
                new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                conferenceOrSeminar.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(conferencesAndSeminarsUpdateDto, conferenceOrSeminar);

            Repo.Update(conferenceOrSeminar);
            await SaveChangesAsync();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task DeleteSeminarOrConferenceAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var conferenceOrSeminar = await Repo.GetAsync(
                new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                conferenceOrSeminar.FacultyMemberId,
                facultyMemberEmail);

            conferenceOrSeminar.IsDeleted = true;

            Repo.Update(conferenceOrSeminar);
            await SaveChangesAsync();
        }
    }
}