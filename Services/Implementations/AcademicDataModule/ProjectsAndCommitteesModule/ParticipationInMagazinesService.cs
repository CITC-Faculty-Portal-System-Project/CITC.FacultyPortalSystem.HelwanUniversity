using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ParticipationInMagazinesService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<ParticipationInMagazines, int>(unitOfWork, authenticationService, mapper),
        IParticipationInMagazinesService
    {
        protected override string EntityName => "Participation In Magazines";

        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var magazines = await Repo.GetAllAsync(
                new ParticipationInMagazinesSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(magazines);

            var totalCount = await Repo.CountAsync(
                new ParticipationInMagazinesCountSpecifications(parameters, email));

            return new PaginatedResult<ParticipationInMagazinesResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                participation.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var participation = Mapper.Map<ParticipationInMagazines>(dto);
            participation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int id,
            ParticipationInMagazineUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                participation.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, participation);

            Repo.Update(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participation);
        }

        public async Task DeleteParticipationInMagazineAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var participation = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(id))
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