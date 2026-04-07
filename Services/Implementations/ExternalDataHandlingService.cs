using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Helpers.ExternalDataFetchingServiceHelpers;
using Services.Specifications.AcademicDataModule.HigherStudiesModule;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Services.Specifications.ResearchesModule;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Shared.Enums.Logging;

namespace Services.Implementations
{
	public class ExternalDataHandlingService(IUnitOfWork _unitOfWork, IMapper _mapper
		, IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper
		_getDataFromExternalServiceGetFacultyMembersAndLookupsHelper,
		ILogger<ExternalDataHandlingService> _logger) : IExternalDataHandlingService
	{
		public async Task<bool> AcademicDataHandle(string? json)
		{
			var academicDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var academicRepo = _unitOfWork.GetRepository<AcademicQualifications, int>();
			try
			{
				var flag = await BulkHelper.HandleAsync<
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
								return null!;

							var dto = _mapper.Map<AcademicQualificationCreateDto>(item);

							dto.DispatchId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Dispatch);
							dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);
							dto.QualificationId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Qualification);
							dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
							#region Log
							nationalNumber = item.NationalNumber;
							var academicRecordLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing academic qualification record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing academic qualification record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Specialization : {item.Specialization} - Qualification : {item.Qualification} - Dispatch : {item.Dispatch} - Grade : {item.Grade} - University/Faculty : {item.UniversityFaculty}]"
							};
							_logger.LogInformation("{@LogDetails}", academicRecordLog);
							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);
				#region Log
				if (!flag)
				{
					#region Log
					academicDataLog.Timestamp = DateTime.Now;
					academicDataLog.RenderedMessage = $"Failed to process academic qualification records";
					academicDataLog.AdditionalData = $"Failure during the processing of academic qualification records for faculty member with national number : {nationalNumber}";
					academicDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database";
					academicDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", academicDataLog);
					#endregion
					return false;
				}
				#region Log
				academicDataLog.Timestamp = DateTime.Now;
				academicDataLog.AdditionalData = $"Academic data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<AcademicQualificationFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}";
				academicDataLog.RenderedMessage = $"Completed processing academic qualification records for faculty member";
				academicDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", academicDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				academicDataLog.Timestamp = DateTime.Now;
				academicDataLog.RenderedMessage = $"Failed to process academic qualification records";
				academicDataLog.AdditionalData = $"Failure during the processing of academic qualification records for faculty member with national number : {nationalNumber}";
				academicDataLog.ExceptionMessage = ex.Message;
				academicDataLog.ExceptionDetail = ex.StackTrace;
				academicDataLog.Exception = ex.ToString();
				academicDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", academicDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ContactDataHandle(string? json)
		{
			var contactDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			var contactRepo = _unitOfWork.GetRepository<ContactData, int>();
			string? nationalNumber = null;
			try
			{
				var flag = await BulkHelper.HandleAsync<
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
								return null!;

							var dto = _mapper.Map<ContactDataCreateDTO>(item);
							dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
							#region Log
							nationalNumber = item.NationalNumber;
							var contactRecordLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing contact data record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing contact data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Phone Number : {item.MainPhoneNumber} - Email : {item.OfficialEmail}]."
							};
							_logger.LogInformation("{@LogDetails}", contactRecordLog);
							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);

				#region Log
				if (!flag)
				{
					#region Log
					contactDataLog.Level = "Error";
					contactDataLog.Timestamp = DateTime.Now;
					contactDataLog.RenderedMessage = $"Failed to process contact data records.";
					contactDataLog.AdditionalData = $"Failure during the processing of contact data records for faculty member with national number : {nationalNumber}.";
					contactDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					_logger.LogError("{@LogDetails}", contactDataLog);
					#endregion
					return false;
				}
				#region Log
				contactDataLog.Timestamp = DateTime.Now;
				contactDataLog.AdditionalData = $"Contact data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<ContactDataFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				contactDataLog.RenderedMessage = $"Completed processing contact data records for faculty member.";
				contactDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", contactDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				contactDataLog.Level = "Error";
				contactDataLog.Timestamp = DateTime.Now;
				contactDataLog.RenderedMessage = $"Failed to process contact data records";
				contactDataLog.AdditionalData = $"Failure during the processing of contact data records for faculty member with national number : {nationalNumber}";
				contactDataLog.ExceptionMessage = ex.Message;
				contactDataLog.ExceptionDetail = ex.StackTrace;
				contactDataLog.Exception = ex.ToString();
				_logger.LogError("{@LogDetails}", contactDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> EmploymentDataHandle(string? json)
		{
			var employmentDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var jobRanksRepo = _unitOfWork.GetRepository<JobRanks, int>();
			try
			{
				var flag = await BulkHelper.HandleAsync<
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
								return null!;

							var dto = _mapper.Map<JobRankCreateDto>(item);

							dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
							dto.JobRankId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Name);
							#region Log
							nationalNumber = item.NationalNumber;
							var employmentRecordLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing employment data record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing employment data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Job Rank : {item.Name} - Promotion Date : {item.PromotionDate}]."
							};
							_logger.LogInformation("{@LogDetails}", employmentRecordLog);
							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);
				#region Log
				if (!flag)
				{
					#region Log
					employmentDataLog.Timestamp = DateTime.Now;
					employmentDataLog.RenderedMessage = $"Failed to process employment data records";
					employmentDataLog.AdditionalData = $"Failure during the processing of employment data records for faculty member with national number : {nationalNumber}";
					employmentDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database";
					employmentDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", employmentDataLog);
					#endregion
					return false;
				}
				#region Log
				employmentDataLog.Timestamp = DateTime.Now;
				employmentDataLog.AdditionalData = $"Employment data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<JobRanksFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}";
				employmentDataLog.RenderedMessage = $"Completed processing employment data records for faculty member";
				employmentDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", employmentDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				employmentDataLog.Timestamp = DateTime.Now;
				employmentDataLog.RenderedMessage = $"Failed to process employment data records";
				employmentDataLog.AdditionalData = $"Failure during the processing of employment data records for faculty member with national number : {nationalNumber}";
				employmentDataLog.ExceptionMessage = ex.Message;
				employmentDataLog.ExceptionDetail = ex.StackTrace;
				employmentDataLog.Exception = ex.ToString();
				employmentDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", employmentDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ManagerialDataHandle(string? json)
		{
			var managerialDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var adminRepo = _unitOfWork.GetRepository<AdministrativePositions, int>();
			try
			{
				var flag = await BulkHelper.HandleAsync<
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
								return null!;

							var dto = _mapper.Map<AdministrativePositionCreateDto>(item);

							dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

							dto.Notes = item.Description;
							dto.Position = item.Name;
							#region Log
							nationalNumber = item.NationalNumber;
							var managerialDataLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing managerial data record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing managerial data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Adminstrative position : {item.Name} - Promotion(start) Date : {item.StartDate}]."
							};
							_logger.LogInformation("{@LogDetails}", managerialDataLog);

							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);
				#region Log
				if (!flag)
				{
					#region Log
					managerialDataLog.Timestamp = DateTime.Now;
					managerialDataLog.RenderedMessage = $"Failed to process managerial data records.";
					managerialDataLog.AdditionalData = $"Failure during the processing of managerial data records for faculty member with national number : {nationalNumber}.";
					managerialDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					managerialDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", managerialDataLog);
					#endregion
					return false;
				}
				#region Log
				managerialDataLog.Timestamp = DateTime.Now;
				managerialDataLog.AdditionalData = $"Managerial data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<JobRanksFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}";
				managerialDataLog.RenderedMessage = $"Completed processing managerial data records for faculty member";
				managerialDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", managerialDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				managerialDataLog.Timestamp = DateTime.Now;
				managerialDataLog.RenderedMessage = $"Failed to process managerial data records";
				managerialDataLog.AdditionalData = $"Failure during the processing of managerial data records for faculty member with national number : {nationalNumber}";
				managerialDataLog.ExceptionMessage = ex.Message;
				managerialDataLog.ExceptionDetail = ex.StackTrace;
				managerialDataLog.Exception = ex.ToString();
				managerialDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", managerialDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> PersonalDataHandle(string? json)
		{
			var personalDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();
			try
			{
				var flag = await BulkHelper.HandleAsync<
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
								return null!;

							var dto = _mapper.Map<PersonalDataCreateDTO>(item);

							dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
							dto.TitleId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Title);
							dto.GenderId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Gender);
							dto.MaritalStatusId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.SocialStatus);
							dto.AuthorityId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.FacultyName);
							dto.DepartmentId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Department);
							dto.FieldId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.FieldOfStudy);
							dto.UniversityId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.University);
							#region Log
							nationalNumber = item.NationalNumber;
							var personalDataLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing personal data record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing personal data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Name : {item.Name} - Gender : {item.Gender} - Social Status : {item.SocialStatus} - Faculty Name : {item.FacultyName} - Department : {item.Department} - University : {item.University}]."
							};
							_logger.LogInformation("{@LogDetails}", personalDataLog);
							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);
				#region Log
				if (!flag)
				{
					#region Log
					personalDataLog.Timestamp = DateTime.Now;
					personalDataLog.RenderedMessage = $"Failed to process personal data records.";
					personalDataLog.AdditionalData = $"Failure during the processing of personal data records for faculty member with national number : {nationalNumber}.";
					personalDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					personalDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", personalDataLog);
					#endregion
					return false;
				}
				#region Log
				personalDataLog.Timestamp = DateTime.Now;
				personalDataLog.AdditionalData = $"Personal data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<JobRanksFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				personalDataLog.RenderedMessage = $"Completed processing personal data records for faculty member.";
				personalDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", personalDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				personalDataLog.Timestamp = DateTime.Now;
				personalDataLog.RenderedMessage = $"Failed to process personal data records.";
				personalDataLog.AdditionalData = $"Failure during the processing of personal data records for faculty member with national number : {nationalNumber}.";
				personalDataLog.ExceptionMessage = ex.Message;
				personalDataLog.ExceptionDetail = ex.StackTrace;
				personalDataLog.Exception = ex.ToString();
				personalDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", personalDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ScientificDutyDataHandle(string? json)
		{
			var scientificDutyDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var missionRepo = _unitOfWork.GetRepository<ScientificMissions, int>();
			try
			{

				var flag = await BulkHelper.HandleAsync<
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
							return null!;

						var dto = _mapper.Map<ScientificMissionCreateDto>(item);
						dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
						#region Log
						nationalNumber = item.NationalNumber;
						var scientificDutyDataLog = new LogEntry
						{
							Timestamp = DateTime.Now,
							RenderedMessage = $"Processing scientific duty data record for faculty member with national number: {item.NationalNumber}.",
							Category = Category.ExternalDataHandling.ToString(),
							CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
							Level = "Information",
							AdditionalData = $"Processing scientific duty data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Scientific Mission : {item.Name} - Start Date : {item.StartDate} - End Date : {item.EndDate}]."
						};
						_logger.LogInformation("{@LogDetails}", scientificDutyDataLog);
						#endregion
						return dto;
					},
					_mapper,
					_unitOfWork
				);
				#region Log
				if (!flag)
				{
					#region Log
					scientificDutyDataLog.Timestamp = DateTime.Now;
					scientificDutyDataLog.RenderedMessage = $"Failed to process scientific duty data records.";
					scientificDutyDataLog.AdditionalData = $"Failure during the processing of scientific duty data records for faculty member with national number : {nationalNumber}.";
					scientificDutyDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					scientificDutyDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", scientificDutyDataLog);
					#endregion
					return false;
				}
				#region Log
				scientificDutyDataLog.Timestamp = DateTime.Now;
				scientificDutyDataLog.AdditionalData = $"Scientific duty data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<SceintificMissionsFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				scientificDutyDataLog.RenderedMessage = $"Completed processing scientific duty data records for faculty member.";
				scientificDutyDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", scientificDutyDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				scientificDutyDataLog.Timestamp = DateTime.Now;
				scientificDutyDataLog.RenderedMessage = $"Failed to process scientific duty data records.";
				scientificDutyDataLog.AdditionalData = $"Failure during the processing of scientific duty data records for faculty member with national number : {nationalNumber}.";
				scientificDutyDataLog.ExceptionMessage = ex.Message;
				scientificDutyDataLog.ExceptionDetail = ex.StackTrace;
				scientificDutyDataLog.Exception = ex.ToString();
				scientificDutyDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", scientificDutyDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ThesisDataHandle(string? json)
		{
			var thesisDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var thesesRepo = _unitOfWork.GetRepository<Thesis, int>();
			var supervisorRepo = _unitOfWork.GetRepository<ThesisComittee, int>();

			try
			{
				var flag = await BulkHelper.HandleAsync<
						ThesesFetchingDTO,
						ThesesCreateDTO,
						Thesis,
						int
					>(
						json,
						async item =>
						{
							var spec = new ExternalComingThesesSpecifications(item);
							if (await thesesRepo.ExistsAsync(spec))
								return null!;

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
									supervisor.JobLevelId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(supervisorDto.JobLevel);
									dto.Supervisors.Add(supervisor);
								}
							}
							#region Log
							nationalNumber = item.NationalNumber;
							var thesisDataLog = new LogEntry
							{
								Timestamp = DateTime.Now,
								RenderedMessage = $"Processing thesis data record for faculty member with national number: {item.NationalNumber}.",
								Category = Category.ExternalDataHandling.ToString(),
								CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
								Level = "Information",
								AdditionalData = $"Processing thesis data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Thesis Title : {item.Title} - Grade : {item.Grade} - Type : {item.Type} - Number of Supervisors : {item.Supervisors?.Count}]."
							};
							_logger.LogInformation("{@LogDetails}", thesisDataLog);
							#endregion
							return dto;
						},
						_mapper,
						_unitOfWork
					);
				#region Log
				if (!flag)
				{
					#region Log
					thesisDataLog.Timestamp = DateTime.Now;
					thesisDataLog.RenderedMessage = $"Failed to process thesis data records.";
					thesisDataLog.AdditionalData = $"Failure during the processing of thesis data records for faculty member with national number : {nationalNumber}.";
					thesisDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					thesisDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", thesisDataLog);
					#endregion
					return false;
				}
				#region Log
				thesisDataLog.Timestamp = DateTime.Now;
				thesisDataLog.AdditionalData = $"Thesis data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<ThesesFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				thesisDataLog.RenderedMessage = $"Completed processing thesis data records for faculty member.";
				thesisDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", thesisDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				thesisDataLog.Timestamp = DateTime.Now;
				thesisDataLog.RenderedMessage = $"Failed to process thesis data records.";
				thesisDataLog.AdditionalData = $"Failure during the processing of thesis data records for faculty member with national number : {nationalNumber}.";
				thesisDataLog.ExceptionMessage = ex.Message;
				thesisDataLog.ExceptionDetail = ex.StackTrace;
				thesisDataLog.Exception = ex.ToString();
				thesisDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", thesisDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ThesisSupervisingDataHandle(string? json)
		{
			var thesisSupervisingDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var supervisingRepo = _unitOfWork.GetRepository<Supervising, int>();

			try
			{
				var flag = await BulkHelper.HandleAsync<
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
							return null!;

						var dto = _mapper.Map<SupervisingCreateDTO>(item);

						dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
						dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);
						#region Log
						nationalNumber = item.NationalNumber;
						var thesisSupervisingDataLog = new LogEntry
						{
							Timestamp = DateTime.Now,
							RenderedMessage = $"Processing thesis supervising data record for faculty member with national number: {item.NationalNumber}.",
							Category = Category.ExternalDataHandling.ToString(),
							CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
							Level = "Information",
							AdditionalData = $"Processing thesis supervising data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Thesis Title : {item.ThesisTitle} - Thesis Type : {item.ThesisType} - Role : {item.Role} - Student Name : {item.StudentName}]."
						};
						_logger.LogInformation("{@LogDetails}", thesisSupervisingDataLog);
						#endregion
						return dto;
					},
					_mapper,
					_unitOfWork
				);
				#region Log
				if (!flag)
				{
					#region Log
					thesisSupervisingDataLog.Timestamp = DateTime.Now;
					thesisSupervisingDataLog.RenderedMessage = $"Failed to process thesis supervising data records.";
					thesisSupervisingDataLog.AdditionalData = $"Failure during the processing of thesis supervising data records for faculty member with national number : {nationalNumber}.";
					thesisSupervisingDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					thesisSupervisingDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", thesisSupervisingDataLog);
					#endregion
					return false;
				}
				#region Log
				thesisSupervisingDataLog.Timestamp = DateTime.Now;
				thesisSupervisingDataLog.AdditionalData = $"Thesis supervising data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<SupervisingsFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				thesisSupervisingDataLog.RenderedMessage = $"Completed processing thesis supervising data records for faculty member.";
				thesisSupervisingDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", thesisSupervisingDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				thesisSupervisingDataLog.Timestamp = DateTime.Now;
				thesisSupervisingDataLog.RenderedMessage = $"Failed to process thesis supervising data records.";
				thesisSupervisingDataLog.AdditionalData = $"Failure during the processing of thesis supervising data records for faculty member with national number : {nationalNumber}.";
				thesisSupervisingDataLog.ExceptionMessage = ex.Message;
				thesisSupervisingDataLog.ExceptionDetail = ex.StackTrace;
				thesisSupervisingDataLog.Exception = ex.ToString();
				thesisSupervisingDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", thesisSupervisingDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> TrainingProgramDataHandle(string? json)
		{
			var trainingProgramDataLog = new LogEntry
			{
				Category = Category.ExternalDataHandling.ToString(),
				CategoryAction = CategoryAction.ExternalDataProcessing.ToString()
			};
			string? nationalNumber = null;
			var trainingRepo = _unitOfWork.GetRepository<TrainingPrograms, int>();

			try
			{
				var flag = await BulkHelper.HandleAsync<
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
									return null!;

								var dto = _mapper.Map<TrainingProgramsCreateDto>(item);

								dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
								#region Log
								nationalNumber = item.NationalNumber;
								var trainingProgramDataLog = new LogEntry
								{
									Timestamp = DateTime.Now,
									RenderedMessage = $"Processing training program data record for faculty member with national number: {item.NationalNumber}.",
									Category = Category.ExternalDataHandling.ToString(),
									CategoryAction = CategoryAction.ExternalDataProcessing.ToString(),
									Level = "Information",
									AdditionalData = $"Processing training program data record from external data source for faculty member with national number : {item.NationalNumber} -> Data [Program Name : {item.Name} - Program Type : {item.ProgramType} - Participaction Type : {item.ParticipationType} - Organizer : {item.OrganizerName}]."
								};
								_logger.LogInformation("{@LogDetails}", trainingProgramDataLog);
								#endregion
								return dto;
							},
							_mapper,
							_unitOfWork
						);
				#region Log
				if (!flag)
				{
					#region Log
					trainingProgramDataLog.Timestamp = DateTime.Now;
					trainingProgramDataLog.RenderedMessage = $"Failed to process training program data records.";
					trainingProgramDataLog.AdditionalData = $"Failure during the processing of training program data records for faculty member with national number : {nationalNumber}.";
					trainingProgramDataLog.ExceptionMessage = "Failure might be caused by: failed to save updates to database since 0 changes where applied to the database.";
					trainingProgramDataLog.Level = "Error";
					_logger.LogError("{@LogDetails}", trainingProgramDataLog);
					#endregion
					return false;
				}
				#region Log
				trainingProgramDataLog.Timestamp = DateTime.Now;
				trainingProgramDataLog.AdditionalData = $"Training program data handling process completed successfully. Processed {JsonHelper.DeserializeListOrThrow<TrainingProgramsFetchingDTO>(json!).Count} records for faculty member with national number : {nationalNumber}.";
				trainingProgramDataLog.RenderedMessage = $"Completed processing training program data records for faculty member.";
				trainingProgramDataLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", trainingProgramDataLog);
				#endregion
				#endregion
				return flag;
			}
			catch (Exception ex)
			{
				#region Log
				trainingProgramDataLog.Timestamp = DateTime.Now;
				trainingProgramDataLog.RenderedMessage = $"Failed to process training program data records.";
				trainingProgramDataLog.AdditionalData = $"Failure during the processing of training program data records for faculty member with national number : {nationalNumber}.";
				trainingProgramDataLog.ExceptionMessage = ex.Message;
				trainingProgramDataLog.ExceptionDetail = ex.StackTrace;
				trainingProgramDataLog.Exception = ex.ToString();
				trainingProgramDataLog.Level = "Error";
				_logger.LogError("{@LogDetails}", trainingProgramDataLog);
				#endregion
				throw;
			}
		}

		public async Task<bool> ResearchDataHandle(string? json)
		{
			var researchersRepo = _unitOfWork.GetRepository<ResearcherProfile, int>();
			var interestsRepo = _unitOfWork.GetRepository<ScientificInterest, int>();
			var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
			var researchRepo = _unitOfWork.GetRepository<Research, int>();
			var coAuthorsRepo = _unitOfWork.GetRepository<CoAuthor, int>();
			var personalDataRepo = _unitOfWork.GetRepository<PersonalData, int>();

			var dto = JsonSerializer.Deserialize<ResearcherDataFetchingDTO>(json!)
					  ?? throw new Exception("Invalid JSON");

			var facultyMember = await facultyMemberRepo.GetAsync(
				new FacultyMemberWithNationalNumberSpecifications(dto.NationalNumber)
			);
			if (facultyMember is null) throw new Exception("Faculty member not found");

			facultyMember.ResearchContributions = facultyMember.ResearchContributions.EnsureList();

			var researcher = await researchersRepo.GetAsync(
				new ResearcherProfileSpceification(dto.ScholarProfileLink)
			);

			var isNewResearcher = researcher is null;
			if (isNewResearcher)
			{
				researcher = _mapper.Map<ResearcherProfile>(dto);
			}
			else
			{
				researcher!.AcademicName = dto.AcademicName?.Trim() ?? researcher.AcademicName;
				researcher.OrganisationalDomain = dto.OrganisationalDomain ?? researcher.OrganisationalDomain;
				researcher.JobTitle = dto.JobTitle ?? researcher.JobTitle;
				researcher.ScholarProfileLink = dto.ScholarProfileLink ?? researcher.ScholarProfileLink;
				researcher.ScholarProfileImageURL = dto.ScholarProfileImageURL ?? researcher.ScholarProfileImageURL;
			}

			researcher!.ResearcherInterests = researcher.ResearcherInterests.EnsureList();
			researcher.ResearcherCites = researcher.ResearcherCites.EnsureList();
			researcher!.CoAuthors = researcher.CoAuthors.EnsureList();

			var incomingInterestNames = (dto.Interests ?? new List<ExternalResearcherInterestsFetchingDTO>())
				.Select(x => x.Name?.Trim())
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

			var interestEntities = new List<ScientificInterest>();

			foreach (var name in incomingInterestNames)
			{
				var interest = await UpsertHelpers.GetOrCreateAsync(
					getter: () => interestsRepo.GetAsync(new ResearcherInterestSpecification(name!)),
					factory: () =>
					{
						var created = _mapper.Map<ScientificInterest>(
							new ExternalResearcherInterestsFetchingDTO { Name = name! }
						);
						created.Researchers = created.Researchers.EnsureList();
						return created;
					});

				interest.Researchers = interest.Researchers.EnsureList();
				interestEntities.Add(interest);
			}

			foreach (var interest in interestEntities)
			{
				var alreadyLinked = researcher.ResearcherInterests.Any(ri =>
					ri.Interest != null &&
					string.Equals(ri.Interest.Name, interest.Name, StringComparison.OrdinalIgnoreCase)
				);

				if (!alreadyLinked)
				{
					var link = new ResearcherInterest { Researcher = researcher, Interest = interest };
					researcher.ResearcherInterests.Add(link);
					interest.Researchers!.Add(link);
				}
			}

			var incomingResearcherCites = dto.ResearcherCites ?? new List<ExternalResearcherCitesFetchingDTO>();

			researcher.ResearcherCites.UpsertMany(
				dtos: incomingResearcherCites,
				match: (d, c) => Convert.ToInt32(c.Year) == d.Year,
				createAction: d =>
				{
					var citeEntity = _mapper.Map<ResearcherCite>(d);
					citeEntity.Researcher = researcher;
					return citeEntity;
				},
				updateAction: (d, existing) => _mapper.Map(d, existing)
			);

			var incomingResearchDtos = dto.Researches ?? new List<ExternalResearchesFetchingDTO>();

			foreach (var rDto in incomingResearchDtos)
			{
				var existingResearch = await researchRepo.GetAsync(
					new RecommendedResearchesSpecifications(rDto.Title!)
				);

				var researchEntity = existingResearch;
				var isNewResearch = researchEntity is null;

				if (isNewResearch)
				{
					researchEntity = _mapper.Map<Research>(rDto);
					researchEntity.Contributions = researchEntity.Contributions.EnsureList();
					researchEntity.Cites = researchEntity.Cites.EnsureList();
					researchEntity.DOI = null;

					researchEntity.PublisherType = Domain.Enums.PublisherType.Unspecified;
					researchEntity.PublicationType = Domain.Enums.PublicationType.International;
					researchEntity.Source = Domain.Enums.ResearchSource.External;
					researchEntity.ResearchDerivedFrom = Domain.Enums.ResearchDerivedFrom.Other;
					researchEntity.PubYear = researchEntity.PubYear == 0 ? null : researchEntity.PubYear;

					await researchRepo.AddAsync(researchEntity);
				}
				else
				{
					researchEntity!.Title = rDto.Title ?? researchEntity.Title;
					researchEntity.DOI = rDto.DOI ?? researchEntity.DOI;

					researchEntity.PublisherType = Domain.Enums.PublisherType.Unspecified;
					researchEntity.PublicationType = Domain.Enums.PublicationType.International;
					researchEntity.Source = Domain.Enums.ResearchSource.External;
					researchEntity.ResearchDerivedFrom = Domain.Enums.ResearchDerivedFrom.Other;

					researchEntity.Contributions = researchEntity.Contributions.EnsureList();
					researchEntity.Cites = researchEntity.Cites.EnsureList();
				}

				var incomingContribs = rDto.Contributions ?? new List<ExternalResearchContributionFetchingDTO>();

				foreach (var cDto in incomingContribs)
				{
					var exists = researchEntity!.Contributions
						.FirstOrDefault(c =>
							string.Equals(c.MemberAcademicName, cDto.MemberAcademicName, StringComparison.OrdinalIgnoreCase)
						);

					var probableFacultyMemberEntity = await personalDataRepo.GetAsync(new PersonalDataWithNameSpecification(cDto.MemberAcademicName));
					if (probableFacultyMemberEntity is not null && probableFacultyMemberEntity.FacultyMemberId != facultyMember.Id)
					{
						researchEntity.Contributions.Add(new ResearchContribution
						{
							Contributor = probableFacultyMemberEntity.FacultyMember,
							MemberAcademicName = cDto.MemberAcademicName,
							ContributorType = Domain.Enums.ContributorType.FromUniverstity
						});
					}

					if (exists is not null) continue;

					var contEntity = _mapper.Map<ResearchContribution>(cDto);
					contEntity.ContributorType = Domain.Enums.ContributorType.Unspecified;
					contEntity.Research = researchEntity;

					if (string.Equals(
						UpsertHelpers.NormalizeName(cDto.MemberAcademicName),
						UpsertHelpers.NormalizeName(researcher.AcademicName),
						StringComparison.OrdinalIgnoreCase
					))
					{
						contEntity.ContributorType = Domain.Enums.ContributorType.FromUniverstity;
						contEntity.IsTheMajorResearcher = true;

						contEntity.Contributor = facultyMember;
						facultyMember.ResearchContributions.Add(contEntity);
					}

					researchEntity.Contributions.Add(contEntity);
				}

				if (researchEntity!.Contributions.All(c => c.Contributor != facultyMember))
				{
					researchEntity.Contributions.Add(new ResearchContribution
					{
						Contributor = facultyMember,
						MemberAcademicName = facultyMember.Id.ToString(),
						IsTheMajorResearcher = true,
						ContributorType = Domain.Enums.ContributorType.FromUniverstity
					});
				}




				var incomingCites = rDto.Cites ?? new List<ExternalResearchCitesFetchingDTO>();

				researchEntity.Cites.UpsertMany(
					dtos: incomingCites,
					match: (d, c) => c.Year == d.Year && c.NumberOfCites == d.NumberOfCites,
					createAction: d =>
					{
						var citeEntity = _mapper.Map<ResearchCite>(d);
						citeEntity.Research = researchEntity;
						return citeEntity;
					},
					updateAction: (d, existing) =>
					{
						existing.Year = d.Year;
						existing.NumberOfCites = d.NumberOfCites;
					}
				);
			}

			var incomingCoAuthorsProfiles = (dto.CoAuthors ?? new List<ResearcherCoAuthorFetchingDTO>());
			var coAuthorsEntities = new List<CoAuthor>();

			foreach (var profile in incomingCoAuthorsProfiles)
			{
				profile.ScholarProfileLink = $"https://scholar.google.com.eg/citations?hl=ar&user={profile.ScholarProfileLink}";
				profile.ScholarProfileImageURL = $"https://scholar.googleusercontent.com/citations?view_op=view_photo&user={profile.ScholarProfileLink}&citpid=5";

				var coAuthor = await UpsertHelpers.GetOrCreateAsync(
					getter: async () => await coAuthorsRepo.GetAsync(new CoAuthorSpecification(profile.ScholarProfileLink)),
					factory: () =>
					{
						var created = _mapper.Map<CoAuthor>(profile);
						created.Researchers = created.Researchers.EnsureList();
						return created;
					});

				coAuthor.Researchers = coAuthor.Researchers.EnsureList();
				coAuthorsEntities.Add(coAuthor);
			}

			foreach (var coAuthor in coAuthorsEntities)
			{
				var alreadyLinked = researcher.CoAuthors.Any(ri =>
					ri.CoAuthor != null &&
					string.Equals(ri.CoAuthor.ScholarProfileLink, coAuthor.ScholarProfileLink)
				);

				if (!alreadyLinked)
				{
					var link = new ResearcherCoAuthor { Researcher = researcher, CoAuthor = coAuthor };
					researcher.CoAuthors.Add(link);
					coAuthor.Researchers!.Add(link);
				}
			}

			researcher.FacultyMember = facultyMember;

			if (isNewResearcher) await researchersRepo.AddAsync(researcher);
			else researchersRepo.Update(researcher);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}


	}
}
