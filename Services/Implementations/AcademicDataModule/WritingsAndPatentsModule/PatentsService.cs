using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule;
using Services.Global;
using Services.Specifications.AcademicDataModule.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.AcademicDataModule.WritingsAndPatentsModule
{
    public class PatentsService(
      IUnitOfWork unitOfWork,
      IMapper mapper,
      IAuthenticationService authenticationService,
      IPatentsHelper patentsHelper)
      : BaseService<Patents, int>(unitOfWork, authenticationService, mapper),
        IPatentsService
    {
        private readonly IPatentsHelper _helper = patentsHelper;

        protected override string EntityName => "Patents";

        public async Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
            PatentsSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.GetAllPatentsAsync(parameters, currentUser.Email);
        }

        public async Task<PatentsResponseDTO> GetPatentByIdAsync(int id)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(id))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.GetPatentByIdAsync(id);
        }

        public async Task<PatentsResponseDTO> CreatePatentAsync(PatentsCreateDTO patentCreateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            return await _helper.CreatePatentAsync(patentCreateDto, currentUser.Email);
        }

        public async Task<PatentsResponseDTO> UpdatePatentAsync(int patentId, PatentsUpdateDTO patentUpdateDto)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            return await _helper.UpdatePatentAsync(patentId, patentUpdateDto);
        }

        public async Task DeletePatentAsync(int patentId)
        {
            var currentUser = await GetCurrentUserAsync();

            var patent = await Repo.GetAsync(new PatentsSpecifications(patentId))
                ?? throw NotFound();

            EnsureOwnership(patent.FacultyMemberId, currentUser.UserId, EntityName);

            await _helper.DeletePatentAsync(patentId);
        }
    }
}