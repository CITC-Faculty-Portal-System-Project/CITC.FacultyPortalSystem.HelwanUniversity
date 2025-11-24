using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Implementations
{
    public class ScientificProgressionService(IUnitOfWork _unitOfWork, IMapper _mapper, IAuthenticationService _authenticationService) : IScientificProgressionService
    {
        #region Academic Qualifications
        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters)
        {
            //Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Academic Qualifications.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Academic Qualifications Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(parameters);
            var academicQualificaions = await academicQualificationsRepo.GetAllAsync(specifications) ?? throw new NotFoundException("Academic Qualifications are Not Found");

            //Map Result in IEnumerable Wrapped Dto
            var academicQualificationsResult = _mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(academicQualificaions);

            //Get The Page Size
            var currentPageCount = academicQualificationsResult.Count();

            //Get Count of The Specifications
            var countSpecifications = new AcademicQualificationsCountSpecifications(parameters);

            //Get Total Count 
            var totalCount = await academicQualificationsRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<AcademicQualificationResponseDto>(parameters.PageIndex, currentPageCount, totalCount, academicQualificationsResult);

        }
        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id)
        {
            //Get Current User Email
            var currentUser = await _authenticationService
                 .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                 throw new UnauthorizedAccessException("Can't Access The Academic Qualification.");

            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(id);
            var academicQualificaion = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualifications are Not Found");

            if (academicQualificaion.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access this Academic Qualification.");

            //Map The Result to Dto
            var academicQualificationResult = _mapper.Map<AcademicQualificationResponseDto>(academicQualificaion);

            //Return The Result
            return academicQualificationResult;

        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto)
        {
            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto to Entity and Add FacultyMemberId
            var academicQualification = _mapper.Map<AcademicQualifications>(academicQualificationCreateDto);
            academicQualification.FacultyMemberId = currentUser.UserId;

            //Add and Save to Database
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            await academicQualificationsRepo.AddAsync(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(academicQualificationId);
            var academicQualification = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualification is Not Found.");

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            
            if (academicQualification.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update this Academic Qualification.");

            //Map Dto to Entity
            _mapper.Map(academicQualificationsUpdateDto, academicQualification);

            //Update and Save to Database
            academicQualificationsRepo.Update(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId)
        {
            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(academicQualificationId);
            var academicQualification = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualification is Not Found.");

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (academicQualification.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("Cannot Delete This Record.");

            //Apply Soft Delete
            academicQualification.IsDeleted = true;

            academicQualificationsRepo.Update(academicQualification);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Job Ranks
        public async Task<PaginatedResult<JobRankResponseDto>> GetAllJobRanksAsync(JobRanksSpecificationsParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Job Ranks.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Job Ranks Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(parameters);
            var jobRanks = await jobRanksRepo.GetAllAsync(specifications) ?? throw new NotFoundException("Job Ranks are Not Found.");

            //Map Result in IEnumerable Wrapped Dto
            var jobRanksResult = _mapper.Map<IEnumerable<JobRankResponseDto>>(jobRanks);

            //Get The Page Size
            var currentPageCount = jobRanksResult.Count();

            //Get Count of The Specifications
            var countSpecifications = new JobRanksCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await jobRanksRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<JobRankResponseDto>(parameters.PageIndex, currentPageCount, totalCount, jobRanksResult);

        }

        public async Task<JobRankResponseDto> GetJobRankByIdAsync(int id)
        {
            //Get Current User Email
            var currentUser = await _authenticationService
                 .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                 throw new UnauthorizedAccessException("Can't Access The Job Rank.");

            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(id);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            if (jobRank.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access this Job Rank.");

            //Map To Dto
            var jobRankResult = _mapper.Map<JobRankResponseDto>(jobRank);

            //Return Maapped Result
            return jobRankResult;
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(JobRankCreateDto jobRanksCreateDto)
        {
            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto to Entity and Add FacultyMemberId
            var jobRank = _mapper.Map<JobRanks>(jobRanksCreateDto);
            jobRank.FacultyMemberId = currentUser.UserId;

            //Add and Save to Database
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            await jobRanksRepo.AddAsync(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<JobRankResponseDto>(jobRank);

        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, JobRankUpdateDto jobRanksUpdateDto)
        {
            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(jobRankId);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (jobRank.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update this Job Rank.");

            //Map Dto to Entity
            _mapper.Map(jobRanksUpdateDto, jobRank);

            //Update and Save to Database
            jobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteJobRankAsync(int jobRankId)
        {
            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(jobRankId);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (jobRank.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete this Job Rank.");

            //Apply Soft Delete
            jobRank.IsDeleted = true;

            jobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Administrative Positions
        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Administrative Positions.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Administrative Positions Data
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications  = new AdministrativePositionsSpecifications(parameters);
            var administrativePositions = await administrativePositionsRepo.GetAllAsync(specifications) ?? throw new NotFoundException("Administrative Positions are Not Found");

            //Map Result in IEnumerable Wrapped Dto
            var administrativePositionsResult = _mapper.Map<IEnumerable<AdministrativePositionDto>>(administrativePositions);

            //Get The Page Size
            var currentPageCount = administrativePositionsResult.Count();

            //Get Count of The Specifications
            var countSpecifications = new AdministrativePositionsCountSpecifications(parameters);

            //Get Total Count
            var totalCount  = await administrativePositionsRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<AdministrativePositionDto>(parameters.PageIndex, currentPageCount, totalCount, administrativePositionsResult);
        }

        public async Task<AdministrativePositionDto> GetAdministrativePositionByIdAsync(int id)
        {
            // Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access This Administrative Position.");

            //Load Administrative Position
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(id);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found.");

            if (administrativePosition.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access This Administrative Position.");

            //Map To Dto
            var administrativePositionResult = _mapper.Map<AdministrativePositionDto>(administrativePosition);

            //Return Mapped Result
            return administrativePositionResult;
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(AdministrativePositionCreateDto administrativePositionCreateDto)
        {
            // Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map Dto To Entity and Add Faculty Member Id
            var administrativePosition = _mapper.Map<AdministrativePositions>(administrativePositionCreateDto);
            administrativePosition.FacultyMemberId = currentUser.UserId;

            //Add and Save To Database
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            await administrativePositionsRepo.AddAsync(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);

        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, AdministrativePositionDto administrativePositionUpdateDto)
        {
            //Load Administrative Position Data
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(administrativePositionId);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (administrativePosition.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update This Administrative Position.");

            //Map Dto To Entity
            _mapper.Map(administrativePositionUpdateDto, administrativePosition);

            //Update and Save Updated Data
            administrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId)
        {
            //Load Administrative Position Data
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(administrativePositionId);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found");

            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (administrativePosition.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete This Administrative Position.");

            //Apply Soft Delete
            administrativePosition.IsDeleted = true;

            administrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}