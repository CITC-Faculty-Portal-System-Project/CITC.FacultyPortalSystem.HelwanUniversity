using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Abstraction.Contracts.AdminModule
{
    public interface IFacultyMemberPatentsManagementService
    {
        Task<PaginatedResult<PatentsResponseDTO>> GetFacultyMemberPatentsAsync(
       PatentsSpecificationParameters parameters,
       string facultyMemberEmail);

        Task<PatentsResponseDTO> GetFacultyMemberPatentByIdAsync(int id);

        Task<PatentsResponseDTO> CreateFacultyMemberPatentAsync(
            PatentsCreateDTO patentCreateDto,
            string facultyMemberEmail);

        Task<PatentsResponseDTO> UpdateFacultyMemberPatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto);

        Task DeleteFacultyMemberPatentAsync(int patentId);
    }
}
