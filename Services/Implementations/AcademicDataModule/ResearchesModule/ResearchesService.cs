using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Global;
using Services.Helpers.CollectionSyncingHelpers;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Threading;

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


        #region Helpers

        private async Task AttachUniversityContributorsAsync(
            Research entity,
            IUnitOfWork unitOfWork)
                {
            if (entity.Contributions is null || !entity.Contributions.Any())
                return;

            var personalDataRepo = unitOfWork.GetRepository<PersonalData, int>();
            var facultyMemberRepo = unitOfWork.GetRepository<FacultyMember, Guid>();

            foreach (var cont in entity.Contributions)
            {
                if (string.IsNullOrWhiteSpace(cont.MemberAcademicName))
                    continue;

                var teammate = await personalDataRepo.GetAsync(
                    new PersonalDataWithNameSpecification(cont.MemberAcademicName)
                );

                if (teammate?.FacultyMember is null)
                    continue;

                cont.ContributorType = Domain.Enums.ContributorType.FromUniverstity;

                teammate.FacultyMember.ResearchContributions ??= new List<ResearchContribution>();

                teammate.FacultyMember.ResearchContributions.Add(cont);
            }
        }


        #endregion


        public async Task<ResearchResponseDTO> AddResearch(ResearchDTO research)
        {
            var personalDataRepo = UnitOfWork.GetRepository<PersonalData, int>();
            var facultyMemberRepo = UnitOfWork.GetRepository<FacultyMember, Guid>();
            
            var currentUser = await GetCurrentUserAsync();

            var entity = Mapper.Map<Research>(research);
            entity.Source = Domain.Enums.ResearchSource.Internal;

            await AttachUniversityContributorsAsync(entity, UnitOfWork);

            var currentContributor = await facultyMemberRepo.GetAsync
                (new FacultyMemberWithEmailSpecifications(currentUser.Email));

            currentContributor!.ResearchContributions!.Add(new ResearchContribution
            {
                Contributor = currentContributor,
                Research = entity,
                MemberAcademicName = currentContributor.PersonalData!.Name,
                IsConfirmed = true
            });

            await Repo.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            
            return Mapper.Map<ResearchResponseDTO>(entity);
        }

        public async Task<ResearchResponseDTO> ConfirmRecommendedResearch(int researchId)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new RecommendedResearchesSpecifications(researchId , user.UserId))
                ?? throw NotFound();

            if (!researchEntity.Contributions!.Any(c => c.ContributorId == user.UserId))
                throw new UnauthorizedException("You Can't Modify this research!");
           
            researchEntity.Contributions!
             .SingleOrDefault(c=> c.ContributorId == user.UserId)!.IsConfirmed = true;

            Repo.Update(researchEntity);

            await unitOfWork.SaveChangesAsync();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);

        }

        public async Task DeleteResearch(int researchId)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new ResearchSpecifications(researchId , user.UserId))
                ?? throw NotFound();

            if (!researchEntity.Contributions!.Any(c => c.ContributorId == user.UserId))
                throw new UnauthorizedException("You Can't Modify this research!");

            var researcherContribution = researchEntity.Contributions!.FirstOrDefault(c => c.ContributorId == user.UserId);

            researcherContribution!.IsDeleted = true;
            researcherContribution.DeletedAt = DateTime.Now;
            researcherContribution.DeletedBy = user.UserName;

            Repo.Update(researchEntity);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<PaginatedResult<ResearchResponseDTO>> GetAllRecommendedResearches
                                    (RecommendedResearchesSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var recommendedResearchesEntites = await Repo.GetAllAsync(new RecommendedResearchesSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new RecommendedResearchesCountSpecifications(parameters, user.UserId));
            
            var currentPage = recommendedResearchesEntites.Count();

            var recommendedResearchesResponse = Mapper.Map<IEnumerable<ResearchResponseDTO>>(recommendedResearchesEntites);

            return new PaginatedResult<ResearchResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, recommendedResearchesResponse);
        }

        public async Task<PaginatedResult<ResearchResponseDTO>> GetAllResearches(ResearchSpecificationParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            var researchesEntites = await Repo.GetAllAsync(new ResearchSpecifications(parameters, user.UserId))
                        ?? throw NotFound();

            var totalPagesCount = await Repo.CountAsync(new ResearchCountSpecifications(parameters, user.UserId));

            var currentPage = researchesEntites.Count();

            var researchesResponse = Mapper.Map<IEnumerable<ResearchResponseDTO>>(researchesEntites);

            return new PaginatedResult<ResearchResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, researchesResponse);
        }

        public async Task<ResearchResponseDTO> GetResarchById(int researchId)
        {
            var currentUser = await GetCurrentUserAsync();

            var research = await Repo.GetAsync(new ResearchSpecifications(researchId, currentUser.UserId))
                            ?? throw NotFound();

            return Mapper.Map<ResearchResponseDTO>(research);
        }

        public async Task<ResearchResponseDTO> GetResearchByTitle(string title)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new ResearchSpecifications(title, user.UserId))
                        ?? throw NotFound();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);
        }

        public async Task RejectResearch(int researchId)
        {
            var user = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new RecommendedResearchesSpecifications(researchId, user.UserId))
                ?? throw NotFound();

            if (!researchEntity.Contributions!.Any(c => c.ContributorId == user.UserId))
                throw new UnauthorizedException("You Can't Modify this research!");

            researchEntity.Contributions!.SingleOrDefault(c => c.ContributorId == user.UserId)!
                .IsDeleted = true;
            researchEntity.DeletedBy = user.UserName;

            Repo.Update(researchEntity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ResearchResponseDTO> UpdateResearch(int researchId, ResearchUpdateDTO researchUpdate)
        {
            var currentUser = await GetCurrentUserAsync();

            var researchEntity = await Repo.GetAsync(new ResearchSpecifications(researchId, currentUser.UserId))
                ?? throw NotFound();


            CollectionSync.Sync<
                ResearchContribution,
                ResearchContributionDTO,
                ResearchContributionDTO,
                ResearchContributionResponseDTO,
                int
            >(
                current: researchEntity.Contributions!,
                toAdd: researchUpdate.ResearchContributionsToAdd,
                toUpdate: researchUpdate.ResearchContributionsToUpdate,
                toDelete: researchUpdate.ResearchContributionsToDelete,

                childKey: rc => rc.Id,
                deleteKey: d => d.Id,

                mapAdd: d => Mapper.Map<ResearchContribution>(d),


                mapUpdate: (dto, entity) => Mapper.Map(dto, entity),

                onDelete: e => e.IsDeleted = true,

                onUpdateNotFound: id =>
                    throw new NotFoundException($"ResearchContribution not found"),

                onDeleteNotFound: id =>
                    throw new NotFoundException($"ResearchContribution not found for delete")
            );


            Mapper.Map(researchUpdate, researchEntity);

            await AttachUniversityContributorsAsync(researchEntity, UnitOfWork);

            Repo.Update(researchEntity);
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);

        }
    }
}
