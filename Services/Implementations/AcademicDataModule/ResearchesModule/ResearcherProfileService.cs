using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ResearcherProfileService(IUnitOfWork unitOfWork , IMapper mapper , IAuthenticationService authenticationService
        ) : BaseService<ResearcherProfile, int>(unitOfWork, authenticationService , mapper), IResearcherProfileService
    {
        protected override string EntityName => "Researcher Profile";

        public async Task<ResearcherProfileResponseDTO> GetResearcherProfile()
        {
            var user = await GetCurrentUserAsync();

            var profile = await Repo.GetAsync(new ResearcherProfileSpceification(user.UserId));

            return Mapper.Map<ResearcherProfileResponseDTO>(profile);
        }
    }
}
