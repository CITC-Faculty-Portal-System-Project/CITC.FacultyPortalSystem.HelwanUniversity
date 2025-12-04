using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Implementations
{
    public class ScientificProgressionService(IUnitOfWork _unitOfWork, IMapper _mapper, IAuthenticationService _authenticationService) : IScientificProgressionService
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

        private IGenericRepository<AcademicQualifications, int> AcademicQualificationsRepo
            => _unitOfWork.GetRepository<AcademicQualifications, int>();

        private IGenericRepository<JobRanks, int> JobRanksRepo
            => _unitOfWork.GetRepository<JobRanks, int>();

        private IGenericRepository<AdministrativePositions, int> AdministrativePositionsRepo
            => _unitOfWork.GetRepository<AdministrativePositions, int>();
        #endregion

        #region Academic Qualifications
        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualifications Data
            var academicQualifications = await AcademicQualificationsRepo.GetAllAsync(new AcademicQualificationsSpecifications(parameters, currentUser.Email))
                ?? throw new NotFoundException("Academic Qualifications are Not Found");

            //Map Result in IEnumerable Wrapped Dto
            var academicQualificationsResult = _mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(academicQualifications);

            //Get The Page Size
            var currentPageCount = academicQualificationsResult.Count();

            //Get Total Count 
            var totalCount = await AcademicQualificationsRepo.CountAsync(new AcademicQualificationsCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<AcademicQualificationResponseDto>(parameters.PageIndex, currentPageCount, totalCount, academicQualificationsResult);

        }
        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualification Data
            var academicQualification = await AcademicQualificationsRepo.GetAsync(new AcademicQualificationsSpecifications(id)) ?? throw new NotFoundException("Academic Qualifications are Not Found");

            //Ensure Ownership of Data
            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, "Academic Qualification");

            //Map The Result to Dto
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto to Entity and Add FacultyMemberId
            var academicQualification = _mapper.Map<AcademicQualifications>(academicQualificationCreateDto);
            academicQualification.FacultyMemberId = currentUser.UserId;

            //Add and Save to Database
            await AcademicQualificationsRepo.AddAsync(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualification Data
            var academicQualification = await AcademicQualificationsRepo.GetAsync(new AcademicQualificationsSpecifications(academicQualificationId)) 
                ?? throw new NotFoundException("Academic Qualification is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, "Academic Qualification");

            //Map Dto to Entity
            _mapper.Map(academicQualificationsUpdateDto, academicQualification);

            //Update and Save to Database
            AcademicQualificationsRepo.Update(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualification Data
            var academicQualification = await AcademicQualificationsRepo.GetAsync(new AcademicQualificationsSpecifications(academicQualificationId)) 
                ?? throw new NotFoundException("Academic Qualification is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, "Academic Qualification");

            //Apply Soft Delete
            academicQualification.IsDeleted = true;

            AcademicQualificationsRepo.Update(academicQualification);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Job Ranks
        public async Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(JobRanksSpecificationsParameters parameters)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Job Ranks Data
            var jobRanks = await JobRanksRepo.GetAllAsync(new JobRanksSpecifications(parameters, currentUser.Email))
                ?? throw new NotFoundException("Job Ranks are Not Found.");

            //Map Result in IEnumerable Wrapped Dto
            var jobRanksResult = _mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            //Get The Page Size
            var currentPageCount = jobRanksResult.Count();

            //Get Total Count
            var totalCount = await JobRanksRepo.CountAsync(new JobRanksCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<JobRankResponseDto>(parameters.PageIndex, currentPageCount, totalCount, jobRanksResult);

        }

        public async Task<JobRankResponseDto> GetJobRankByIdAsync(int id)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Job Rank Data
            var jobRank = await JobRanksRepo.GetAsync(new JobRanksSpecifications(id)) ?? throw new NotFoundException("Job Rank is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, "Job Rank");

            //Map To Dto
            return _mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto to Entity and Add FacultyMemberId
            var jobRank = _mapper.Map<JobRanks>(jobRanksCreateDto);
            jobRank.FacultyMemberId = currentUser.UserId;

            //Add and Save to Database
            await JobRanksRepo.AddAsync(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<JobRankResponseDto>(jobRank);

        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto)
        {
            //Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Job Rank Data
            var jobRank = await JobRanksRepo.GetAsync(new JobRanksSpecifications(jobRankId)) 
                ?? throw new NotFoundException("Job Rank is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, "Job Rank");

            //Map Dto to Entity
            _mapper.Map(jobRanksUpdateDto, jobRank);

            //Update and Save to Database
            JobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteJobRankAsync(int jobRankId)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Job Rank Data
            var jobRank = await JobRanksRepo.GetAsync(new JobRanksSpecifications(jobRankId)) ?? throw new NotFoundException("Job Rank is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(jobRank.FacultyMemberId, currentUser.UserId, "Job Rank");

            //Apply Soft Delete
            jobRank.IsDeleted = true;

            JobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Administrative Positions
        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Administrative Positions Data
            var administrativePositions = await AdministrativePositionsRepo.GetAllAsync(new AdministrativePositionsSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("Administrative Positions are Not Found");

            //Map Result in IEnumerable Wrapped Dto
            var administrativePositionsResult = _mapper.Map<IEnumerable<AdministrativePositionDto>>(administrativePositions);

            //Get The Page Size
            var currentPageCount = administrativePositionsResult.Count();

            //Get Total Count
            var totalCount  = await AdministrativePositionsRepo.CountAsync(new AdministrativePositionsCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<AdministrativePositionDto>(parameters.PageIndex, currentPageCount, totalCount, administrativePositionsResult);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Load Administrative Position
            var administrativePosition = await AdministrativePositionsRepo.GetAsync(new AdministrativePositionsSpecifications(id)) 
                ?? throw new NotFoundException("Administrative Position is Not Found.");

            //Ensure Ownership of Data
            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, "Administrative Position");

            //Map To Dto
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto)
        {
            // Get Current User 
            var currentUser = await GetCurrentUserAsync();

            //Map Dto To Entity and Add Faculty Member Id
            var administrativePosition = _mapper.Map<AdministrativePositions>(administrativePositionCreateDto);
            administrativePosition.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            await AdministrativePositionsRepo.AddAsync(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);

        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Administrative Position Data
            var administrativePosition = await AdministrativePositionsRepo.GetAsync(new AdministrativePositionsSpecifications(administrativePositionId)) 
                ?? throw new NotFoundException("Administrative Position is Not Found");

            //Ensure Ownership of Data
            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, "Administrative Position");

            //Map Dto To Entity
            _mapper.Map(administrativePositionUpdateDto, administrativePosition);

            //Update and Save Updated Data
            AdministrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Administrative Position Data
            var administrativePosition = await AdministrativePositionsRepo.GetAsync(new AdministrativePositionsSpecifications(administrativePositionId)) 
                ?? throw new NotFoundException("Administrative Position is Not Found");

            //Ensure Ownership of Data
            EnsureOwnership(administrativePosition.FacultyMemberId, currentUser.UserId, "Administrative Position");

            //Apply Soft Delete
            administrativePosition.IsDeleted = true;

            AdministrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}