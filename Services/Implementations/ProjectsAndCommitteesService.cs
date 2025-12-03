using Domain.Entities.ProjectsAndCommitteesModule;
using Services.Specifications.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Implementations
{
    public class ProjectsAndCommitteesService(IUnitOfWork _unitOfWork, IMapper _mapper, IAuthenticationService _authenticationService) : IProjectsAndCommitteesService
    {
        #region Helper Methods
        //Get Current Logged User 
        private async Task<UserResultDto> GetCurrentUserAsync()
        {
            var email = _authenticationService.GetLoggedUserEmail();
            var user = await _authenticationService.GetCurrentUserAsync(email)
                       ?? throw new UnauthorizedAccessException("Unauthorized.");
            return user;
        }

        //Get Faculty Member By Email
        private async Task<FacultyMember> GetFacultyMemberByEmailAsync(string email)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var spec = new FacultyMemberWithEmailSpecifications(email);

            return await repo.GetAsync(spec)
                   ?? throw new NotFoundException("Faculty Member Not Found.");
        }

        //Ensure Ownership
        private static void EnsureOwnership(Guid entityFacultyMemberId, Guid currentUserId, string entityName)
        {
            if (entityFacultyMemberId != currentUserId)
                throw new UnauthorizedAccessException($"You cannot access this {entityName}.");
        }

        private IGenericRepository<CommitteesAndAssociations, int> CommitteesAndAssociationsRepo
            => _unitOfWork.GetRepository<CommitteesAndAssociations, int>();

        private IGenericRepository<ReviewingArticles, int> ReviewingArticlesRepo
            => _unitOfWork.GetRepository<ReviewingArticles, int>();

        private IGenericRepository<ParticipationInMagazines, int> ParticipationInMagazinesRepo
            => _unitOfWork.GetRepository<ParticipationInMagazines, int>();

        private IGenericRepository<Projects, int> ProjectsRepo
            => _unitOfWork.GetRepository<Projects, int>();
        #endregion

        #region Committees And Associations
        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Committees And Associations Data
            var committeesAndAssociations = await CommitteesAndAssociationsRepo.GetAllAsync(new CommitteesAndAssociationsSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("No Committee Or Association is Found.");

            //Map Result in IEnumerable Wrapped Dto
            var committeesAndAssociationsResult = _mapper.Map<IEnumerable<CommitteesAndAssociationsResponseDto>>(committeesAndAssociations);

            //Get The Page Size
            var currentPageCount = committeesAndAssociations.Count();

            //Get Total Count
            var totalCount = await CommitteesAndAssociationsRepo.CountAsync(new CommitteesAndAssociationsCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<CommitteesAndAssociationsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, committeesAndAssociationsResult);
        }

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationByIdAsync(int id)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Committee Or Association Data
            var committeeOrAssociation = await CommitteesAndAssociationsRepo.GetAsync(new CommitteesAndAssociationsSpecifications(id)) 
                ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, "Committee Or Association");

            //Map To Dto
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto To Entity and Add Faculty Member Id
            var committeeOrAssociation = _mapper.Map<CommitteesAndAssociations>(committeeOrAssociationCreateDto);
            committeeOrAssociation.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            await CommitteesAndAssociationsRepo.AddAsync(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }
        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Committee Or Association Data
            var committeeOrAssociation = await CommitteesAndAssociationsRepo.GetAsync(new CommitteesAndAssociationsSpecifications(committeeOrAssociationId))
                ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, "Committee Or Association");

            //Map Dto To Entity
            _mapper.Map(committeeOrAssociationUpdateDto, committeeOrAssociation);

            //Update and Save Updated Data
            CommitteesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId)
        {
            //Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Committee Or Association Data
            var committeeOrAssociation = await CommitteesAndAssociationsRepo.GetAsync(new CommitteesAndAssociationsSpecifications(committeeOrAssociationId)) 
                ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(committeeOrAssociation.FacultyMemberId, currentUser.UserId, "Committee Or Association");

            //Apply Soft Delete
            committeeOrAssociation.IsDeleted = true;

            CommitteesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Reviewing Articles
        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Reviewing Articles Data
            var reviewingArticles = await ReviewingArticlesRepo.GetAllAsync(new ReviewingArticlesSpecifications(parameters, currentUser.Email))
                ?? throw new NotFoundException("No Articles are Found.");

            //Map Result in IEnumerable Wrapped Dto
            var reviewingArticlesResult = _mapper.Map<IEnumerable<ReviewingArticlesDto>>(reviewingArticles);

            //Get The Page Size
            var currentPageCount = reviewingArticles.Count();

            //Get Total Count
            var totalCount = await ReviewingArticlesRepo.CountAsync(new ReviewingArticlesCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<ReviewingArticlesDto>(parameters.PageIndex, currentPageCount, totalCount, reviewingArticlesResult);
        }

        public async Task<ReviewingArticlesDto> GetReviewingArticleByIdAsync(int id)
        {
            // Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Reviewing Article
            var reviewingArticle = await ReviewingArticlesRepo.GetAsync(new ReviewingArticlesSpecifications(id)) 
                ?? throw new NotFoundException("Article is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, "Reviewing Article");

            //Map To Dto
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(ReviewingArticleCreateDto reviewingArticleCreateDto)
        {
            // Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Map Dto To Entity and Add Faculty Member Id
            var reviewingArticle = _mapper.Map<ReviewingArticles>(reviewingArticleCreateDto);
            reviewingArticle.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            await ReviewingArticlesRepo.AddAsync(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, ReviewArticleUpdateDto reviewingArticleUpdateDto)
        {
            //Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Reviewing Article
            var reviewingArticle = await ReviewingArticlesRepo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId)) 
                ?? throw new NotFoundException("Article is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, "Reviewing Article");

            //Map Dto To Entity
            _mapper.Map(reviewingArticleUpdateDto, reviewingArticle);

            //Update and Save Updated Data
            ReviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Reviewing Article
            var reviewingArticle = await ReviewingArticlesRepo.GetAsync(new ReviewingArticlesSpecifications(reviewingArticleId)) 
                ?? throw new NotFoundException("Article is Not Found.");

            EnsureOwnership(reviewingArticle.FacultyMemberId, currentUser.UserId, "Reviewing Article");

            //Apply Soft Delete
            reviewingArticle.IsDeleted = true;

            ReviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Participation In Magazines
        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Participation In Magazines Data
            var participationInMagazines = await ParticipationInMagazinesRepo.GetAllAsync(new ParticipationInMagazinesSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("Participation in Magazines are Not Found.");

            //Map Result in IEnumerable Wrapped Dto
            var participationIndMagazinesResult = _mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(participationInMagazines);

            //Get The Page Size
            var currentPageSize = participationInMagazines.Count();

            //Get Total Count
            var totalCount = await ParticipationInMagazinesRepo.CountAsync(new ParticipationInMagazinesCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<ParticipationInMagazinesResponseDto>(parameters.PageIndex, currentPageSize, totalCount, participationIndMagazinesResult);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Participation In Magazine Data
            var participationInMagazine = await ParticipationInMagazinesRepo.GetAsync(new ParticipationInMagazinesSpecifications(id)) 
                ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, "Participation In Magazine");

            //Map To Dto
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto To Entity and Add Faculty Member Id
            var participationInMagazine = _mapper.Map<ParticipationInMagazines>(participationInMagazinesCreateDto);
            participationInMagazine.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            await ParticipationInMagazinesRepo.AddAsync(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Participation In Magazine Data
            var participationInMagazine = await ParticipationInMagazinesRepo.GetAsync(new ParticipationInMagazinesSpecifications(participationInMagazineId)) 
                ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, "Participation In Magazine");

            //Map Dto To Entity
            _mapper.Map(participationInMagazinesUpdateDto, participationInMagazine);

            //Update and Save Updated Data
            ParticipationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Participation In Magazine Data
            var participationInMagazine = await ParticipationInMagazinesRepo.GetAsync(new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(participationInMagazine.FacultyMemberId, currentUser.UserId, "Participation In Magazine");

            //Apply Soft Delete
            participationInMagazine.IsDeleted = true;

            ParticipationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Projects
        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters)
        {
            // Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Projects Data
            var projects = await ProjectsRepo.GetAllAsync(new ProjectsSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("Projects are Not Found.");

            //Map Result In IEnumerable Wrapped Dto
            var projectsResult = _mapper.Map<IEnumerable<ProjectsResponseDto>>(projects);

            //Get The Page Size
            var currentPageCount = projects.Count();

            //Get Total Count
            var totalCount = await ProjectsRepo.CountAsync(new ProjectsCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<ProjectsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, projectsResult);
        }

        public async Task<ProjectsResponseDto> GetProjectByIdAsync(int id)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Project Data
            var project = await ProjectsRepo.GetAsync(new ProjectsSpecifications(id)) ?? throw new NotFoundException("Project is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, "Project");

            //Map To Dto
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto To Entity and Add Faculty Member Id
            var project = _mapper.Map<Projects>(projectCreateDto);
            project.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            await ProjectsRepo.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Project Data
            var project = await ProjectsRepo.GetAsync(new ProjectsSpecifications(projectId)) 
                ?? throw new NotFoundException("Project is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, "Project");

            //Map Dto To Entity
            _mapper.Map(projectUpdateDto, project);

            //Update and Save Updated Data
            ProjectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Project Data
            var project = await ProjectsRepo.GetAsync(new ProjectsSpecifications(projectId))
                ?? throw new NotFoundException("Project is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(project.FacultyMemberId, currentUser.UserId, "Project");

            //Apply Soft Delete
            project.IsDeleted = true;

            ProjectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}