using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule
{
    public class AcademicQualificationsHelper(
       IUnitOfWork unitOfWork,
       IAuthenticationService authenticationService,
       IMapper mapper)
       : BaseService<AcademicQualifications, int>(unitOfWork, authenticationService, mapper),
         IAcademicQualificationsHelper
    {
        protected override string EntityName => "Academic Qualifications";

        public async Task<PaginatedResult<AcademicQualificationResponseDto>> GetAllAcademicQualificationsAsync(
            AcademicQualificationsSpecificationParamters parameters,
            string facultyMemberEmail)
        {
            var academicQualifications = await Repo.GetAllAsync(
                new AcademicQualificationsSpecifications(parameters, facultyMemberEmail));

            var academicQualificationsResult =
                Mapper.Map<IEnumerable<AcademicQualificationResponseDto>>(academicQualifications);

            var currentPageCount = academicQualificationsResult.Count();

            var totalCount = await Repo.CountAsync(
                new AcademicQualificationsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<AcademicQualificationResponseDto>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                academicQualificationsResult);
        }

        public async Task<AcademicQualificationResponseDto> GetAcademicQualificationByIdAsync(int id)
        {
            var academicQualification = await Repo.GetAsync(new AcademicQualificationsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> CreateAcademicQualificationAsync(
            AcademicQualificationCreateDto academicQualificationCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var academicQualification = Mapper.Map<AcademicQualifications>(academicQualificationCreateDto);
            academicQualification.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(academicQualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task<AcademicQualificationResponseDto> UpdateAcademicQualificationAsync(
            int academicQualificationId,
            AcademicQualificationsUpdateDto academicQualificationsUpdateDto)
        {
            var academicQualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            Mapper.Map(academicQualificationsUpdateDto, academicQualification);

            Repo.Update(academicQualification);
            await SaveChangesAsync();

            return Mapper.Map<AcademicQualificationResponseDto>(academicQualification);
        }

        public async Task DeleteAcademicQualificationAsync(int academicQualificationId)
        {
            var academicQualification = await Repo.GetAsync(
                new AcademicQualificationsSpecifications(academicQualificationId))
                ?? throw NotFound();

            academicQualification.IsDeleted = true;

            Repo.Update(academicQualification);
            await SaveChangesAsync();
        }
    }
}
