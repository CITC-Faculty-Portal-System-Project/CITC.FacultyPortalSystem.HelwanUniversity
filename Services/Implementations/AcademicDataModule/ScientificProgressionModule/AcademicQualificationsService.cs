using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.ScientificProgressionModule;
using Shared.Dtos.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class AcademicQualificationsService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<AcademicQualifications, int>(unitOfWork, authenticationService, mapper), IAcademicQualificationsService
    {
        protected override string EntityName => "Academic Qualifications";
        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(AcademicQualificationsSpecificationParamters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualifications = await Repo.GetAllAsync(new AcademicQualificationsSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var academicQualificationsResult = Mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(academicQualifications);

            var currentPageCount = academicQualificationsResult.Count();

            var totalCount = await Repo.CountAsync(new AcademicQualificationsCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<AcademicQualificationResponseDto>(parameters.PageIndex, currentPageCount, totalCount, academicQualificationsResult);

        }
        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(new AcademicQualificationsSpecifications(id)) ?? throw new NotFoundException("Academic Qualifications are Not Found");

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(AcademicQualificationCreateDto academicQualificationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = Mapper.Map<AcademicQualifications>(academicQualificationCreateDto);
            academicQualification.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(academicQualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(int academicQualificationId, AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(academicQualificationsUpdateDto, academicQualification);

            Repo.Update(academicQualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            academicQualification.IsDeleted = true;

            Repo.Update(academicQualification);
            await SaveChangesAsync();
        }
    }
}