using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.SpecificationParameters.AcademicDataModule.ExperiencesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule
{
    public class TeachingExperiencesHelper(
          IUnitOfWork unitOfWork,
          IAuthenticationService authenticationService,
          IMapper mapper)
          : BaseService<TeachingExperiences, int>(unitOfWork, authenticationService, mapper),
            ITeachingExperiencesHelper
    {
        protected override string EntityName => "Teaching Experiences";

        public async Task<PaginatedResult<TeachingExperiencesResponseDTO>> GetAllTeachingExperiencesAsync(
            TeachingExperiencesSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var teachingExperiences = await Repo.GetAllAsync(
                new TeachingExperiencesSpecifications(parameters, facultyMemberEmail));

            var teachingExperiencesResult =
                Mapper.Map<IEnumerable<TeachingExperiencesResponseDTO>>(teachingExperiences);

            var currentPageCount = teachingExperiencesResult.Count();

            var totalCount = await Repo.CountAsync(
                new TeachingExperiencesCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<TeachingExperiencesResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                teachingExperiencesResult);
        }

        public async Task<TeachingExperiencesResponseDTO> GetTeachingExperienceByIdAsync(int id)
        {
            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> CreateTeachingExperienceAsync(
            TeachingExperiencesCreateDTO teachingExperienceCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var teachingExperience = Mapper.Map<TeachingExperiences>(teachingExperienceCreateDto);
            teachingExperience.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task<TeachingExperiencesResponseDTO> UpdateTeachingExperienceAsync(
            int teachingExperienceId,
            TeachingExperiencesUpdateDTO teachingExperienceUpdateDto)
        {
            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            Mapper.Map(teachingExperienceUpdateDto, teachingExperience);

            Repo.Update(teachingExperience);
            await SaveChangesAsync();

            return Mapper.Map<TeachingExperiencesResponseDTO>(teachingExperience);
        }

        public async Task DeleteTeachingExperienceAsync(int teachingExperienceId)
        {
            var teachingExperience = await Repo.GetAsync(new TeachingExperiencesSpecifications(teachingExperienceId))
                ?? throw NotFound();

            teachingExperience.IsDeleted = true;

            Repo.Update(teachingExperience);
            await SaveChangesAsync();
        }
    }
}
