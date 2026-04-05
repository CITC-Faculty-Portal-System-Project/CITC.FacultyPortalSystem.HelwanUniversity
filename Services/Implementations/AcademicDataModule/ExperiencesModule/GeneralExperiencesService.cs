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
      IAuthenticationService authenticationService,
      IMapper mapper)
      : BaseService<GeneralExperiences, int>(unitOfWork, authenticationService, mapper),
        IGeneralExperiencesService
    {
        protected override string EntityName => "General Experiences";

        public async Task<PaginatedResult<GeneralExperiencesResponseDTO>> GetAllGeneralExperiencesAsync(
            GeneralExperiencesSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var generalExperiences = await Repo.GetAllAsync(
                new GeneralExperiencesSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<GeneralExperiencesResponseDTO>>(generalExperiences);

            var totalCount = await Repo.CountAsync(
                new GeneralExperiencesCountSpecifications(parameters, email));

            return new PaginatedResult<GeneralExperiencesResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<GeneralExperiencesResponseDTO> GetGeneralExperienceByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var generalExperience = await Repo.GetAsync(
                new GeneralExperiencesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                generalExperience.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> CreateGeneralExperienceAsync(
            GeneralExperiencesCreateDTO generalExperienceCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var generalExperience = Mapper.Map<GeneralExperiences>(generalExperienceCreateDto);
            generalExperience.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task<GeneralExperiencesResponseDTO> UpdateGeneralExperienceAsync(
            int generalExperienceId,
            GeneralExperiencesUpdateDTO generalExperienceUpdateDto,
            string? facultyMemberEmail = null)
        {
            var generalExperience = await Repo.GetAsync(
                new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                generalExperience.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(generalExperienceUpdateDto, generalExperience);

            Repo.Update(generalExperience);
            await SaveChangesAsync();

            return Mapper.Map<GeneralExperiencesResponseDTO>(generalExperience);
        }

        public async Task DeleteGeneralExperienceAsync(
            int generalExperienceId,
            string? facultyMemberEmail = null)
        {
            var generalExperience = await Repo.GetAsync(
                new GeneralExperiencesSpecifications(generalExperienceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                generalExperience.FacultyMemberId,
                facultyMemberEmail);

            generalExperience.IsDeleted = true;

            Repo.Update(generalExperience);
            await SaveChangesAsync();
        }
    }
}