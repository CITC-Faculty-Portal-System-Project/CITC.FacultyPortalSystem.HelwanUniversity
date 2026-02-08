using Shared.Dtos.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IThesesSupervisingService
    {
        public Task<SupervisingThesesAddDTO> AddThesesSupervising(SupervisingThesesAddDTO thesesDTO);
        public Task<SupervisingThsesResponseDTO> GetThesesSupervisingById(int id);
        public Task<SupervisingThsesResponseDTO> UpdateThesesSupervising(int id , SupervisingThesesUpdateDTO supervisingThesesUpdateDTO);
        public Task DeleteThesesSupervising(int id);
        public Task<PaginatedResult<SupervisingThsesResponseDTO>> GetAllSupervisings(ThesesSupervisingSpecificationParameters supervisingSpecificationParameters);
    }
}
