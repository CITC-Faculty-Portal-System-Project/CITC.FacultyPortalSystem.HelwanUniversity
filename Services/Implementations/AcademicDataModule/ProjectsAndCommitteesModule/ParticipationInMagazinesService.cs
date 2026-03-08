using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ParticipationInMagazinesService(
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IAuthenticationService authenticationService,
          IParticipationInMagazinesHelper participationInMagazinesHelper)
          : BaseService<ParticipationInMagazines, int>(unitOfWork, authenticationService, mapper),
            IParticipationInMagazinesService
    {
        private readonly IParticipationInMagazinesHelper _helper = participationInMagazinesHelper;

        protected override string EntityName => "Participation In Magazines";

        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllParticipationInMagazinesAsync(parameters, currentUser.Email);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(new ParticipationInMagazinesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetParticipationInMagazineByIdAsync(id);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateParticipationInMagazineAsync(
                participationInMagazinesCreateDto,
                currentUser.Email);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int participationInMagazineId,
            ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateParticipationInMagazineAsync(
                participationInMagazineId,
                participationInMagazinesUpdateDto);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId)
        {
            var currentUser = await GetCurrentUserAsync();

            var participationInMagazine = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteParticipationInMagazineAsync(participationInMagazineId);
        }
    }
}