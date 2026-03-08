using Domain.Entities.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule
{
    public class ParticipationInQualityWorksHelper(
        IUnitOfWork unitOfWork,
        IAuthenticationService authenticationService,
        IMapper mapper)
        : BaseService<ParticipationInQualityWorks, int>(unitOfWork, authenticationService, mapper),
          IParticipationInQualityWorksServiceHelper
    {
        protected override string EntityName => "Participation In Quality Works";

        public async Task<PaginatedResult<ParticipationInQualityWorksResponseDTO>> GetAllParticipationsInQualityWorksAsync(
            ParticipationInQualityWorksSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var participations = await Repo.GetAllAsync(
                new ParticipationInQualityWorksSpecifications(parameters, facultyMemberEmail));

            var participationResult =
                Mapper.Map<IEnumerable<ParticipationInQualityWorksResponseDTO>>(participations);

            var currentPageCount = participationResult.Count();

            var totalCount = await Repo.CountAsync(
                new ParticipationInQualityWorksCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ParticipationInQualityWorksResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                participationResult);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> GetParticipationInQualityWorksByIdAsync(int id)
        {
            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> CreateParticipationInQualityWorksAsync(
            ParticipationInQualityWorksCreateDTO participationInQualityWorksCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var participation = Mapper.Map<ParticipationInQualityWorks>(participationInQualityWorksCreateDto);
            participation.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task<ParticipationInQualityWorksResponseDTO> UpdateParticipationInQualityWorksAsync(
            int participationInQualityWorksId,
            ParticipationInQualityWorksUpdateDTO participationInQualityWorksUpdateDto)
        {
            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            Mapper.Map(participationInQualityWorksUpdateDto, participation);

            Repo.Update(participation);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInQualityWorksResponseDTO>(participation);
        }

        public async Task DeleteParticipationInQualityWorksAsync(int participationInQualityWorksId)
        {
            var participation = await Repo.GetAsync(new ParticipationInQualityWorksSpecifications(participationInQualityWorksId))
                ?? throw NotFound();

            participation.IsDeleted = true;

            Repo.Update(participation);
            await SaveChangesAsync();
        }
    }
}
