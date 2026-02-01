using Domain.Contracts;
using Domain.Entities.Attachments;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Specifications.AttachmentsModule;
using Shared.Dtos.AttachmentsModule;
using System.Net.Mail;

namespace Services.Implementations.AttachmentsModule
{
    public class AttachmentService(IEncryptionService _encryptionService , 
        IFTPFileStorageService _fTPClientService , IUnitOfWork _unitOfWork 
        , IMapper _mapper , IProcessingService _processingService ,
        IAuthenticationService _authenticationService , 
        IAttachmentsAcsessabilityService _attachmentsAcsessabilityService) : IAttachmentService
    {

        #region Repo Initialsation

        private IGenericRepository<AttachmentReference, Guid> AttachmentReferenceRepo
           => _unitOfWork.GetRepository<AttachmentReference, Guid>();


        #endregion

        #region Helpers

        private async Task<AttachmentReference> CreateNewAttachmentAsync(
    IFormFile file,
    Guid userId,
    string userName)
        {
            var upload = await _processingService.ProcessAsync(file, "/files", "creator");

            var entity = _mapper.Map<AttachmentReference>(upload);
            entity.CreatedBy = userName;
            entity.FacultyMembers!.Add(new FacultyMemberAttachments
            {
                FacultyMemberId = userId
            });

            return entity;
        }

        private async Task AttachToExistingFileAsync(Guid attachmentId, Guid userId)
        {
            var entity = await AttachmentReferenceRepo.GetAsync(
                new AttachmentGetByIdSpecification(attachmentId))
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            if (entity.FacultyMembers?.Any(x => x.FacultyMemberId == userId && !x.IsDeleted) == true)
                return;

            entity.FacultyMembers!.Add(new FacultyMemberAttachments
            {
                FacultyMemberId = userId
            });

            AttachmentReferenceRepo.Update(entity);
        }

        private async Task<AttachmentReference> SplitAndCreateNewAsync(
            Guid oldAttachmentId,
            Guid userId,
            string userName,
            IFormFile newFile)
        {
            var old = await AttachmentReferenceRepo.GetAsync(
                new AttachmentGetByIdSpecification(oldAttachmentId))
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            var link = old.FacultyMembers?
                .FirstOrDefault(x => x.FacultyMemberId == userId);

            if (link is not null)
                old.FacultyMembers!.Remove(link);

            var fresh = await CreateNewAttachmentAsync(newFile, userId, userName);

            AttachmentReferenceRepo.Update(old);
            await AttachmentReferenceRepo.AddAsync(fresh);
            await _unitOfWork.SaveChangesAsync();

            return fresh;
        }

        private async Task<AttachmentReference> SwitchUserToExistingAsync(
            Guid oldAttachmentId,
            Guid existingAttachmentId,
            Guid userId,
            string userName)
        {
            var old = await AttachmentReferenceRepo.GetAsync(
                new AttachmentGetByIdSpecification(oldAttachmentId))
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            var existing = await AttachmentReferenceRepo.GetAsync(
                new AttachmentGetByIdSpecification(existingAttachmentId))
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            var link = old.FacultyMembers?
                .FirstOrDefault(x => x.FacultyMemberId == userId);

            if (link is not null)
                old.FacultyMembers!.Remove(link);

            if (existing.FacultyMembers?.Any(x => x.FacultyMemberId == userId && !x.IsDeleted) != true)
                existing.FacultyMembers!.Add(new FacultyMemberAttachments
                {
                    FacultyMemberId = userId
                });

            if ((old.FacultyMembers?.Count ?? 0) == 0)
            {
                await _fTPClientService.DeleteFileAsync(old.RemotePath);
                old.IsDeleted = true;
                old.DeletedBy = userName;
            }

            AttachmentReferenceRepo.Update(old);
            AttachmentReferenceRepo.Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return existing;
        }

        private async Task<AttachmentReference> ReplaceSingleOwnerAsync(
            Guid attachmentId,
            string userName,
            IFormFile newFile)
        {
            var old = await AttachmentReferenceRepo.GetByIdAsync(attachmentId)
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            await _fTPClientService.DeleteFileAsync(old.RemotePath);

            var upload = await _processingService.ProcessAsync(newFile, "/files", "creator");
            _mapper.Map(upload, old);

            AttachmentReferenceRepo.Update(old);
            await _unitOfWork.SaveChangesAsync();

            return old;
        }

        private async Task DeleteForUserAsync(Guid attachmentId, Guid userId, string userName)
        {
            var attachment = await AttachmentReferenceRepo.GetAsync(
                new AttachmentGetByIdSpecification(attachmentId))
                ?? throw new NotFoundException("Desired Attachment Wasn't Found!");

            var link = attachment.FacultyMembers?
                .FirstOrDefault(x => x.FacultyMemberId == userId)
                ?? throw new UnauthorizedException("You aren't authorized to delete this file!");

            if ((attachment.FacultyMembers?.Count ?? 0) <= 1)
            {
                await _fTPClientService.DeleteFileAsync(attachment.RemotePath);
                attachment.IsDeleted = true;
                attachment.DeletedBy = userName;
            }

            link.IsDeleted = true;
            link.DeletedBy = userName;
            link.DeletedAt = DateTime.UtcNow;

            AttachmentReferenceRepo.Update(attachment);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        public async Task<AttachmentDownloadDTO> GetAttachmentAsync(Guid attachmentId)
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var attachmentDto = await _attachmentsAcsessabilityService.EnsureOnwerShipAsync(user.UserId, attachmentId);

            using var encryptedStream = await _fTPClientService.DownloadFileAsync(attachmentDto!.RemotePath);

            using var ms = new MemoryStream();
            await encryptedStream.CopyToAsync(ms);

            var fileData = await _encryptionService.DecryptAsync(
                ms.ToArray(),
                _mapper.Map<AttachmentReferenceDTO>(attachmentDto));

            return new AttachmentDownloadDTO
            {
                AttachmentData = fileData,
                FileName = attachmentDto.FileName
            };
        }

        public async Task<AttachmentResponseDTO> UpdateAttachmentAsync(Guid oldAttachmentId, IFormFile newFile)
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var oldDto = await _attachmentsAcsessabilityService.EnsureOnwerShipAsync(user.UserId, oldAttachmentId);
            var existingNew = await _attachmentsAcsessabilityService.EsnureNewFileAsync(newFile.FileName, user.UserId);

            if (existingNew is not null)
                return _mapper.Map<AttachmentResponseDTO>(
                    await SwitchUserToExistingAsync(oldDto!.Id, existingNew.Id, user.UserId, user.UserName));

            if (oldDto!.FacultyMembersCount > 1)
                return _mapper.Map<AttachmentResponseDTO>(
                    await SplitAndCreateNewAsync(oldDto.Id, user.UserId, user.UserName, newFile));

            return _mapper.Map<AttachmentResponseDTO>(
                await ReplaceSingleOwnerAsync(oldDto.Id, user.UserName, newFile));
        }

        public async Task<IEnumerable<AttachmentResponseDTO>> AddAttachmentAsync(IList<IFormFile> files)
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var newAttachments = new List<AttachmentReference>();

            foreach (var file in files)
            {
                var existing = await _attachmentsAcsessabilityService.EsnureNewFileAsync(file.FileName, user.UserId);

                if (existing is null)
                    newAttachments.Add(await CreateNewAttachmentAsync(file, user.UserId, user.UserName));
                else
                    await AttachToExistingFileAsync(existing.Id, user.UserId);
            }

            await AttachmentReferenceRepo.AddRangeAsync(newAttachments);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<IEnumerable<AttachmentResponseDTO>>(newAttachments);
        }

        public async Task DeleteAttachmentAsync(Guid attachmentId)
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            await _attachmentsAcsessabilityService.EnsureOnwerShipAsync(user.UserId, attachmentId);
            await DeleteForUserAsync(attachmentId, user.UserId, user.UserName);
        }
    }
}
