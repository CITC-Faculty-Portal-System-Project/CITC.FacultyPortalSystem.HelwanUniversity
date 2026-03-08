using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.AcademicDataModule.ExperiencesModule
{
    public class TeachingExperiencesService(
         IUnitOfWork unitOfWork,
         IMapper mapper,
         IAuthenticationService authenticationService,
         ITeachingExperiencesHelper teachingExperiencesHelper)
         : BaseService<TeachingExperiences, int>(unitOfWork, authenticationService, mapper),
           ITeachingExperiencesService
    {
        private readonly ITeachingExperiencesHelper _helper = teachingExperiencesHelper;

        protected override string EntityName => "Teaching Experiences";

        public async Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
            TeachingExperiencesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllTeachingExperiencesAsync(parameters, currentUser.Email);
        }

        public async Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetTeachingExperienceByIdAsync(id);
        }

        public async Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreateTeachingExperienceAsync(
                teachingExperienceCreateDto,
                currentUser.Email);
        }

        public async Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdateTeachingExperienceAsync(
                teachingExperienceId,
                teachingExperienceUpdateDto);
        }

        public async Task DeleteTeachingExperienceAsync(int teachingExperienceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeleteTeachingExperienceAsync(teachingExperienceId);
        }
    }
}