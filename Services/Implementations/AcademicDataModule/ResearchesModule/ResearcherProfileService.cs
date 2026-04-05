using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ResearcherProfileService(
       IUnitOfWork unitOfWork,
       IMapper mapper,
       IAuthenticationService authenticationService)
       : BaseService<ResearcherProfile, int>(unitOfWork, authenticationService, mapper),
         IResearcherProfileService
    {
        protected override string EntityName => "Researcher Profile";

        public async Task<ResearcherProfileResponseDTO> GetResearcherProfile(Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();

            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var profile = await Repo.GetAsync(
                new ResearcherProfileSpceification(targetFacultyMemberId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                profile.FacultyMemberId,
                facultyMemberId?.ToString());

            return Mapper.Map<ResearcherProfileResponseDTO>(profile);
        }
    }
}
