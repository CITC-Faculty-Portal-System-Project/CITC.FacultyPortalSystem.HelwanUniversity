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
    public class ResearchesService(
      IUnitOfWork unitOfWork,
      IMapper mapper,
      IAuthenticationService authenticationService)
      : BaseService<Research, int>(unitOfWork, authenticationService, mapper),
        IResearchesService
    {
        protected override string EntityName => "Researches";

        #region Helpers

        private async Task AttachUniversityContributorsAsync(
            Research entity,
            IUnitOfWork unitOfWork
            , Guid targetFacultyMemberId)
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
                    new PersonalDataWithNameSpecification(cont.MemberAcademicName));

                if (teammate?.FacultyMember is null)
                    continue;

                if (teammate.FacultyMember.Id != targetFacultyMemberId)
                {
                    cont.ContributorType = Domain.Enums.ContributorType.FromUniverstity;

                    teammate.FacultyMember.ResearchContributions ??= new List<ResearchContribution>();
                    teammate.FacultyMember.ResearchContributions.Add(cont);
                }
            }
        }

        #endregion

        public async Task<ResearchResponseDTO> AddResearch(
            ResearchDTO research,
            Guid? facultyMemberId = null)
        {
            var personalDataRepo = UnitOfWork.GetRepository<PersonalData, int>();
            var facultyMemberRepo = UnitOfWork.GetRepository<FacultyMember, Guid>();

            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            if (facultyMemberId is null)
                EnsureOwnership(targetFacultyMemberId, currentUser.UserId, EntityName);

            var entity = Mapper.Map<Research>(research);
            entity.Source = Domain.Enums.ResearchSource.Internal;
            entity.CreatedBy = targetFacultyMemberId.ToString();

            await AttachUniversityContributorsAsync(entity, UnitOfWork , targetFacultyMemberId);

            var currentContributor = await facultyMemberRepo.GetByIdAsync(targetFacultyMemberId)
                ?? throw new NotFoundException("Faculty Member is Not Found.");

            currentContributor.ResearchContributions ??= new List<ResearchContribution>();

            currentContributor.ResearchContributions.Add(new ResearchContribution
            {
                Contributor = currentContributor,
                Research = entity,
                MemberAcademicName = currentContributor.PersonalData?.NameInComposition
                                     ?? currentContributor.PersonalData?.Name
                                     ?? currentContributor.Name,
                IsConfirmed = true,
                IsTheMajorResearcher = true,
            });

            await Repo.AddAsync(entity);
            await SaveChangesAsync();

            return Mapper.Map<ResearchResponseDTO>(entity);
        }

        public async Task<ResearchResponseDTO> ConfirmRecommendedResearch(
            int researchId,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchEntity = await Repo.GetAsync(
                new RecommendedResearchesSpecifications(researchId, targetFacultyMemberId))
                ?? throw NotFound();

            
            if(targetFacultyMemberId == currentUser.UserId)
                if (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId))
                    throw new UnauthorizedException("You Can't Modify this research!");

            researchEntity.Contributions!
                .SingleOrDefault(c => c.ContributorId == targetFacultyMemberId)!.IsConfirmed = true;

            Repo.Update(researchEntity);
            await SaveChangesAsync();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);
        }

        public async Task DeleteResearch(
            int researchId,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchEntity = await Repo.GetAsync(
                new ResearchSpecifications(researchId, targetFacultyMemberId))
                ?? throw NotFound();

            if (targetFacultyMemberId == currentUser.UserId)
                if (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId))
                    throw new UnauthorizedException("You Can't Modify this research!");

            var researcherContribution = researchEntity.Contributions!
                .FirstOrDefault(c => c.ContributorId == targetFacultyMemberId);

            researcherContribution!.IsDeleted = true;
            researcherContribution.DeletedAt = DateTime.Now;
            researcherContribution.DeletedBy = currentUser.UserName;

            Repo.Update(researchEntity);
            await SaveChangesAsync();
        }

        public async Task<PaginatedResult<ResearchResponseDTO>> GetAllRecommendedResearches(
            ResearchSpecificationParameters parameters,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var recommendedResearchesEntities = await Repo.GetAllAsync(
                new RecommendedResearchesSpecifications(parameters, targetFacultyMemberId))
                ?? throw NotFound();

            var totalCount = await Repo.CountAsync(
                new RecommendedResearchesCountSpecifications(parameters, targetFacultyMemberId));

            var mapped = Mapper.Map<IEnumerable<ResearchResponseDTO>>(recommendedResearchesEntities);

            return new PaginatedResult<ResearchResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<PaginatedResult<ResearchResponseDTO>> GetAllResearches(
            ResearchSpecificationParameters parameters,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchesEntities = await Repo.GetAllAsync(
                new ResearchSpecifications(parameters, targetFacultyMemberId))
                ?? throw NotFound();

            var totalCount = await Repo.CountAsync(
                new ResearchCountSpecifications(parameters, targetFacultyMemberId));

            var mapped = Mapper.Map<IEnumerable<ResearchResponseDTO>>(researchesEntities);

            return new PaginatedResult<ResearchResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<ResearchResponseDTO> GetResarchById(
            int researchId,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var research = await Repo.GetAsync(
                new ResearchSpecifications(researchId, targetFacultyMemberId))
                ?? throw NotFound();

            return Mapper.Map<ResearchResponseDTO>(research);
        }

        public async Task<ResearchResponseDTO> GetResearchByTitle(
            string title,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchEntity = await Repo.GetAsync(
                new ResearchSpecifications(title, targetFacultyMemberId))
                ?? throw NotFound();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);
        }

        public async Task RejectResearch(
            int researchId,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchEntity = await Repo.GetAsync(
                new RecommendedResearchesSpecifications(researchId, targetFacultyMemberId))
                ?? throw NotFound();

            if (targetFacultyMemberId == currentUser.UserId)
                if (!researchEntity.Contributions!.Any(c => c.ContributorId == targetFacultyMemberId))
                    throw new UnauthorizedException("You Can't Modify this research!");

            researchEntity.Contributions!
                .SingleOrDefault(c => c.ContributorId == targetFacultyMemberId)!.IsDeleted = true;

            Repo.Update(researchEntity);
            await SaveChangesAsync();
        }

        public async Task<ResearchResponseDTO> UpdateResearch(
            int researchId,
            ResearchUpdateDTO researchUpdate,
            Guid? facultyMemberId = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var targetFacultyMemberId = facultyMemberId ?? currentUser.UserId;

            var researchEntity = await Repo.GetAsync(
                new ResearchSpecifications(researchId, targetFacultyMemberId))
                ?? throw NotFound();

            if (researchEntity.Contributions!
                .Any(c => c.ContributorId == targetFacultyMemberId && c.IsTheMajorResearcher == false))
                throw new ForbiddenException("You Can't Modify this research data as you aren't a major researcher!");

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

                mapUpdate: (dto, entity) =>
                {
                    if (entity.IsConfirmed)
                        throw new ForbiddenException("Confirmed contribution can't be updated");

                    if (entity.ContributorId.ToString() == researchEntity.CreatedBy)
                        throw new ForbiddenException("You can't modify the creator contribution");

                    Mapper.Map(dto, entity);
                },

                onDelete: e =>
                {
                    if (e.IsConfirmed)
                        throw new ForbiddenException("Confirmed contribution can't be deleted");

                    if (e.ContributorId.ToString() == researchEntity.CreatedBy)
                        throw new ForbiddenException("You Can't Delete this Contributor as he/she is the creator of the research");

                    e.IsDeleted = true;
                },

                onUpdateNotFound: id =>
                    throw new NotFoundException("ResearchContribution not found"),

                onDeleteNotFound: id =>
                    throw new NotFoundException("ResearchContribution not found for delete")
            );

            Mapper.Map(researchUpdate, researchEntity);

            await AttachUniversityContributorsAsync(researchEntity, UnitOfWork, targetFacultyMemberId);

            Repo.Update(researchEntity);
            await SaveChangesAsync();

            return Mapper.Map<ResearchResponseDTO>(researchEntity);
        }
    }
}
