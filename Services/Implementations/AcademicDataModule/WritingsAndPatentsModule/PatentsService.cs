using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
    public class PatentsService(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<Patents, int>(unitOfWork, authenticationService, mapper),
       IPatentsService
    {
        protected override string EntityName => "Patents";

        public async Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
            PatentsSpecificationParameters parameters,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var patents = await Repo.GetAllAsync(
                new PatentsSpecifications(parameters, email))
                ?? throw NotFound();

            var mapped = Mapper.Map<IEnumerable<PatentsResponseDTO>>(patents);

            var totalCount = await Repo.CountAsync(
                new PatentsCountSpecifications(parameters, email));

            return new PaginatedResult<PatentsResponseDTO>(
                parameters.PageIndex,
                mapped.Count(),
                totalCount,
                mapped);
        }

        public async Task<PatentsResponseDTO> GetPatentByIdAsync(
            int id,
            string? facultyMemberEmail = null)
        {
            var patent = await Repo.GetAsync(
                new PatentsSpecifications(id))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                patent.FacultyMemberId,
                facultyMemberEmail);

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> CreatePatentAsync(
            PatentsCreateDTO patentCreateDto,
            string? facultyMemberEmail = null)
        {
            var currentUser = await GetCurrentUserAsync();
            var email = facultyMemberEmail ?? currentUser.Email;

            var facultyMember = await GetFacultyMemberByEmailAsync(email);

            var patent = Mapper.Map<Patents>(patentCreateDto);
            patent.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> UpdatePatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto,
            string? facultyMemberEmail = null)
        {
            var patent = await Repo.GetAsync(
                new PatentsSpecifications(patentId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                patent.FacultyMemberId,
                facultyMemberEmail);

            Mapper.Map(patentUpdateDto, patent);

            Repo.Update(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task DeletePatentAsync(
            int patentId,
            string? facultyMemberEmail = null)
        {
            var patent = await Repo.GetAsync(
                new PatentsSpecifications(patentId))
                ?? throw NotFound();

            await EnsureOwnershipIfClientAsync(
                patent.FacultyMemberId,
                facultyMemberEmail);

            patent.IsDeleted = true;

            Repo.Update(patent);
            await SaveChangesAsync();
        }
    }
}