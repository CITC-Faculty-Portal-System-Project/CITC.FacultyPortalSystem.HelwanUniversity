using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.AcademicDataModule.ExperiencesModule
{
    public class GeneralExperiencesService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService,
        IGeneralExperiencesHelper generalExperiencesHelper)
        : BaseService<GeneralExperiences, int>(unitOfWork, authenticationService, mapper),
          IGeneralExperiencesService
    {
        private readonly IGeneralExperiencesHelper _helper = generalExperiencesHelper;

        protected override string EntityName => "General Experiences";

        public async Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllGeneralExperiencesAsync(parameters, currentUser.Email);
        }

        public async Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetGeneralExperienceByIdAsync(id);
        }

        public async Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateGeneralExperienceAsync(
                generalExperienceCreateDto,
                currentUser.Email);
        }

        public async Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateGeneralExperienceAsync(
                generalExperienceId,
                generalExperienceUpdateDto);
        }

        public async Task DeleteGeneralExperienceAsync(int generalExperienceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteGeneralExperienceAsync(generalExperienceId);
        }
    }
}