using Domain.Entities.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.MissionsModule
{
    public class SeminarsAndConferencesHelper(
         IUnitOfWork unitOfWork,
         IAuthenticationService authenticationService,
         IMapper mapper)
         : BaseService<ConferencesAndSeminars, int>(unitOfWork, authenticationService, mapper),
           ISeminarsAndConferencesHelper
    {
        protected override string EntityName => "Seminars And Conferences";

        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(
            SeminarsAndConferncesSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var conferencesOrSeminars = await Repo.GetAllAsync(
                new ConferncesAndSeminarsSpecification(parameters, facultyMemberEmail));

            var conferencesOrSeminarsResult =
                Mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferencesOrSeminars);

            var currentPageCount = conferencesOrSeminarsResult.Count();

            var totalCount = await Repo.CountAsync(
                new ConferncesAndSeminarsCountSpecification(parameters, facultyMemberEmail));

            return new PaginatedResult<ConferencesAndSeminarsResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                conferencesOrSeminarsResult);
        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id)
        {
            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(
            ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var conferenceOrSeminar = Mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
            conferenceOrSeminar.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(conferenceOrSeminar);
            await SaveChangesAsync();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(
            int id,
            ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
        {
            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            Mapper.Map(conferencesAndSeminarsUpdateDto, conferenceOrSeminar);

            Repo.Update(conferenceOrSeminar);
            await SaveChangesAsync();

            return Mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task DeleteSeminarOrConferenceAsync(int id)
        {
            var conferenceOrSeminar = await Repo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw NotFound();

            conferenceOrSeminar.IsDeleted = true;

            Repo.Update(conferenceOrSeminar);
            await SaveChangesAsync();
        }
    }
}
