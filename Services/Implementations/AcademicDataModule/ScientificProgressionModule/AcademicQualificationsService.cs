using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.AcademicDataModule.ScientificProgressionModule
{
    public class AcademicQualificationsService(
       IUnitOfWork unitOfWork,
       IMapper mapper,
       IAuthenticationService authenticationService,
       IAcademicQualificationsHelper academicQualificationsHelper)
       : BaseService<AcademicQualifications, int>(unitOfWork, authenticationService, mapper),
         IAcademicQualificationsService
    {
        private readonly IAcademicQualificationsHelper _helper = academicQualificationsHelper;

        protected override string EntityName => "Academic Qualifications";

        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
            AcademicQualificationsSpecificationParamters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllAcademicQualificationsAsync(parameters, currentUser.Email);
        }

        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(new AcademicQualificationsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetAcademicQualificationByIdAsync(id);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto academicQualificationCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateAcademicQualificationAsync(
                academicQualificationCreateDto,
                currentUser.Email);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int academicQualificationId,
            AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateAcademicQualificationAsync(
                academicQualificationId,
                academicQualificationsUpdateDto);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId)
        {
            var currentUser = await GetCurrentUserAsync();

            var academicQualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            EnsureOwnership(academicQualification.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteAcademicQualificationAsync(academicQualificationId);
        }
    }
}