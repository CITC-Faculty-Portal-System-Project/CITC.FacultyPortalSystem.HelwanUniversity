using Domain.Entities.Attachments;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Specifications.AttachmentsModule;
using Shared.Dtos.AttachmentsModule;

namespace Services.Implementations.AttachmentsModule
{
    public class AttachmentsAcsessablityService(IUnitOfWork _unitOfWork , IMapper _mapper) : IAttachmentsAcsessabilityService
    {

        #region Repo

        private IGenericRepository<AttachmentReference, Guid> AttachmentReferenceRepo
             => _unitOfWork.GetRepository<AttachmentReference, Guid>();

        #endregion

        public async Task<AttachmentReadDTO?> EnsureOnwerShipAsync(Guid userId, Guid AttachmentId)
        {
            var ownedSpecification = new AttachmentsAcsessSpecifications(userId, AttachmentId);
            var attachment = await AttachmentReferenceRepo.GetAsync(ownedSpecification);

            if (attachment is null)
                throw new UnauthorizedException("You aren't authroized to acsess this attachment!");

            return _mapper.Map<AttachmentReadDTO?>(attachment);    
        }

        public async Task<AttachmentResponseDTO?> EsnureNewFileAsync(string fileName , Guid facultyMemberId)
        {
            var spec = new AttachmentsAcsessSpecifications(fileName);
            var attachment = await AttachmentReferenceRepo.GetAsync(spec);

            if (attachment is null)
                return null;

            if (attachment.FacultyMembers?.Any(f => f.FacultyMemberId == facultyMemberId) == true)
                throw new AttachmentAlreadyExist(fileName);

            return _mapper.Map<AttachmentResponseDTO>(attachment);
        }
    }
}
