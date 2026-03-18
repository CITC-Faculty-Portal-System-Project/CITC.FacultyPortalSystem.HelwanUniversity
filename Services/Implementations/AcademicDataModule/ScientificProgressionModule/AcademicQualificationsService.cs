using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class AcademicQualificationsService(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<AcademicQualifications, int>(unitOfWork, authenticationService, mapper),
       IAcademicQualificationsService
    {
        protected override string EntityName => "Academic Qualifications";

        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
            AcademicQualificationsSpecificationParamters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var qualifications = await Repo.GetAllAsync(
                new AcademicQualificationsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(qualifications);

            var totalCount = await Repo.CountAsync(
                new AcademicQualificationsCountSpecifications(parameters, email));

            return new PaginatedResult<AcademicQualificationResponseDto>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                qualification.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto dto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var qualification = Mapper.Map<AcademicQualifications>(dto);
            qualification.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(qualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int id,
            AcademicQualificationsUpdateDto dto,
            string? facultyMemberEmail = null)
        {
            var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                qualification.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(dto, qualification);

            Repo.Update(qualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(qualification);
        }

        public async Task DeleteAcademicQualificationAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var qualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                qualification.FacultyMemberId,
                facultyMemberEmail);

            qualification.IsDeleted = true;

            Repo.Update(qualification);
            await SaveChangesAsync();
        }
    }
}