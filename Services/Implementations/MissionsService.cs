using Domain.Entities.MissionsModule;
using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.MissionsModule;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Implementations
{
    public class MissionsService(IUnitOfWork _unitOfWork, IMapper _mapper , IAuthenticationService _authenticationService)
        : IMissionsService
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

        private IGenericRepository<ScientificMissions, int> ScientificMissionsRepo
            => _unitOfWork.GetRepository<ScientificMissions, int>();

        private IGenericRepository<ConferencesAndSeminars, int> ConferencesAndSeminarsRepo
            => _unitOfWork.GetRepository<ConferencesAndSeminars, int>();

        private IGenericRepository<TrainingPrograms, int> TrainingProgramsRepo
            => _unitOfWork.GetRepository<TrainingPrograms, int>();
        #endregion

        #region Scientific Missions
        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(ScientificMissionSpecificationParamaters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMissions = await ScientificMissionsRepo.GetAllAsync(new ScientificMissionsSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("No Missions are Found.");

            var scientificMissionsResult = _mapper.Map<IEnumerable<ScientificMissionResponseDto>>(scientificMissions);

            var currentPageCount = scientificMissions.Count();

            var totalCount = await ScientificMissionsRepo.CountAsync(new ScientificMissionsCountSpecification(parameters, currentUser.Email));

            return new PaginatedResult<ScientificMissionResponseDto?>(parameters.PageIndex, currentPageCount, totalCount, scientificMissionsResult);

        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await ScientificMissionsRepo.GetAsync(new ScientificMissionsSpecifications(id)) 
                ?? throw new NotFoundException("Mission is Not Found.");

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, "Scientific Mission");

            return _mapper.Map<ScientificMissionResponseDto>(scientificMission);
        }

        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = _mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = currentUser.UserId;

            await ScientificMissionsRepo.AddAsync(scientificMission);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScientificMissionResponseDto>(scientificMission);

        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto scientificMissionUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var scientificMission = await ScientificMissionsRepo.GetAsync(new ScientificMissionsSpecifications(id))
                ?? throw new NotFoundException("Mission isnot Found!");

            EnsureOwnership(scientificMission.FacultyMemberId, currentUser.UserId, "Scientific Mission");

            scientificMission = _mapper.Map(scientificMissionUpdateDto, scientificMission);

            ScientificMissionsRepo.Update(scientificMission);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScientificMissionResponseDto>(scientificMissionUpdateDto); ;

        }

        public async Task DeleteScientificMissionAsync(int id)
        {
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var scientificMission = await ScientificMissionsRepo.GetAsync(new ScientificMissionsSpecifications(id)) 
                ?? throw new NotFoundException("Cannot Find this Mission.");

            
            if (scientificMission.FacultyMemberId != currentUser.UserId)
                    throw new UnauthorizedAccessException("You Don't Have Acess to Delete this Mission");

            scientificMission.IsDeleted = true;

            ScientificMissionsRepo.Update(scientificMission);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Seminars And Conferences
        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await ConferencesAndSeminarsRepo.GetAllAsync(new ConferncesAndSeminarsSpecification(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("There are No Seminars");

            var conferenceOrSeminarResult = _mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferenceOrSeminar);

            var currentPageCount = conferenceOrSeminar.Count();

            var totalCount = await ConferencesAndSeminarsRepo.CountAsync(new ConferncesAndSeminarsCountSpecification(parameters, currentUser.Email));

            return new PaginatedResult<ConferencesAndSeminarsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, conferenceOrSeminarResult);
        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id)
        {

            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await ConferencesAndSeminarsRepo.GetAsync(new ConferncesAndSeminarsSpecification(id)) 
                ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, "Conference Or Seminar");

            return _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = _mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
            conferenceOrSeminar.FacultyMemberId = currentUser.UserId;

            await ConferencesAndSeminarsRepo.AddAsync(conferenceOrSeminar);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await ConferencesAndSeminarsRepo.GetAsync(new ConferncesAndSeminarsSpecification(id)) 
                ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, "Conference Or Seminar");

            _mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsUpdateDto);

            ConferencesAndSeminarsRepo.Update(conferenceOrSeminar);
            await _unitOfWork.SaveChangesAsync();

            var conferenceOrSeminarResult = _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
            return conferenceOrSeminarResult;
        }

        public async Task DeleteSeminarOrConferenceAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var conferenceOrSeminar = await ConferencesAndSeminarsRepo.GetAsync(new ConferncesAndSeminarsSpecification(id))
                ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            EnsureOwnership(conferenceOrSeminar.FacultyMemberId, currentUser.UserId, "Conference Or Seminar");

            ConferencesAndSeminarsRepo.Update(conferenceOrSeminar);

            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Training Programs
        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(TrainingProgramsSpecificationParameters parameters)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Training Programs Data
            var trainingPrograms = await TrainingProgramsRepo.GetAllAsync(new TrainingProgramsSpecifications(parameters, currentUser.Email)) 
                ?? throw new NotFoundException("No Training Programs are Found.");

            //Map Result in IEnumerable Wrapped Dto
            var trainingProgramsResult = _mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            //Get The Page Size
            var currentPageCount = trainingPrograms.Count();

            //Get Total Count
            var totalCount = await TrainingProgramsRepo.CountAsync(new TrainingProgramsCountSpecifications(parameters, currentUser.Email));

            //Return Paginated Result
            return new PaginatedResult<TrainingProgramsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, trainingProgramsResult);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Load Training Program Data
            var trainingProgram = await TrainingProgramsRepo.GetAsync(new TrainingProgramsSpecifications(id)) 
                ?? throw new NotFoundException("Training Program is Not Found.");

            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, "Training Program");

            //Map To Dto
            return _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(TrainingProgramsCreateDto trainingProgramsCreateDto)
        {
            //Get Current User Email
            var currentUser = await GetCurrentUserAsync();

            //Map To Entity
            var trainingProgram = _mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = currentUser.UserId;

            //Add And Save Training Program Data
            await TrainingProgramsRepo.AddAsync(trainingProgram);

            await _unitOfWork.SaveChangesAsync();

            //Return Result Data
            return _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(int id, TrainingProgramsUpdateDto trainingProgramsUpdateDto)
        {
            //Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualification Data
            var trainingProgram = await TrainingProgramsRepo.GetAsync(new TrainingProgramsSpecifications(id)) 
                ?? throw new NotFoundException("Training Program is Not Found.");

            //Check OwnerShip of Data
            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, "Training Program");

            //Map Dto to Entity
            _mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            //Update and Save to Database
            TrainingProgramsRepo.Update(trainingProgram);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(int id)
        {
            //Get Current User
            var currentUser = await GetCurrentUserAsync();

            //Load Academic Qualification Data
            var trainingProgram = await TrainingProgramsRepo.GetAsync(new TrainingProgramsSpecifications(id))
                ?? throw new NotFoundException("Training Program is Not Found.");

            //Check OwnerShip of Data
            EnsureOwnership(trainingProgram.FacultyMemberId, currentUser.UserId, "Training Program");

            //Apply Soft Delete
            trainingProgram.IsDeleted = true;

            TrainingProgramsRepo.Update(trainingProgram);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
