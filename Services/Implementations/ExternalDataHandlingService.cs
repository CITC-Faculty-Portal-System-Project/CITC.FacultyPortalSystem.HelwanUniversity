using Domain.Entities.MissionsModule;
using Domain.Entities.ScientificProgressionModule;
using Shared.Enums;
using Services.Specifications.LookUpItems;
using Services.Specifications.MissionsModule;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.MissionsModule;
using Shared.Dtos.ScientificProgressionModule;
using Shared.Enums.MissionsModule;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ExternalDataHandlingService(IUnitOfWork _unitOfWork , IMapper _mapper) : IExternalDataHandlingService
    {
        public async Task<bool> AcademicDataHandle(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<AcademicQualificationFetchingDTO>>(json, options);
            var dataAddRequest = new List<AcademicQualificationCreateDto>();

            if (listResult != null && listResult.Any())
            {
                var academicRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
                var dispatchRepo = _unitOfWork.GetRepository<Lookup, Guid>();
                var gradeRepo = _unitOfWork.GetRepository<Lookup, Guid>();
                var qualificationRepo = _unitOfWork.GetRepository<Lookup, Guid>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();




                foreach (var item in listResult)
                {
                    var qualificationSpecification = new AcademicQualificationsSpecifications(item);
                    var data = await academicRepo.GetAllAsync(qualificationSpecification);
                    if (data.Any())
                        continue;

                    var dispatchSpecification = new LookUpItemNameSpecification(item.Dispatch);
                    var dispatch = await dispatchRepo.GetAllAsync(dispatchSpecification);

                    var gradeSpecification = new LookUpItemNameSpecification(item.Grade);
                    var grade = await gradeRepo.GetAllAsync(gradeSpecification);

                    var qualificationTypeSpecification = new LookUpItemNameSpecification(item.Qualification);
                    var qualification = await qualificationRepo.GetAllAsync(qualificationTypeSpecification);

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAllAsync(facultyMemberSpecification);

                    var currentQualification = new AcademicQualificationCreateDto
                    {
                        Specialization = item.Specialization,
                        CountryOrCity = item.CountryCity,
                        DateOfObtainingTheQualification = DateOnly.Parse(item.DateOfAcquisition),
                        DispatchId = dispatch.FirstOrDefault()?.Id ?? Guid.Empty,
                        FacultyMemberId = member.FirstOrDefault()?.Id ?? Guid.Empty,
                        QualificationId = qualification.FirstOrDefault()?.Id ?? Guid.Empty,
                        GradeId = grade.FirstOrDefault()?.Id ?? Guid.Empty,
                        UniversityOrFaculty = item.UniversityFaculty,
                        
                    };

                    dataAddRequest.Add(currentQualification);
                }

               var entites = _mapper.Map<IEnumerable<AcademicQualifications>>(dataAddRequest);
               await academicRepo.AddRangeAsync(entites);
               return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;

         }

		public async Task<bool> ContactDataHandle(string? json)
		{
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<ContactDataFetchingDTO>>(json, options);
            var dataAddRequest = new List<ContactDataCreateDTO>();

            if (listResult != null && listResult.Any())
            {
                var contactDataRepo = _unitOfWork.GetRepository<ContactData, int>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();

                foreach (var item in listResult)
                {
                    var contactDataSpecification = new ContactDataWithExternalServiceSpecification(item);
                    var data = await contactDataRepo.GetAllAsync(contactDataSpecification);

                    if (data.Any())
                        continue;

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAsync(facultyMemberSpecification);

                    var currentContactData = new ContactDataCreateDTO
                    {
                        Address = item.Address,
                        AlternativeEmail = item.PersonalEmail,
                        FaxNumber = item.FaxNumber,
                        HomePhoneNumber = item.HomePhoneNumber,
                        MainPhoneNumber = item.MainPhoneNumber,
                        OfficialEmail = item.OfficialEmail,
                        PersonalEmail = item.PersonalEmail,
                        WorkPhoneNumber = item.WorkPhoneNumber,
                        FacultyMemberId = member.Id
                    };

                    dataAddRequest.Add(currentContactData);
                }

                var entites = _mapper.Map<IEnumerable<ContactData>>(dataAddRequest);
                await contactDataRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> EmploymentDataHandle(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<JobRanksFetchingDTO>>(json, options);
            var dataAddRequest = new List<JobRankCreateDto>();


            if (listResult != null && listResult.Any())
            {
                var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
                var lookUpRepo = _unitOfWork.GetRepository<Lookup, Guid>();

                foreach (var item in listResult)
                {
                    var jobRankSpecification = new JobRanksSpecifications(item);
                    var data = await jobRanksRepo.GetAllAsync(jobRankSpecification);
                    if (data.Any())
                        continue;

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var facultyMember = await facultyMemberRepo.GetAsync(facultyMemberSpecification);


                    var currentJobSpecification = new LookUpItemNameSpecification(item.Name);
                    var currentJob = await lookUpRepo.GetAsync(currentJobSpecification);

                    var currentJobRank = new JobRankCreateDto
                    {
                        DateOfJobRank = DateOnly.Parse(item.PromotionDate),
                        FacultyMemberId = facultyMember.Id,
                        JobRankId = currentJob.Id,
                        Notes = "لا يوجد"
                    };

                    dataAddRequest.Add(currentJobRank);
                }

                var entites = _mapper.Map<IEnumerable<JobRanks>>(dataAddRequest);
                await jobRanksRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> ManagerialDataHandle(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<AdminstrativePostionsFetchingDTO>>(json, options);
            var dataAddRequest = new List<AdministrativePositionCreateDto>();

            if (listResult != null && listResult.Any())
            {
                var adminstrativePositionRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();

                foreach (var item in listResult)
                {
                    var adminstrativePositionSpecification = new AdministrativePositionsSpecifications(item);
                    var data = await adminstrativePositionRepo.GetAllAsync(adminstrativePositionSpecification);
                    
                    if (data.Any())
                        continue;

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAsync(facultyMemberSpecification);

                    var currentAdminstrtivePosition = new AdministrativePositionCreateDto
                    {
                       StartDate = DateOnly.Parse(item.StartDate),
                       EndDate = (string.IsNullOrEmpty(item.EndDate)
                                      ?  null
                                      : DateOnly.Parse(item.EndDate)),
                        FacultyMemberId = member.Id,
                       Notes = item.Description,
                       Position = item.Name,
                    };

                    dataAddRequest.Add(currentAdminstrtivePosition);
                }

                var entites = _mapper.Map<IEnumerable<AdministrativePositions>>(dataAddRequest);
                await adminstrativePositionRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;

        }

		public async Task<bool> PersonalDataHandle(string? json)
		{
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<PersonalDataFetchingDTO>>(json, options);
            var dataAddRequest = new List<PersonalDataCreateDTO>();

            if (listResult != null && listResult.Any())
            {
                var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();
                var lookUpRepo = _unitOfWork.GetRepository<Lookup, Guid>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();

                
                foreach (var item in listResult)
                {
                    var personalDataSpecification = new PersonalDataWithIncludesSpecifications(item);
                    var data = await personalDataRepo.GetAllAsync(personalDataSpecification);
                    if (data.Any())
                        continue;

                    var titleSpecification = new LookUpItemNameSpecification(item.Title);
                    var title = await lookUpRepo.GetAsync(titleSpecification);

                    var genderSpecification = new LookUpItemNameSpecification(item.Gender);
                    var gender = await lookUpRepo.GetAsync(titleSpecification);

                    var materialStatusSpecification = new LookUpItemNameSpecification(item.SocialStatus);
                    var materialStatus = await lookUpRepo.GetAsync(titleSpecification);

                    var facultySpecification = new LookUpItemNameSpecification(item.FacultyName);
                    var faculty = await lookUpRepo.GetAsync(facultySpecification);

                    var departmentSpecification = new LookUpItemNameSpecification(item.Department);
                    var department = await lookUpRepo.GetAsync(departmentSpecification);

                    var fieldSpecification = new LookUpItemNameSpecification(item.FieldOfStudy);
                    var field = await lookUpRepo.GetAsync(fieldSpecification);

                    var universitySpecification = new LookUpItemNameSpecification(item.University);
                    var university = await lookUpRepo.GetAsync(universitySpecification);

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAsync(facultyMemberSpecification);

                    var currentData = new PersonalDataCreateDTO
                    {
                        DepartmentId = department.Id,
                        BirthDate = item.BirthDate,
                        AccurateSpecialization = item.AccurateSpecialization,
                        GeneralSpecialization = item.GeneralSpecialization,
                        MaritalStatusId = materialStatus.Id,
                        AuthorityId = faculty.Id,
                        UniversityId = university.Id,
                        BirthPlace = item.BirthPlace,
                        CompositionTopics = item.CompositionTopics,
                        FieldId = field.Id,
                        GenderId = gender.Id,
                        Name = item.Name,
                        NameInComposition = item.NameInCompositions,
                        TitleId = title.Id,
                        FacultyMemberId = member.Id
                    };

                    dataAddRequest.Add(currentData);
                }

                var entites = _mapper.Map<IEnumerable<PersonalData>>(dataAddRequest);
                await personalDataRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;


        }

        public async Task<bool> ScientificDutyDataHandle(string? json)
		{
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<SceintificMissionsFetchingDTO>>(json, options);
            var dataAddRequest = new List<ScientificMissionCreateDto>();

            if (listResult != null && listResult.Any())
            {
                var missionRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();


                foreach (var item in listResult)
                {
                    var missionSpecification = new ScientificMissionsSpecifications(item);
                    var data = await missionRepo.GetAllAsync(missionSpecification);
                    
                    if (data.Any())
                        continue;

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAsync(facultyMemberSpecification);

                    var currentData = new ScientificMissionCreateDto
                    {
                        StartDate = item.StartDate,
                        CountryOrCity = item.CountryCity,
                        Description = item.Description,
                        EndDate = item.EndDate,
                        FacultyMemberId = member.Id,
                        name = item.Name,
                        UniversityOrFaculty = item.UniversityFaculty
                    };

                    dataAddRequest.Add(currentData);
                }

                var entites = _mapper.Map<IEnumerable<ScientificMissions>>(dataAddRequest);
                await missionRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;
        }

        /*public Task<bool> SpecializationDataHandle(string? json)
		{
			throw new NotImplementedException();
		}*/

        public Task<bool> ThesisDataHandle(string? json)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ThesisSupervisingDataHandle(string? json)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> TrainingProgramDataHandle(string? json)
		{
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listResult = JsonSerializer.Deserialize<List<TrainingProgramsFetchingDTO>>(json, options);
            var dataAddRequest = new List<TrainingProgramsCreateDto>();

            if (listResult != null && listResult.Any())
            {
                var trainingRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();
                var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();


                foreach (var item in listResult)
                {
                    var trainingSpecification = new TrainingProgramsSpecifications(item);
                    var data = await trainingRepo.GetAllAsync(trainingSpecification);

                    if (data.Any())
                        continue;

                    var facultyMemberSpecification = new FacultyMemberWithNationalNumberSpecifications(item.NationalNumber);
                    var member = await facultyMemberRepo.GetAsync(facultyMemberSpecification);

                    var currentData = new TrainingProgramsCreateDto
                    {
                        StartDate = item.StartDate,
                        Description = item.Description,
                        EndDate = item.EndDate,
                        FacultyMemberId = member.Id,
                        OrganizingAuthority = item.OrganizerName,
                        ParticipationType =
                            (item.ParticipationType?.Trim() == "محاضر")
                            ? TrainingProgramParticipationType.lecturer
                            : TrainingProgramParticipationType.listener,

                        TrainingProgramName = item.Name,
                        Venue = item.ProgramPlace,
                        Type = (item.ProgramType?.Trim() == "في التخصص")
                            ? TrainingProgramType.InTheSpecialty
                            : TrainingProgramType.OutTheSpecialty,

                    };

                    dataAddRequest.Add(currentData);
                }

                var entites = _mapper.Map<IEnumerable<TrainingPrograms>>(dataAddRequest);
                await trainingRepo.AddRangeAsync(entites);
                return await _unitOfWork.SaveChangesAsync() > 0;
            }

            return false;
        }
	}
}
