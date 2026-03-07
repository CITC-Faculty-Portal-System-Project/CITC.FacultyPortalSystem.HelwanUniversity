using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Helpers.ExternalDataFetchingServiceHelpers;
using Services.Specifications.HigherStudiesModule;
using Services.Specifications.AcademicDataModule.MissionsModule;
using Services.Specifications.ResearchesModule;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using System.Text.Json;
using Services.Specifications.AcademicDataModule.HigherStudiesModule;

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
                        return null!;

                    var dto = _mapper.Map<AcademicQualificationCreateDto>(item);

                    dto.DispatchId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Dispatch);
                    dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);
                    dto.QualificationId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Qualification);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<ContactDataCreateDTO>(item);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<JobRankCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.JobRankId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Name);
                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<AdministrativePositionCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    dto.Notes = item.Description;
                    dto.Position = item.Name;

                    return dto;
                },
                _mapper,
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

                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<ScientificMissionCreateDto>(item);
                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
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
            var supervisorRepo = _unitOfWork.GetRepository<ThesisComittee, int>();

            return await BulkHelper.HandleAsync<
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

                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<SupervisingCreateDTO>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);
                    dto.GradeId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetLookupIdByNameAsync(item.Grade);

                    return dto;
                },
                _mapper,
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
                        return null!;

                    var dto = _mapper.Map<TrainingProgramsCreateDto>(item);

                    dto.FacultyMemberId = await _getDataFromExternalServiceGetFacultyMembersAndLookupsHelper.GetFacultyIdByNationalNumberAsync(item.NationalNumber);

                    return dto;
                },
                _mapper,
                _unitOfWork
            );
        }

		public async Task<bool> ResearchDataHandle(string? json)
		{
            var researchersRepo = _unitOfWork.GetRepository<ResearcherProfile, int>();
            var interestsRepo = _unitOfWork.GetRepository<ScientificInterest, int>();
            var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
            var researchRepo = _unitOfWork.GetRepository<Research, int>();

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

                    researchEntity.PublisherType = Domain.Enums.PublisherType.Unspecified;
                    researchEntity.PublicationType = Domain.Enums.PublicationType.Unspecified;
                    researchEntity.Source = Domain.Enums.ResearchSource.External;
                    researchEntity.ResearchDerivedFrom = Domain.Enums.ResearchDerivedFrom.Other;

                    await researchRepo.AddAsync(researchEntity);
                }
                else
                {
                    researchEntity!.Title = rDto.Title ?? researchEntity.Title;
                    researchEntity.DOI = rDto.DOI ?? researchEntity.DOI;

                    researchEntity.PublisherType = Domain.Enums.PublisherType.Unspecified;
                    researchEntity.PublicationType = Domain.Enums.PublicationType.Unspecified;
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

            researcher.FacultyMember = facultyMember;

            if (isNewResearcher) await researchersRepo.AddAsync(researcher);
            else researchersRepo.Update(researcher);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
