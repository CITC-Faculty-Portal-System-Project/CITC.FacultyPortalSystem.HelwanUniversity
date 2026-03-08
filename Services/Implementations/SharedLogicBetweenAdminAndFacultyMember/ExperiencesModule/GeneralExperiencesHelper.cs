using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule
{
    public class GeneralExperiencesHelper(
          IUnitOfWork unitOfWork,
          IAuthenticationService authenticationService,
          IMapper mapper)
          : BaseService<GeneralExperiences, int>(unitOfWork, authenticationService, mapper),
            IGeneralExperiencesHelper
    {
        protected override string EntityName => "General Experiences";

        public async Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var generalExperiences = await Repo.GetAllAsync(
                new GeneralExperiencesSpecifications(parameters, facultyMemberEmail));

            var generalExperiencesResult =
                Mapper.Map<IEnumerable<GeneralExperiencesResponseDTO>>(generalExperiences);

            var currentPageCount = generalExperiencesResult.Count();

            var totalCount = await Repo.CountAsync(
                new GeneralExperiencesCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<GeneralExperiencesResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                generalExperiencesResult);
        }

        public async Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(int id)
        {
            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var generalExperience = Mapper.Map<GeneralExperiences>(generalExperienceCreateDto);
            generalExperience.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
        {
            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            Mapper.Map(generalExperienceUpdateDto, generalExperience);

            Repo.Update(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task DeleteGeneralExperienceAsync(int generalExperienceId)
        {
            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            generalExperience.IsDeleted = true;

            Repo.Update(generalExperience);
            await SaveChangesAsync();
        }
    }
}
