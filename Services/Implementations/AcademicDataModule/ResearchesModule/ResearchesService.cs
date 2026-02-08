using Domain.Contracts;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Formats.Asn1;

namespace Services.Implementations.AcademicDataModule.ResearchesModule
{
    public class ResearchesService(IUnitOfWork unitOfWork 
                , IMapper mapper 
                , IAuthenticationService authenticationService) :  
                     BaseService<Research , int>(unitOfWork 
                         , authenticationService  
                         , mapper) , IResearchesService
    {
        protected override string EntityName => "Researches";

        public async Task<ResearchCardResponseDTO> ConfirmRecommendedResearch(int researchId)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new RecommendedResearchesSpecifications(researchId))
                ?? throw NotFound();

            if (!researchEntity.Contributions!.Any(c => c.ContributorId == user.UserId))
                throw new UnauthorizedException("You Can't Modify this research!");
           
            researchEntity.IsConfirmed = true;

            Repo.Update(researchEntity);

            await unitOfWork.SaveChangesAsync();

            return Mapper.Map<ResearchCardResponseDTO>(researchEntity);

        }

        public async Task DeleteResearch(int researchId)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new RecommendedResearchesSpecifications(researchId))
                ?? throw NotFound();

            if (!researchEntity.Contributions!.Any(c => c.ContributorId == user.UserId))
                throw new UnauthorizedException("You Can't Modify this research!");

            var researcherContribution = researchEntity.Contributions!.FirstOrDefault(c => c.ContributorId == user.UserId);

            researchEntity.IsDeleted = true;
            researchEntity.DeletedAt = DateTime.Now;
            researchEntity.DeletedBy = user.UserName;
            researcherContribution!.IsDeleted = true;
            researcherContribution.DeletedAt = DateTime.Now;
            researcherContribution.DeletedBy = user.UserName;

            Repo.Update(researchEntity);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<PaginatedResult<ResearchCardResponseDTO>> GetAllRecommendedResearches
                                    (RecommendedResearchesSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var recommendedResearchesEntites = await Repo.GetAllAsync(new RecommendedResearchesSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new RecommendedResearchesCountSpecifications(parameters, user.UserId));
            
            var currentPage = recommendedResearchesEntites.Count();

            var recommendedResearchesResponse = Mapper.Map<IEnumerable<ResearchCardResponseDTO>>(recommendedResearchesEntites);

            return new PaginatedResult<ResearchCardResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, recommendedResearchesResponse);
        }

        public async Task<ResearchResponseDTO> GetResearchByTitle(string title)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new ResearchSpecifications(title, user.UserId))
                        ?? throw NotFound();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);
        }
    }
}
