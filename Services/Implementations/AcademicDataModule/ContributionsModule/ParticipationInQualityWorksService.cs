using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Global;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.AcademicDataModule.ContributionsModule
{
    public class ParticipationInQualityWorksService(
         IUnitOfWork unitOfWork,
         IMapper mapper,
         IAuthenticationService authenticationService,
         IParticipationInQualityWorksServiceHelper participationInQualityWorksHelper)
         : BaseService<ParticipationInQualityWorks, int>(unitOfWork, authenticationService, mapper),
           IParticipationInQualityWorksService
    {
        private readonly IParticipationInQualityWorksServiceHelper _helper = participationInQualityWorksHelper;

        protected override string EntityName => "Participation In Quality Works";

        public async Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
            ParticipationInQualityWorksSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllParticipationsInQualityWorksAsync(parameters, currentUser.Email);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(participation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetParticipationInQualityWorksByIdAsync(id);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateParticipationInQualityWorksAsync(
                participationInQualityWorksCreateDto,
                currentUser.Email);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            EnsureOwnership(participation.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateParticipationInQualityWorksAsync(
                participationInQualityWorksId,
                participationInQualityWorksUpdateDto);
        }

        public async Task DeleteParticipationInQualityWorksAsync(int participationInQualityWorksId)
        {
            var currentUser = await GetCurrentUserAsync();

            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            EnsureOwnership(participation.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteParticipationInQualityWorksAsync(participationInQualityWorksId);
        }
    }
}