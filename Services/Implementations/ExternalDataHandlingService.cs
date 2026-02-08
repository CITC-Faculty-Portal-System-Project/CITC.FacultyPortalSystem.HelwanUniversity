using Services.Specifications.AcademicDataModule.MissionsModule;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Services.Specifications.AcademicDataModule.HigherStudiesModule;
using Services.Helpers.ExternalDataFetchingServiceHelpers;
using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;

namespace Services.Implementations
{
    public class ExternalDataHandlingService(IUnitOfWork _unitOfWork , IMapper _mapper 
        , IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper 
        _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper) : IExternalDataHandlingService
    {
        public async Task<bool> AcademicDataHandle(string? json)
        {
            var academicRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();

            return await BulkHelper.HandleAsync<
                AcademicQualificationFetchingDTO,
                AcademicQualificationCreateDto,
                AcademicQualifications, 
                int
            >(
                json,
                async item =>
                {
                    var spec = new AcademicQualificationsSpecifications(item);

                    if (await academicRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<AcademicQualificationCreateDto>(item);

                    dto.DispatchId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Dispatch);
                    dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);
                    dto.QualificationId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Qualification);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
                academicRepo,
                _unitOfWork
            );
        }

		public async Task<bool> ContactDataHandle(string? json)
		{
            var contactRepo = _unitOfWork.GetRepository<ContactData, int>();

            return await BulkHelper.HandleAsync<
                ContactDataFetchingDTO,
                ContactDataCreateDTO,
                ContactData,
                int
            >(
                json,
                async item =>
                {
                    var spec = new ContactDataWithExternalServiceSpecification(item);
                    if (await contactRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<ContactDataCreateDTO>(item);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
                contactRepo,
                _unitOfWork
            );
        }

        public async Task<bool> EmploymentDataHandle(string? json)
        {
            var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();

            return await BulkHelper.HandleAsync<
                JobRanksFetchingDTO,
                JobRankCreateDto,
                JobRanks,
                int
            >(
                json,
                async item =>
                {
                    var spec = new JobRanksSpecifications(item);
                    if (await jobRanksRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<JobRankCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.JobRankId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Name);
                    return dto;
                },
                _mapper,
                jobRanksRepo,
                _unitOfWork
            );
        }

        public async Task<bool> ManagerialDataHandle(string? json)
        {
            var adminRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();

            return await BulkHelper.HandleAsync<
                AdminstrativePostionsFetchingDTO,
                AdministrativePositionCreateDto,
                AdministrativePositions,
                int
            >(
                json,
                async item =>
                {
                    var spec = new AdministrativePositionsSpecifications(item);
                    if (await adminRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<AdministrativePositionCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    dto.Notes = item.Description;
                    dto.Position = item.Name;

                    return dto;
                },
                _mapper,
                adminRepo,
                _unitOfWork
            );
        }

		public async Task<bool> PersonalDataHandle(string? json)
		{
            var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

            return await BulkHelper.HandleAsync<
                PersonalDataFetchingDTO,
                PersonalDataCreateDTO,
                PersonalData,
                int
            >(
                json,
                async item =>
                {
                    var spec = new PersonalDataWithIncludesSpecifications(item);
                    if (await personalDataRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<PersonalDataCreateDTO>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.TitleId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Title);
                    dto.GenderId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Gender);
                    dto.MaritalStatusId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.SocialStatus);
                    dto.AuthorityId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.FacultyName);
                    dto.DepartmentId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Department);
                    dto.FieldId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.FieldOfStudy);
                    dto.UniversityId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.University);

                    return dto;
                },
                _mapper,
                personalDataRepo,
                _unitOfWork
            );
        }

		public async Task<bool> ScientificDutyDataHandle(string? json)
		{
            var missionRepo = _unitOfWork.GetRepository<ScientificMissions, int>();

            return await BulkHelper.HandleAsync<
                SceintificMissionsFetchingDTO,
                ScientificMissionCreateDto,
                ScientificMissions,
                int
            >(
                json,
                async item =>
                {
                    var spec = new ScientificMissionsSpecifications(item);
                    if (await missionRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<ScientificMissionCreateDto>(item);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
                missionRepo,
                _unitOfWork
            );
        }

        /*public Task<bool> SpecializationDataHandle(string? json)
		{
			throw new NotImplementedException();
		}*/

        public async Task<bool> ThesisDataHandle(string? json)
		{
            var thesesRepo = _unitOfWork.GetRepository<Thesis, int>();
            var supervisorRepo = _unitOfWork.GetRepository<Supervisor, int>();

            return await BulkHelper.HandleAsync<
                ThesesFetchingDTO,
                ThesesCreateDTO,
                Thesis,
                int
            >(
                json,
                async item =>
                {
                    var spec = new ThesesSpecifications(item);
                    if (await thesesRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<ThesesCreateDTO>(item);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);

                    var thesisEntity = _mapper.Map<Thesis>(dto);

                    if (item.Supervisors != null && item.Supervisors.Any())
                    {
                        dto.Supervisors = new List<SupervisorCreateDTO>();

                        foreach (var supervisorDto in item.Supervisors)
                        {
                            var supervisor = _mapper.Map<SupervisorCreateDTO>(supervisorDto);
                            supervisor.JobLevelId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);
                            dto.Supervisors.Add(supervisor);
                        }
                    }

                    return dto;
                },
                _mapper,
                thesesRepo,
                _unitOfWork
            );
        }

		public async Task<bool> ThesisSupervisingDataHandle(string? json)
		{
            var supervisingRepo = _unitOfWork.GetRepository<Supervising, int>();

            return await BulkHelper.HandleAsync<
                SupervisingsFetchingDTO,
                SupervisingCreateDTO,
                Supervising,
                int
            >(
                json,
                async item =>
                {
                    var spec = new SupervisingsSepcifications(item);
                    if (await supervisingRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<SupervisingCreateDTO>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);

                    return dto;
                },
                _mapper,
                supervisingRepo,
                _unitOfWork
            );
        }

		public async Task<bool> TrainingProgramDataHandle(string? json)
		{
            var trainingRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();

            return await BulkHelper.HandleAsync<
                TrainingProgramsFetchingDTO,
                TrainingProgramsCreateDto,
                TrainingPrograms,
                int
            >(
                json,
                async item =>
                {
                    var spec = new TrainingProgramsSpecifications(item);
                    if (await trainingRepo.ExistsAsync(spec))
                        return null;

                    var dto = _mapper.Map<TrainingProgramsCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
                trainingRepo,
                _unitOfWork
            );
        }

		//Researches Data
		public Task<bool> ResearchDataHandle(string? json)
		{
			throw new NotImplementedException();
		}

	}
}
