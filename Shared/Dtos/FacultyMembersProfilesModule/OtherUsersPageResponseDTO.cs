using Shared.Dtos.AttachmentsModule;
using Shared.Enums.IdentityModule.SpecificationEnums;

namespace Shared.Dtos.FacultyMembersProfilesModule
{
    public record OtherUsersPageResponseDTO
    {
        public Guid Id { get; set; }
        public string FacultyMemberName { get; set; } = string.Empty;
        public string FacultyMemberPosition { get; set; } = string.Empty;
        public string FacultyMemberDepartment { get; set; } = string.Empty;
        public AttachmentResponseDTO? ProfilePicture { get; set; }
    }
}
