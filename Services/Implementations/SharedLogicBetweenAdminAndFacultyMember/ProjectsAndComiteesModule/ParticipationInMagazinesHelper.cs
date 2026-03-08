using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule
{
    public class ParticipationInMagazinesHelper(
         IUnitOfWork unitOfWork,
         IAuthenticationService authenticationService,
         IMapper mapper)
         : BaseService<ParticipationInMagazines, int>(unitOfWork, authenticationService, mapper),
           IParticipationInMagazinesHelper
    {
        protected override string EntityName => "Participation In Magazines";

        public async Task<PaginatedResult<ParticipationInMagazinesResponseDto>> GetAllParticipationInMagazinesAsync(
            ParticipationInMagazinesSpecificationsParameters parameters,
            string facultyMemberEmail)
        {
            var participationInMagazines = await Repo.GetAllAsync(
                new ParticipationInMagazinesSpecifications(parameters, facultyMemberEmail));

            var participationInMagazinesResult =
                Mapper.Map<IEnumerable<ParticipationInMagazinesResponseDto>>(participationInMagazines);

            var currentPageSize = participationInMagazinesResult.Count();

            var totalCount = await Repo.CountAsync(
                new ParticipationInMagazinesCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<ParticipationInMagazinesResponseDto>(
                parameters.PageIndex,
                currentPageSize,
                totalCount,
                participationInMagazinesResult);
        }

        public async Task<ParticipationInMagazinesResponseDto> GetParticipationInMagazineByIdAsync(int id)
        {
            var participationInMagazine = await Repo.GetAsync(new ParticipationInMagazinesSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> CreateParticipationInMagazineAsync(
            ParticipationInMagazineCreateDto participationInMagazinesCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var participationInMagazine = Mapper.Map<ParticipationInMagazines>(participationInMagazinesCreateDto);
            participationInMagazine.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(participationInMagazine);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task<ParticipationInMagazinesResponseDto> UpdateParticipationInMagazineAsync(
            int participationInMagazineId,
            ParticipationInMagazineUpdateDto participationInMagazinesUpdateDto)
        {
            var participationInMagazine = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            Mapper.Map(participationInMagazinesUpdateDto, participationInMagazine);

            Repo.Update(participationInMagazine);
            await SaveChangesAsync();

            return Mapper.Map<ParticipationInMagazinesResponseDto>(participationInMagazine);
        }

        public async Task DeleteParticipationInMagazineAsync(int participationInMagazineId)
        {
            var participationInMagazine = await Repo.GetAsync(
                new ParticipationInMagazinesSpecifications(participationInMagazineId))
                ?? throw NotFound();

            participationInMagazine.IsDeleted = true;

            Repo.Update(participationInMagazine);
            await SaveChangesAsync();
        }
    }
}
