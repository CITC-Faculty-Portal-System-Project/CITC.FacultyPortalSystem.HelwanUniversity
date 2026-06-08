using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.EncryptionServices;
using Shared.Dtos.AttachmentsModule;

namespace Services.Implementations.AttachmentsModule.Helpers
{
    public sealed class AttachmentCore(
           IAttachmentEncryptionService _encryptionService,
           IFTPFileStorageService _fTPClientService,
           IUnitOfWork _unitOfWork,
           IMapper _mapper,
           IProcessingService _processingService,
           IAuthenticationService _authenticationService)
    {
        public Func<Guid, string> BuildRemotePath { get; } =
            userId => $"files/{userId}/{Guid.NewGuid()}";

        public async Task<IReadOnlyList<AttachmentResponseDTO>> AddInternalAsync<TAttachment>(
            int ownerId,
            IList<IFormFile> files,
            Action<TAttachment, int> setOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity, new()
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();
    
            var results = new List<AttachmentResponseDTO>(files.Count);

            foreach (var file in files)
            {
                var remotePath = BuildRemotePath(user.UserId);

                var attachmentRefDto = await _processingService.ProcessAsync(
                    file,
                    remotePath,
                    creator: user.UserName);

                var entity = _mapper.Map<TAttachment>(attachmentRefDto);
                setOwner(entity, ownerId);
  
                await repo.AddAsync(entity);
                results.Add(_mapper.Map<AttachmentResponseDTO>(entity));
            }

            await _unitOfWork.SaveChangesAsync();
            return results;
        }

        public async Task<AttachmentDownloadDTO> GetInternalAsync<TAttachment>(
            int ownerId,
            Guid attachmentId,
            Func<TAttachment, int, bool> matchOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity
        {
            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();
            var attachment = await repo.GetByIdAsync(attachmentId)
                ?? throw new KeyNotFoundException("Attachment not found.");

            if (!matchOwner(attachment, ownerId))
                throw new UnauthorizedAccessException("Attachment does not belong to this owner.");

            using var encryptedStream = await _fTPClientService.DownloadFileAsync(attachment.RemotePath);
            using var ms = new MemoryStream();
            await encryptedStream.CopyToAsync(ms, ct);

            var refDto = _mapper.Map<AttachmentReferenceDTO>(attachment);

            var fileData = await _encryptionService.DecryptAsync(ms.ToArray(), refDto);

            return new AttachmentDownloadDTO
            {
                AttachmentData = fileData,
                FileName = attachment.FileName
            };
        }

        public async Task<AttachmentResponseDTO> UpdateInternalAsync<TAttachment>(
            int ownerId,
            Guid attachmentId,
            IFormFile newFile,
            Func<TAttachment, int, bool> matchOwner,
            Func<TAttachment, string> getOwnerFolder,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity
        {
            if (newFile is null || newFile.Length == 0)
                throw new ArgumentException("File is empty", nameof(newFile));

            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();
            var entity = await repo.GetByIdAsync(attachmentId)
                ?? throw new KeyNotFoundException("Attachment not found.");

            if (!matchOwner(entity, ownerId))
                throw new UnauthorizedAccessException("Attachment does not belong to this owner.");

            var oldRemotePath = entity.RemotePath;
            var remoteFolder = $"{user.UserId}/{getOwnerFolder(entity)}/{Guid.NewGuid()}";

            var newRefDto = await _processingService.ProcessAsync(
                newFile,
                remoteFolder,
                creator: user.UserName);

            _mapper.Map(newRefDto, entity);
            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(oldRemotePath) && oldRemotePath != entity.RemotePath)
                await _fTPClientService.DeleteFileAsync(oldRemotePath);

            return _mapper.Map<AttachmentResponseDTO>(entity);
        }

        public async Task DeleteInternalAsync<TAttachment>(
            int ownerId,
            Guid attachmentId,
            Func<TAttachment, int, bool> matchOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();
            var entity = await repo.GetByIdAsync(attachmentId)
                ?? throw new KeyNotFoundException("Attachment not found.");

            if (!matchOwner(entity, ownerId))
                throw new UnauthorizedAccessException("Attachment does not belong to this owner.");

            if (entity.IsDeleted) return;

            var remotePath = entity.RemotePath;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = user.UserName;

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(remotePath))
                await _fTPClientService.DeleteFileAsync(remotePath);
        }
    }
}
