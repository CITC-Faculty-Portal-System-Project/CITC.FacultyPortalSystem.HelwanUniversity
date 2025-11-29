using Domain.Entities.ProjectsAndCommitteesModule;
using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Implementations
{
    public class ProjectsAndCommitteesService(IUnitOfWork _unitOfWork, IMapper _mapper, IAuthenticationService _authenticationService) : IProjectsAndCommitteesService
    {
        #region Committees And Associations
        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access Committees And Associations.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Committees And Associations Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(parameters);
            var committeesAndAssociations = await committeesAndAssociationsRepo.GetAllAsync(specifications) ?? throw new NotFoundException("No Committee Or Association is Found.");

            //Map Result in IEnumerable Wrapped Dto
            var committeesAndAssociationsResult = _mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committeesAndAssociations);

            //Get The Page Size
            var currentPageCount = committeesAndAssociations.Count();

            //Get Count of The Specifications
            var countSpecifications = new CommitteesAndAssociationsCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await committeesAndAssociationsRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<CommitteesAndAssociationsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, committeesAndAssociationsResult);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Committee Or Association.");

            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(id);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            if (committeeOrAssociation.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access This Committee Or Association.");

            //Map To Dto
            var committeeOrAssociationResult = _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);

            //Return Result Data
            return committeeOrAssociationResult;
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
        {
            // Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto To Entity and Add Faculty Member Id
            var committeeOrAssociation = _mapper.Map<CommitteesAndAssociations>(committeeOrAssociationCreateDto);
            committeeOrAssociation.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            await committeesAndAssociationsRepo.AddAsync(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }
        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(committeeOrAssociationId);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (committeeOrAssociation.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update This Committee Or Association.");

            //Map Dto To Entity
            _mapper.Map(committeeOrAssociationUpdateDto, committeeOrAssociation);

            //Update and Save Updated Data
            committeesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(committeeOrAssociationId);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (committeeOrAssociation.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete This Committee Or Association.");

            //Apply Soft Delete
            committeeOrAssociation.IsDeleted = true;

            committeesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Reviewing Articles
        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Reviewing Articles.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Reviewing Articles Data
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(parameters);
            var reviewingArticles = await reviewingArticlesRepo.GetAllAsync(specifications) ?? throw new NotFoundException("No Articles are Found.");

            //Map Result in IEnumerable Wrapped Dto
            var reviewingArticlesResult = _mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

            //Get The Page Size
            var currentPageCount = reviewingArticles.Count();

            //Get Count of The Specifications
            var countSpecifications = new ReviewingArticlesCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await reviewingArticlesRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<ReviewingArticlesDto>(parameters.PageIndex, currentPageCount, totalCount, reviewingArticlesResult);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Reviewing Article.");

            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(id);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            if (reviewingArticle.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access This Reviwing Article.");

            //Map To Dto
            var reviewingArticleResult = _mapper.Map<ReviewingArticlesDto>(reviewingArticle);

            //Return Mapped Data
            return reviewingArticleResult;
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto)
        {
            // Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto To Entity and Add Faculty Member Id
            var reviewingArticle = _mapper.Map<ReviewingArticles>(reviewingArticleCreateDto);
            reviewingArticle.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            await reviewingArticlesRepo.AddAsync(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, ReviewArticleUpdateDto reviewingArticleUpdateDto)
        {
            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(reviewingArticleId);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (reviewingArticle.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update This Reviewing Article.");

            //Map Dto To Entity
            _mapper.Map(reviewingArticleUpdateDto, reviewingArticle);

            //Update and Save Updated Data
            reviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId)
        {
            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(reviewingArticleId);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (reviewingArticle.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete This Reviewing Article.");

            //Apply Soft Delete
            reviewingArticle.IsDeleted = true;

            reviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Participation In Magazines
        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Participation in Magazines.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Participation In Magazines Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(parameters);
            var participationInMagazines = await participationInMagazinesRepo.GetAllAsync(specifications) ?? throw new NotFoundException("Participation in Magazines are Not Found.");

            //Map Result in IEnumerable Wrapped Dto
            var participationIndMagazinesResult = _mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(participationInMagazines);

            //Get The Page Size
            var currentPageSize = participationInMagazines.Count();

            //Get Count of The Specifications
            var countSpecifications = new ParticipationInMagazinesCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await participationInMagazinesRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<ParticipationInMagazinesResponseDto>(parameters.PageIndex, currentPageSize, totalCount, participationIndMagazinesResult);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Participation in Magazine.");

            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(id);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            if (participationInMagazine.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access This Participation in Magazine.");

            //Map To Dto
            var participationInMagazineResult = _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);

            //Return Mapped Data
            return participationInMagazineResult;
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
        {
            // Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto To Entity and Add Faculty Member Id
            var participationInMagazine = _mapper.Map<ParticipationInMagazines>(participationInMagazinesCreateDto);
            participationInMagazine.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            await participationInMagazinesRepo.AddAsync(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(participationInMagazineId);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (participationInMagazine.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update This Paricipation in Magazine.");

            //Map Dto To Entity
            _mapper.Map(participationInMagazinesUpdateDto, participationInMagazine);

            //Update and Save Updated Data
            participationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId)
        {
            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(participationInMagazineId);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (participationInMagazine.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete This Paricipation in Magazine.");

            //Apply Soft Delete
            participationInMagazine.IsDeleted = true;

            participationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Projects
        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Projects.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Projects Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(parameters);
            var projects = await projectsRepo.GetAllAsync(specification) ?? throw new NotFoundException("Projects are Not Found.");

            //Map Result In IEnumerable Wrapped Dto
            var projectsResult = _mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

            //Get The Page Size
            var currentPageCount = projects.Count();

            //Get Count of The Specifications
            var countSpecifications = new ProjectsCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await projectsRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<ProjectsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, projectsResult);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(int id)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Project.");

            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(id);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            if (project.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access This Project.");

            //Map To Dto
            var projectResult = _mapper.Map<ProjectsResponseDto>(project);

            //Return Mapped Result
            return projectResult;
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            // Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto To Entity and Add Faculty Member Id
            var project = _mapper.Map<Projects>(projectCreateDto);
            project.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            await projectsRepo.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(projectId);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (project.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update This Project.");

            //Map Dto To Entity
            _mapper.Map(projectUpdateDto, project);

            //Update and Save Updated Data
            projectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(projectId);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (project.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete This Project.");

            //Apply Soft Delete
            project.IsDeleted = true;

            projectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}