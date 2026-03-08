using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Implementations.AdminModule
{
    public class FacultyMemberPatentsManagementService(IPatentsHelper _helper)
        :IFacultyMemberPatentsManagementService
    {

        public Task<PaginatedResult<PatentsResponseDTO>> GetFacultyMemberPatentsAsync(
            PatentsSpecificationParameters parameters,
            string facultyMemberEmail)
            => _helper.GetAllPatentsAsync(parameters, facultyMemberEmail);

        public Task<PatentsResponseDTO> GetFacultyMemberPatentByIdAsync(int id)
            => _helper.GetPatentByIdAsync(id);

        public Task<PatentsResponseDTO> CreateFacultyMemberPatentAsync(
            PatentsCreateDTO patentCreateDto,
            string facultyMemberEmail)
            => _helper.CreatePatentAsync(patentCreateDto, facultyMemberEmail);

        public Task<PatentsResponseDTO> UpdateFacultyMemberPatentAsync(
            int patentId,
            PatentsUpdateDTO patentUpdateDto)
            => _helper.UpdatePatentAsync(patentId, patentUpdateDto);

        public Task DeleteFacultyMemberPatentAsync(int patentId)
            => _helper.DeletePatentAsync(patentId);
    }
}
