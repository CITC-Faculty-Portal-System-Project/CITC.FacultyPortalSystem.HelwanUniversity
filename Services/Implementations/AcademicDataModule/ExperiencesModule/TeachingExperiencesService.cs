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
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<TeachingExperiences, int>(unitOfWork, authenticationService, mapper),
       ITeachingExperiencesService
    {
        protected override string EntityName => "Teaching Experiences";

        public async Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
            TeachingExperiencesSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var teachingExperiences = await Repo.GetAllAsync(
                new TeachingExperiencesSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<TeachingExperiencesResponseDTO>>(teachingExperiences);

            var totalCount = await Repo.CountAsync(
                new TeachingExperiencesCountSpecifications(parameters, email));

            return new PaginatedResult<TeachingExperiencesResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var teachingExperience = await Repo.GetAsync(
                new TeachingExperiencesSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                teachingExperience.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var teachingExperience = Mapper.Map<TeachingExperiences>(teachingExperienceCreateDto);
            teachingExperience.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto,
            string? facultyMemberEmail = null)
        {
            var teachingExperience = await Repo.GetAsync(
                new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                teachingExperience.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(teachingExperienceUpdateDto, teachingExperience);

            Repo.Update(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task DeleteTeachingExperienceAsync(
            int teachingExperienceId,
            string? facultyMemberEmail = null)
        {
            var teachingExperience = await Repo.GetAsync(
                new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                teachingExperience.FacultyMemberId,
                facultyMemberEmail);

            teachingExperience.IsDeleted = true;

            Repo.Update(teachingExperience);
            await SaveChangesAsync();
        }
    }
}