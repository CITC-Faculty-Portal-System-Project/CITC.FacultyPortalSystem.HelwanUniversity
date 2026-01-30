using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IAttachmentsAcsessabilityService
    {
        public Task<AttachmentReadDTO?> EnsureOnwerShipAsync(Guid userId , Guid AttachmentId);
        public Task<AttachmentResponseDTO?> EsnureNewFileAsync(string fileName , Guid facultyMemberId);
    }
}
