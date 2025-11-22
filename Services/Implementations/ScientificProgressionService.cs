using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Implementations
{
    public class ScientificProgressionService(IUnitOfWork _unitOfWork, IMapper _mapper) : IScientificProgressionService
    {
        #region Academic Qualifications
        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters)
        {
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
            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(id);
            var academicQualificaion = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualifications are Not Found");

            //Map The Result to Dto
            var academicQualificationResult = _mapper.Map<AcademicQualificationResponseDto>(academicQualificaion);

            //Return The Result
            return academicQualificationResult;

        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(string facultyMemberEmail, AcademicQualificationCreateDto academicQualificationCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto to Entity and Add FacultyMemberId
            var academicQualification = _mapper.Map<AcademicQualifications>(academicQualificationCreateDto);
            academicQualification.FacultyMemberId = facultyMember.Id;

            //Add and Save to Database
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            await academicQualificationsRepo.AddAsync(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, string facultyMemberEmail, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(academicQualificationId);
            var academicQualification = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualification is Not Found.");

            if (academicQualification.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto to Entity
            _mapper.Map(academicQualificationsUpdateDto, academicQualification);

            //Update and Save to Database
            academicQualificationsRepo.Update(academicQualification);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId, string facultyMemberEmail)
        {
            //Load Academic Qualification Data
            var academicQualificationsRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
            var specifications = new AcademicQualificationsSpecifications(academicQualificationId);
            var academicQualification = await academicQualificationsRepo.GetAsync(specifications) ?? throw new NotFoundException("Academic Qualification is Not Found.");

            if (academicQualification.FacultyMember?.Email != facultyMemberEmail)
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

        public async Task<JobRankResponseDto> GetJobRankById(int id)
        {
            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(id);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            //Map To Dto
            var jobRankResult = _mapper.Map<JobRankResponseDto>(jobRank);

            //Return Maapped Result
            return jobRankResult;
        }

        public async Task<JobRankResponseDto> CreateJobRankAsync(string facultyMemberEmail, JobRankCreateDto jobRanksCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto to Entity and Add FacultyMemberId
            var jobRank = _mapper.Map<JobRanks>(jobRanksCreateDto);
            jobRank.FacultyMemberId = facultyMember.Id;

            //Add and Save to Database
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            await jobRanksRepo.AddAsync(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data to Response Dto
            return _mapper.Map<JobRankResponseDto>(jobRank);

        }

        public async Task<JobRankResponseDto> UpdateJobRankAsync(int jobRankId, string facultyMemberEmail, JobRankUpdateDto jobRanksUpdateDto)
        {
            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(jobRankId);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            if (jobRank.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto to Entity
            _mapper.Map(jobRanksUpdateDto, jobRank);

            //Update and Save to Database
            jobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<JobRankResponseDto>(jobRank);
        }

        public async Task DeleteJobRankAsync(int jobRankId, string facultyMemberEmail)
        {
            //Load Job Rank Data
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
            var specifications = new JobRanksSpecifications(jobRankId);
            var jobRank = await jobRanksRepo.GetAsync(specifications) ?? throw new NotFoundException("Job Rank is Not Found.");

            if (jobRank.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            jobRank.IsDeleted = true;

            jobRanksRepo.Update(jobRank);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Administrative Positions
        public async Task<PaginatedResult<AdministrativePositionDto>> GetAllAdministrativePositionsAsync(AdministrativePositionsSpecificationParameters parameters)
        {
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

        public async Task<AdministrativePositionDto> GetAdministrativePositionById(int id)
        {
            //Load Administrative Position
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(id);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found.");

            //Map To Dto
            var administrativePositionResult = _mapper.Map<AdministrativePositionDto>(administrativePosition);

            //Return Mapped Result
            return administrativePositionResult;
        }

        public async Task<AdministrativePositionDto> CreateAdministrativePositionAsync(string facultyMemberEmail, AdministrativePositionCreateDto administrativePositionCreateDto)
        {
            //Load Faculty Member Data
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var specifications = new FacultyMemberWithEmailSpecifications(facultyMemberEmail);
            var facultyMember = await facultyMemberRepo.GetAsync(specifications) ?? throw new NotFoundException("Faculty Member is Not Found.");

            //Map Dto To Entity and Add Faculty Member Id
            var administrativePosition = _mapper.Map<AdministrativePositions>(administrativePositionCreateDto);
            administrativePosition.FacultyMemberId = facultyMember.Id;

            //Add and Save To Database
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            await administrativePositionsRepo.AddAsync(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return The Mapped Data To Response Dto
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);

        }

        public async Task<AdministrativePositionDto> UpdateAdministrativePositionAsync(int administrativePositionId, string facultyMemberEmail, AdministrativePositionDto administrativePositionUpdateDto)
        {
            //Load Administrative Position Data
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(administrativePositionId);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found");

            if (administrativePosition.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Map Dto To Entity
            _mapper.Map(administrativePositionUpdateDto, administrativePosition);

            //Update and Save Updated Data
            administrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Result
            return _mapper.Map<AdministrativePositionDto>(administrativePosition);
        }

        public async Task DeleteAdministrativePositionAsync(int administrativePositionId, string facultyMemberEmail)
        {
            //Load Administrative Position Data
            var administrativePositionsRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
            var specifications = new AdministrativePositionsSpecifications(administrativePositionId);
            var administrativePosition = await administrativePositionsRepo.GetAsync(specifications) ?? throw new NotFoundException("Administrative Position is Not Found");

            if (administrativePosition.FacultyMember?.Email != facultyMemberEmail)
                throw new UnauthorizedAccessException("Cannot Update This Record.");

            //Apply Soft Delete
            administrativePosition.IsDeleted = true;

            administrativePositionsRepo.Update(administrativePosition);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}