using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule
{
    public interface IThesesSupervisingService
    {
        Task<SupervisingThesesAddDTO> AddThesesSupervising(
         SupervisingThesesAddDTO thesesDTO,
         Guid? facultyMemberId = null);

        Task<SupervisingThsesResponseDTO> GetThesesSupervisingById(
            int id,
            Guid? facultyMemberId = null);

        Task<SupervisingThsesResponseDTO> UpdateThesesSupervising(
            int id,
            SupervisingThesesUpdateDTO supervisingThesesUpdateDTO,
            Guid? facultyMemberId = null);

        Task DeleteThesesSupervising(
            int id,
            Guid? facultyMemberId = null);

        Task<PaginatedResult<SupervisingThsesResponseDTO>> GetAllSupervisings(
            ThesesSupervisingSpecificationParameters supervisingSpecificationParameters,
            Guid? facultyMemberId = null);

    }
}
