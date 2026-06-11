using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;

namespace Services.Abstraction.Contracts.FacultyMembersPublicProfileModule
{
    public interface IFacultyMemberPublicProfileService
    {
        public Task<CursorPaginatedResult<OtherUsersPageResponseDTO , Guid>> GetAllFacultyMembersProfiles(FacultyMembersProfileSpecificationParamters paramters);
        public Task<FacultyMemberPublicProfileResponseDTO> GetFacultyMemberPublicProfile(Guid facultyMemberId);
        public Task<IEnumerable<OtherUsersPageResponseDTO>> SearchMemberPublicProfile(BaseFacultyMemberProfileSpecificationParamters paramters);
    }
}
