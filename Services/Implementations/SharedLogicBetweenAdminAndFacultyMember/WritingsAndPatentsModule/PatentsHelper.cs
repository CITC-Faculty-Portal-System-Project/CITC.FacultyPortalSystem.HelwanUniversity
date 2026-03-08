using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule
{
    public class PatentsHelper(
     IUnitOfWork unitOfWork,
     IAuthenticationService authenticationService,
     IMapper mapper)
     : BaseService<Patents, int>(unitOfWork, authenticationService, mapper),
       IPatentsHelper
    {
        protected override string EntityName => "Patents";

        public async Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
            PatentsSpecificationParameters parameters,
            string facultyMemberEmail)
        {
            var patents = await Repo.GetAllAsync(
                new PatentsSpecifications(parameters, facultyMemberEmail))
                ?? throw NotFound();

            var patentsResult = Mapper.Map<IEnumerable<PatentsResponseDTO>>(patents);

            var currentPageCount = patentsResult.Count();

            var totalCount = await Repo.CountAsync(
                new PatentsCountSpecifications(parameters, facultyMemberEmail));

            return new PaginatedResult<PatentsResponseDTO>(
                parameters.PageIndex,
                currentPageCount,
                totalCount,
                patentsResult);
        }

        public async Task<PatentsResponseDTO> GetPatentByIdAsync(int id)
        {
            var patent = await Repo.GetAsync(new PatentsSpecifications(id))
                ?? throw NotFound();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> CreatePatentAsync(
            PatentsCreateDTO patentCreateDto,
            string facultyMemberEmail)
        {
            var facultyMember = await GetFacultyMemberByEmailAsync(facultyMemberEmail);

            var patent = Mapper.Map<Patents>(patentCreateDto);
            patent.FacultyMemberId = facultyMember.Id;

            await Repo.AddAsync(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task<PatentsResponseDTO> UpdatePatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto)
        {
            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            Mapper.Map(patentUpdateDto, patent);

            Repo.Update(patent);
            await SaveChangesAsync();

            return Mapper.Map<PatentsResponseDTO>(patent);
        }

        public async Task DeletePatentAsync(int patentId)
        {
            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            patent.IsDeleted = true;

            Repo.Update(patent);
            await SaveChangesAsync();
        }
    }
}
