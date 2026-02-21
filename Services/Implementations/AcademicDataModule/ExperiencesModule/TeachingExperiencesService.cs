using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
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
        IValidationService validationService)
                : BaseService<TeachingExperiences, int>(unitOfWork, authenticationService, mapper, validationService), ITeachingExperiencesService
    {
        protected override string EntityName => "Teaching Experiences";
        public async Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(TeachingExperiencesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperiences = await Repo.GetAllAsync(new TeachingExperiencesSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var teachingExperiencesResult = Mapper.Map<IEnumerable<TeachingExperiencesResponseDTO>>(teachingExperiences);

            var currentPageCount = teachingExperiencesResult.Count();

            var totalCount = await Repo.CountAsync(new TeachingExperiencesCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<TeachingExperiencesResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, teachingExperiencesResult);
        }

        public async Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(TeachingExperiencesCreateDTO teachingExperienceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = Mapper.Map<TeachingExperiences>(teachingExperienceCreateDto);
            teachingExperience.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(int teachingExperienceId, TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(teachingExperienceUpdateDto, teachingExperience);

            Repo.Update(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task DeleteTeachingExperienceAsync(int teachingExperienceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            EnsureOwnership(teachingExperience.FacultyMemberId, currentUser.UserId, EntityName);

            teachingExperience.IsDeleted = true;

            Repo.Update(teachingExperience);
            await SaveChangesAsync();
        }
    }
}