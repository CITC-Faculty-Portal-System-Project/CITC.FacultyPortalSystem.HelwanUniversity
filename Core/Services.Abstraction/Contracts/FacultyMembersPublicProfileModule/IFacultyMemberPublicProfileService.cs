using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;

namespace Services.Abstraction.Contracts.FacultyMembersPublicProfileModule
{
    public interface IFacultyMemberPublicProfileService
    {
        public Task<PaginatedResult<OtherUsersPageResponseDTO>> GetAllFacultyMembersProfiles(FacultyMembersProfileSpecificationParamters paramters);
        public Task<FacultyMemberPublicProfileResponseDTO> GetFacultyMemberPublicProfile(Guid facultyMemberId);
    }
}
