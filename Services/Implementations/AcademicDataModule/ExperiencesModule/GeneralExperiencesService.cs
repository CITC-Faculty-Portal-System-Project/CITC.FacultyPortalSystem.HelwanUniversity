using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.AcademicDataModule.ExperiencesModule
{
    public class GeneralExperiencesService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthenticationService authenticationService)
                : BaseService<GeneralExperiences, int>(unitOfWork, authenticationService, mapper), IGeneralExperiencesService
    {
        protected override string EntityName => "General Experiences";
        public async Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(GeneralExperiencesSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperiences = await Repo.GetAllAsync(new GeneralExperiencesSpecifications(parameters, currentUser.Email))
                ?? throw NotFound();

            var generalExperiencesResult = Mapper.Map<IEnumerable<GeneralExperiencesResponseDTO>>(generalExperiences);
            
            var currentPageCount = generalExperiencesResult.Count();

            var totalCount = await Repo.CountAsync(new GeneralExperiencesCountSpecifications(parameters, currentUser.Email));

            return new PaginatedResult<GeneralExperiencesResponseDTO>(parameters.PageIndex, currentPageCount, totalCount, generalExperiencesResult);
        }

        public async Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(GeneralExperiencesCreateDTO generalExperienceCreateDto)
        {
            var currentUser = await GetCurrentUserAsync(); 

            var generalExperience = Mapper.Map<GeneralExperiences>(generalExperienceCreateDto);
            generalExperience.FacultyMemberId = currentUser.UserId;

            await Repo.AddAsync(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(int generalExperienceId, GeneralExperiencesUpdateDTO generalExperienceUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            Mapper.Map(generalExperienceUpdateDto, generalExperience);

            Repo.Update(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task DeleteGeneralExperienceAsync(int generalExperienceId)
        {
            var currentUser = await GetCurrentUserAsync();

            var generalExperience = await Repo.GetAsync(new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            EnsureOwnership(generalExperience.FacultyMemberId, currentUser.UserId, EntityName);

            generalExperience.IsDeleted = true;

            Repo.Update(generalExperience);
            await SaveChangesAsync();
        }
    }
}