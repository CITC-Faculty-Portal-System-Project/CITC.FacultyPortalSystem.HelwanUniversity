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

        public async Task<CommitteesAndAssociationsResponseDto> GetCommitteeOrAssociationById(int id)
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

        public async Task<CommitteesAndAssociationsResponseDto> CreateCommitteeOrAssociationAsync(string facultyMemberEmail, CommitteesAndAssociationsCreateDto committeesAndAssociationsCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var committeeOrAssociation = _mapper.Map<CommitteesAndAssociations>(committeesAndAssociationsCreateDto);
            committeeOrAssociation.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            await committeesAndAssociationsRepo.AddAsync(committeeOrAssociation);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<CommitteesAndAssociationsResponseDto>(committeeOrAssociation);
        }
        public async Task<CommitteesAndAssociationsResponseDto> UpdateCommitteeOrAssociationAsync(int committeeOrAssociationId, string facultyMemberEmail, CommitteesAndAssociationsUpdateDto committeesAndAssociationsUpdateDto)
        {
            //Load Committee Or Association Data
            var committeesAndAssociationsRepo = _unitOfWork.GetRepository<CommitteesAndAssociations, int>();
            var specifications = new CommitteesAndAssociationsSpecifications(committeeOrAssociationId);
            var committeeOrAssociation = await committeesAndAssociationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Committee Or Association is Not Found.");

            if (committeeOrAssociation.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(committeesAndAssociationsUpdateDto, committeeOrAssociation);

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

        public async Task<ReviewingArticlesDto> GetReviewingArticleById(int id)
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
    }
}