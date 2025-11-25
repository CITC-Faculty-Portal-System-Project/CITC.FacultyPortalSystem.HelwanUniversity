using Domain.Entities.MissionsModule;
using Services.Specifications.MissionsModule;
using Shared.Dtos.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Implementations
{
    public class MissionsService(IUnitOfWork _unitOfWork, IMapper _mapper , IAuthenticationService _authenticationService)
        : IMissionsService
    {
        #region Scientific Missions
        public async Task<PaginatedResult<ScientificMissionResponseDto?>> GetAllScientificMissionsAsync(ScientificMissionSpecificationParamaters parameters)
        {
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Scientific Mission.");

            parameters.FacultyMemberEmail = currentUser.Email;

            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new ScientificMissionsSpecifications(parameters);
            var scientificMissions = await scientificMissionsRepo.GetAllAsync(specification) ?? throw new NotFoundException("No Missions are Found.");

            var scientificMissionsResult = _mapper.Map<IEnumerable<ScientificMissionResponseDto>>(scientificMissions);

            var currentPageCount = scientificMissions.Count();

            var countSpecifications = new ScientificMissionsCountSpecification(parameters);

            var totalCount = await scientificMissionsRepo.CountAsync(countSpecifications);

            return new PaginatedResult<ScientificMissionResponseDto?>(parameters.PageIndex, currentPageCount, totalCount, scientificMissionsResult);

        }

        public async Task<ScientificMissionResponseDto?> GetScientificMissionByIdAsync(int id)
        {
            var currentUser = await _authenticationService
                 .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                 throw new UnauthorizedAccessException("Can't Access The Mission.");


            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specfifcation = new ScientificMissionsSpecifications(id);
            var scientificMission = await scientificMissionsRepo.GetAsync(specfifcation) ?? throw new NotFoundException("Mission is Not Found.");

            if (scientificMission.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access this Mission.");

            var scientificMissionResult = _mapper.Map<ScientificMissionResponseDto>(scientificMission);
            return scientificMissionResult;

        }
        public async Task<ScientificMissionResponseDto> CreateScientificMissionAsync(ScientificMissionCreateDto scientificMissionCreateDto)
        {
            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var scientificMission = _mapper.Map<ScientificMissions>(scientificMissionCreateDto);
            scientificMission.FacultyMemberId = currentUser.UserId;

            await scientificMissionsRepo.AddAsync(scientificMission);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScientificMissionResponseDto>(scientificMission);

        }

        public async Task<ScientificMissionResponseDto> UpdateScientificMissionAsync(int id, ScientificMissionUpdateDto scientificMissionUpdateDto)
        {
            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new ScientificMissionsSpecifications(id);
            var scientificMission = await scientificMissionsRepo.GetAsync(specification)
                ?? throw new NotFoundException("Mission isnot Found!");

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            if (scientificMission.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Edit this Mission");

            scientificMission = _mapper.Map(scientificMissionUpdateDto, scientificMission);

            scientificMissionsRepo.Update(scientificMission);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScientificMissionResponseDto>(scientificMissionUpdateDto); ;

        }

        public async Task DeleteScientificMissionAsync(int id)
        {
            var email = _authenticationService.GetLoggedUserEmail();

            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new ScientificMissionsSpecifications(id);
            var scientificMission = await scientificMissionsRepo.GetAsync(specification) ?? throw new NotFoundException("Cannot Find this Mission.");

            if (scientificMission?.FacultyMember?.Email != email)
                throw new UnauthorizedAccessException("You Don't Have Acess to Delete this Mission");

            scientificMission.IsDeleted = true;

            scientificMissionsRepo.Update(scientificMission);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Seminars And Conferences
        public async Task<PaginatedResult<ConferencesAndSeminarsResponseDto>> GetAllSeminarsAndConferencesAsync(SeminarsAndConferncesSpecificationParameters parameters)
        {
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Get Confernce Or Seminar");

            parameters.FacultyMemberEmail = currentUser.Email;

            var conferencesAndSeminarsRepo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specification = new ConferncesAndSeminarsSpecification(parameters);
            var conferenceOrSeminar = await conferencesAndSeminarsRepo.GetAllAsync(specification) ?? throw new NotFoundException("There are No Seminars");

            var conferenceOrSeminarResult = _mapper.Map<IEnumerable<ConferencesAndSeminarsResponseDto>>(conferenceOrSeminar);

            var currentPageCount = conferenceOrSeminar.Count();

            var countSpecifications = new ConferncesAndSeminarsCountSpecification(parameters);

            var totalCount = await conferencesAndSeminarsRepo.CountAsync(countSpecifications);

            return new PaginatedResult<ConferencesAndSeminarsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, conferenceOrSeminarResult);

        }

        public async Task<ConferencesAndSeminarsResponseDto> GetSeminarOrConferenceByIdAsync(int id)
        {

            var currentUser = await _authenticationService
                        .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                        throw new UnauthorizedAccessException("You Cannot Get Confernce Or Seminar");


            var conferencesAndSeminarsRepo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specfifcation = new ConferncesAndSeminarsSpecification(id);
            var conferenceOrSeminar = await conferencesAndSeminarsRepo.GetAsync(specfifcation) ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            if (conferenceOrSeminar.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access this Seminar or Conference.");

            var conferenceOrSeminarResult = _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
            return conferenceOrSeminarResult;

        }

        public async Task<ConferencesAndSeminarsResponseDto> CreateSeminarOrConferenceAsync(ConferencesAndSeminarsCreateDto conferencesAndSeminarsCreateDto)
        {
            var currentUser = await _authenticationService
                    .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                    throw new UnauthorizedAccessException("You Cannot Add Confernce Or Seminar");

            var conferencesAndSeminarsRepo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();

            var conferenceOrSeminar = _mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsCreateDto);
            conferenceOrSeminar.FacultyMemberId = currentUser.UserId;

            await conferencesAndSeminarsRepo.AddAsync(conferenceOrSeminar);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
        }

        public async Task<ConferencesAndSeminarsResponseDto> UpdateSeminarOrConferenceAsync(int id, ConferencesAndSeminarsUpdateDto conferencesAndSeminarsUpdateDto)
        {
            var currentUser = await _authenticationService
                                 .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                                 throw new UnauthorizedAccessException("You Cannot Update Confernce Or Seminar");

            var conferencesAndSeminarsRepo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specfifcation = new ConferncesAndSeminarsSpecification(id);
            var conferenceOrSeminar = await conferencesAndSeminarsRepo.GetAsync(specfifcation) ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            if (conferenceOrSeminar.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Update this Seminars");

            _mapper.Map<ConferencesAndSeminars>(conferencesAndSeminarsUpdateDto);

            conferencesAndSeminarsRepo.Update(conferenceOrSeminar);
            await _unitOfWork.SaveChangesAsync();

            var conferenceOrSeminarResult = _mapper.Map<ConferencesAndSeminarsResponseDto>(conferenceOrSeminar);
            return conferenceOrSeminarResult;
        }

        public async Task DeleteSeminarOrConferenceAsync(int id)
        {
            var currentUser = await _authenticationService
                    .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                    throw new UnauthorizedAccessException("You Cannot Delete Confernce Or Seminar");

            var conferencesAndSeminarsRepo = _unitOfWork.GetRepository<ConferencesAndSeminars, int>();
            var specification = new ConferncesAndSeminarsSpecification(id);
            var conferenceOrSeminar = await conferencesAndSeminarsRepo.GetAsync(specification) ?? throw new NotFoundException("Seminar or Conference is Not Found.");

            if (conferenceOrSeminar.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Delete Confernce Or Seminar");

            conferencesAndSeminarsRepo.Update(conferenceOrSeminar);

            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Training Programs
        public async Task<PaginatedResult<TrainingProgramsResponseDto>> GetAllTrainingProgramsAsync(TrainingProgramsSpecificationParameters parameters)
        {
            //Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Training Programs Data.");

            parameters.FacultyMemberEmail = currentUser.Email;

            //Load Training Programs Data
            var trainingProgramsRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
            var specification = new TrainingProgramsSpecifications(parameters);
            var trainingPrograms = await trainingProgramsRepo.GetAllAsync(specification) ?? throw new NotFoundException("No Training Programs are Found.");

            //Map Result in IEnumerable Wrapped Dto
            var trainingProgramsResult = _mapper.Map<IEnumerable<TrainingProgramsResponseDto>>(trainingPrograms);

            //Get The Page Size
            var currentPageCount = trainingPrograms.Count();

            //Get Count of The Specifications
            var countSpecifications = new TrainingProgramsCountSpecifications(parameters);

            //Get Total Count
            var totalCount = await trainingProgramsRepo.CountAsync(countSpecifications);

            //Return Paginated Result
            return new PaginatedResult<TrainingProgramsResponseDto>(parameters.PageIndex, currentPageCount, totalCount, trainingProgramsResult);
        }

        public async Task<TrainingProgramsResponseDto> GetTrainingProgramByIdAsync(int id)
        {
            //Get Current User Email
            var currentUser = await _authenticationService
                            .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail()) ??
                            throw new UnauthorizedAccessException("You Cannot Access The Academic Qualifications.");

            //Load Training Program Data
            var trainingProgramsRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
            var specfifcation = new TrainingProgramsSpecifications(id);
            var trainingProgram = await trainingProgramsRepo.GetAsync(specfifcation) ?? throw new NotFoundException("Training Program is Not Found.");

            if(trainingProgram.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Cannot Access this Training Program.");

            //Map To Dto
            var trainingProgramResult = _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);

            //Return Result Data
            return trainingProgramResult;
        }

        public async Task<TrainingProgramsResponseDto> CreateTrainingProgramAsync(TrainingProgramsCreateDto trainingProgramsCreateDto)
        {
            //Get Current User Email
            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            //Map To Entity
            var trainingProgram = _mapper.Map<TrainingPrograms>(trainingProgramsCreateDto);
            trainingProgram.FacultyMemberId = currentUser.UserId;

            //Add And Save Training Program Data
            var trainingProgramsRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
            await trainingProgramsRepo.AddAsync(trainingProgram);

            await _unitOfWork.SaveChangesAsync();

            //Return Result Data
            return _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task<TrainingProgramsResponseDto> UpdateTrainingProgramAsync(int id, TrainingProgramsUpdateDto trainingProgramsUpdateDto)
        {
            //Load Academic Qualification Data
            var trainingProgramsRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
            var specifications = new TrainingProgramsSpecifications(id);
            var trainingProgram = await trainingProgramsRepo.GetAsync(specifications) ?? throw new NotFoundException("Training Program is Not Found.");

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (trainingProgram.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Update this Training Program.");

            //Map Dto to Entity
            _mapper.Map(trainingProgramsUpdateDto, trainingProgram);

            //Update and Save to Database
            trainingProgramsRepo.Update(trainingProgram);
            await _unitOfWork.SaveChangesAsync();

            //Return Updated Data
            return _mapper.Map<TrainingProgramsResponseDto>(trainingProgram);
        }

        public async Task DeleteTrainingProgramAsync(int id)
        {
            //Load Academic Qualification Data
            var trainingProgramsRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
            var specifications = new TrainingProgramsSpecifications(id);
            var trainingProgram = await trainingProgramsRepo.GetAsync(specifications) ?? throw new NotFoundException("Training Program is Not Found.");

            var currentUser = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            if (trainingProgram.FacultyMemberId != currentUser.UserId)
                throw new UnauthorizedAccessException("You Can't Delete this Training Program.");

            //Apply Soft Delete
            trainingProgram.IsDeleted = true;

            trainingProgramsRepo.Update(trainingProgram);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
