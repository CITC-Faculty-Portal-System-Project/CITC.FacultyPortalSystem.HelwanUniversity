using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ParticipationInQualityWorksService(
    IUnitOfWork unitOfWork,
    IAuthenticationService authenticationService,
    IMapper mapper)
    : BaseService<ParticipationInQualityWorks, int>(unitOfWork, authenticationService, mapper),
      IParticipationInQualityWorksService
    {
        protected override string EntityName => "Participation In Quality Works";

        public async Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
            ParticipationInQualityWorksSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var participations = await Repo.GetAllAsync(
                new ParticipationInQualityWorksSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ParticipationInQualityWorksResponseDTO>>(participations);

            var totalCount = await Repo.CountAsync(
                new ParticipationInQualityWorksCountSpecifications(parameters, email));

            return new PaginatedResult<ParticipationInQualityWorksResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInQualityWorksSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                participation.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var participation = Mapper.Map<ParticipationInQualityWorks>(participationInQualityWorksCreateDto);
            participation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                participation.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(participationInQualityWorksUpdateDto, participation);

            Repo.Update(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task DeleteParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                participation.FacultyMemberId,
                facultyMemberEmail);

            participation.IsDeleted = true;

            Repo.Update(participation);
            await SaveChangesAsync();
        }
    }
}