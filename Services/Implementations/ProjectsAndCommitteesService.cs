using Domain.Entities.ProjectsAndCommitteesModule;
using Services.Specifications.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Implementations
{
    public class ProjectsAndCommitteesService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProjectsAndCommitteesService
    {
        #region Committees And Associations
        public async Task<PaginatedResult<CommitteesAndAssociationsResponseDto>> GetAllCommitteesAndAssociationsAsync(CommitteesAndAssociationsSpecificationsParameters parameters)
        {
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
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(id);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            //Map To Dto
            var committeeOrAssociationResult = _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);

            //Return Result Data
            return committeeOrAssociationResult;
        }

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(string facultyMemberEmail, CommitteeOrAssociationCreateDto committeeOrAssociationCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var committeeOrAssociation = _mapper.Map<CommitteesAndAssociations>(committeeOrAssociationCreateDto);
            committeeOrAssociation.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            await committeesAndAssociationsRepo.AddAsync(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }
        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail, CommitteeOrAssociationUpdateDto committeeOrAssociationUpdateDto)
        {
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(committeeOrAssociationId);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            if (committeeOrAssociation.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(committeeOrAssociationUpdateDto, committeeOrAssociation);

            //Update and Save Updated Data
            committeesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }

        public async Task DeleteCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail)
        {
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(committeeOrAssociationId);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            if (committeeOrAssociation.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            committeeOrAssociation.IsDeleted = true;

            committeesAndAssociationsRepo.Update(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Reviewing Articles
        public async Task<PaginatedResult<ReviewingArticlesDto>> GetAllReviewingArticlesAsync(ReviewingArticlesSpecificationsParameters parameters)
        {
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
            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(id);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            //Map To Dto
            var reviewingArticleResult = _mapper.Map<ReviewingArticlesDto>(reviewingArticle);

            //Return Mapped Data
            return reviewingArticleResult;
        }

        public async Task<ReviewingArticlesDto> CreateReviewingArticleAsync(string facultyMemberEmail, ReviewingArticleCreateDto reviewingArticleCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var reviewingArticle = _mapper.Map<ReviewingArticles>(reviewingArticleCreateDto);
            reviewingArticle.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            await reviewingArticlesRepo.AddAsync(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task<ReviewingArticlesDto> UpdateReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail, ReviewingArticlesDto reviewingArticleUpdateDto)
        {
            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(reviewingArticleId);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            if (reviewingArticle.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(reviewingArticleUpdateDto, reviewingArticle);

            //Update and Save Updated Data
            reviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ReviewingArticlesDto>(reviewingArticle);
        }

        public async Task DeleteReviewingArticleAsync(int reviewingArticleId, string facultyMemberEmail)
        {
            //Load Reviewing Article
            var reviewingArticlesRepo = _unitOfWork.GetRepository<ReviewingArticles, int>();
            var specifications = new ReviewingArticlesSpecifications(reviewingArticleId);
            var reviewingArticle = await reviewingArticlesRepo.GetAsync(specifications) ?? throw new NotFoundException("Article is Not Found.");

            if (reviewingArticle.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            reviewingArticle.IsDeleted = true;

            reviewingArticlesRepo.Update(reviewingArticle);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Participation In Magazines
        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(ParticipationInMagazinesSpecificationsParameters parameters)
        {
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
            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(id);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            //Map To Dto
            var participationInMagazineResult = _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);

            //Return Mapped Data
            return participationInMagazineResult;
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(string facultyMemberEmail, ParticipationInMagazineCreateDto participationInMagazinesCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var participationInMagazine = _mapper.Map<ParticipationInMagazines>(participationInMagazinesCreateDto);
            participationInMagazine.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            await participationInMagazinesRepo.AddAsync(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(int participationInMagazineId, string facultyMemberEmail, ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(participationInMagazineId);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            if (participationInMagazine.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(participationInMagazinesUpdateDto, participationInMagazine);

            //Update and Save Updated Data
            participationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId, string facultyMemberEmail)
        {
            //Load Participation In Magazine Data
            var participationInMagazinesRepo = _unitOfWork.GetRepository<ParticipationInMagazines, int>();
            var specifications = new ParticipationInMagazinesSpecifications(participationInMagazineId);
            var participationInMagazine = await participationInMagazinesRepo.GetAsync(specifications) ?? throw new NotFoundException("Participation in Magazine is Not Found.");

            if (participationInMagazine.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            participationInMagazine.IsDeleted = true;

            participationInMagazinesRepo.Update(participationInMagazine);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Projects
        public async Task<PaginatedResult<ProjectsResponseDto>> GetAllProjectsAsync(ProjectsSpecifcationsParameters parameters)
        {
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
            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(id);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            //Map To Dto
            var projectResult = _mapper.Map<ProjectsResponseDto>(project);

            //Return Mapped Result
            return projectResult;
        }

        public async Task<ProjectsResponseDto> CreateProjectAsync(string facultyMemberEmail, ProjectCreateDto projectCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var project = _mapper.Map<Projects>(projectCreateDto);
            project.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            await projectsRepo.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task<ProjectsResponseDto> UpdateProjectAsync(int projectId, string facultyMemberEmail, ProjectUpdateDto projectUpdateDto)
        {
            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(projectId);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            if (project.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(projectUpdateDto, project);

            //Update and Save Updated Data
            projectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<ProjectsResponseDto>(project);
        }

        public async Task DeleteProjectAsync(int projectId, string facultyMemberEmail)
        {
            //Load Project Data
            var projectsRepo = _unitOfWork.GetRepository<Projects, int>();
            var specification = new ProjectsSpecifications(projectId);
            var project = await projectsRepo.GetAsync(specification) ?? throw new NotFoundException("Project is Not Found.");

            if (project.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            project.IsDeleted = true;

            projectsRepo.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}