using Domain.Entities.ScientificProgressionModule;
using Services.Specifications.LookUpItems;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.ScientificProgressionModule;
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

		public Task<bool> PersonalDataHandle(string? json)
		{
            throw new NotImplementedException();

        }

        public Task<bool> ScientificDutyDataHandle(string? json)
		{
			throw new NotImplementedException();
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

		public Task<bool> TrainingProgramDataHandle(string? json)
		{
			throw new NotImplementedException();
		}
	}
}
