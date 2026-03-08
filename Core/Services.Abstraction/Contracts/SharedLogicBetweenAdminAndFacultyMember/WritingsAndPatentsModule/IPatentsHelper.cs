using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule
{
    public interface IPatentsHelper
    {
        Task<PaginatedResult<PatentsResponseDTO>> GetAllPatentsAsync(
    PatentsSpecificationParameters parameters,
    string facultyMemberEmail);

        Task<PatentsResponseDTO> GetPatentByIdAsync(int id);

        Task<PatentsResponseDTO> CreatePatentAsync(
            PatentsCreateDTO patentCreateDto,
            string facultyMemberEmail);

        Task<PatentsResponseDTO> UpdatePatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto);

        Task DeletePatentAsync(int patentId);
    }
}
