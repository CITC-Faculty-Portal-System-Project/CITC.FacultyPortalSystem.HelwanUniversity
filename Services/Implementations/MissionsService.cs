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

        public async Task<bool> DeleteScientificMissionAsync(int id)
        {
            var email = _authenticationService.GetLoggedUserEmail();

            var scientificMissionsRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
            var specification = new ScientificMissionsSpecifications(id);
            var scientificMission = await scientificMissionsRepo.GetAsync(specification) ?? throw new NotFoundException("Cannot Find this Mission.");

            if (scientificMission?.FacultyMember?.Email != email)
                throw new UnauthorizedAccessException("You Don't Have Acess to Delete this Mission");

            scientificMission.IsDeleted = true;

            scientificMissionsRepo.Update(scientificMission);
            return await _unitOfWork.SaveChangesAsync() > 0;
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

        public async Task<bool> DeleteSeminarOrConferenceAsync(int id)
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

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
