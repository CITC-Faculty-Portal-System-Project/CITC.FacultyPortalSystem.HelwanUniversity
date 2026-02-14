using Domain.Contracts;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;
using System.Net.Mail;

namespace Services.Implementations.AttachmentsModule
{
    public class AttachmentService(IEncryptionService _encryptionService , 
        IFTPFileStorageService _fTPClientService , IUnitOfWork _unitOfWork 
        , IMapper _mapper , IProcessingService _processingService ,
        IAuthenticationService _authenticationService) : IAttachmentService
    {


        #region Helpers

        Func<Guid, string> buildRemotePath = userId => $"files/{userId}/{Guid.NewGuid()}";


        private static readonly IReadOnlyDictionary<AttachmentContext, OwnerBindingMap> _ownerMap =
        new Dictionary<AttachmentContext, OwnerBindingMap>
        {
            [AttachmentContext.Research] = new OwnerBindingMap
            {
                Create = () => new ResearchAttachment(),
                SetOwner = (a, id) => ((ResearchAttachment)a).ResearchId = id,
                MatchOwner = (a, id) => ((ResearchAttachment)a).ResearchId == id
            },

            [AttachmentContext.Thesis] = new OwnerBindingMap
            {
                Create = () => new ThesesAttachment(),
                SetOwner = (a, id) => ((ThesesAttachment)a).ThesisId = id,
                MatchOwner = (a, id) => ((ThesesAttachment)a).ThesisId == id
            }
        };

        private async Task<IReadOnlyList<AttachmentResponseDTO>> AddInternalAsync<TAttachment>(
            int ownerId,
            IList<IFormFile> files,
            Func<Guid, string> buildRemotePath,
            Action<TAttachment, int> setOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity, new()
        {
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());
            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();

            var results = new List<AttachmentResponseDTO>(files.Count);

            foreach (var file in files)
            {
                var remotePath = buildRemotePath(user.UserId);

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


        private async Task<AttachmentDownloadDTO> GetInternalAsync<TAttachment>(
            int ownerId,
            Guid attachmentId,
            Func<TAttachment, int, bool> matchOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity
        {
            var user = await _authenticationService
                .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();
            var attachment = await repo.GetByIdAsync(attachmentId);
            if (attachment is null)
                throw new KeyNotFoundException("Attachment not found.");

            if (!matchOwner(attachment, ownerId))
                throw new UnauthorizedAccessException("Attachment does not belong to this owner.");

            using var encryptedStream = await _fTPClientService.DownloadFileAsync(attachment.RemotePath);

            using var ms = new MemoryStream();
            await encryptedStream.CopyToAsync(ms, ct);
            
            var refDto = _mapper.Map<AttachmentReferenceDTO>(attachment);

            var fileData = await _encryptionService.DecryptAsync(
                ms.ToArray(),
                refDto);

            return new AttachmentDownloadDTO
            {
                AttachmentData = fileData,
                FileName = attachment.FileName
            };
        }


        private async Task<AttachmentResponseDTO> UpdateInternalAsync<TAttachment>(
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

            var user = await _authenticationService
                .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

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
                creator: user.UserName
            );

            _mapper.Map(newRefDto, entity); 
            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(oldRemotePath) && oldRemotePath != entity.RemotePath)
                await _fTPClientService.DeleteFileAsync(oldRemotePath);

            return _mapper.Map<AttachmentResponseDTO>(entity);
        }


        private async Task DeleteInternalAsync<TAttachment>(
            int ownerId,
            Guid attachmentId,
            Func<TAttachment, int, bool> matchOwner,
            CancellationToken ct)
            where TAttachment : BaseAttachmentEntity
        {
                var user = await _authenticationService
                .GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var repo = _unitOfWork.GetRepository<TAttachment, Guid>();

            var entity = await repo.GetByIdAsync(attachmentId)
                ?? throw new KeyNotFoundException("Attachment not found.");

            if (!matchOwner(entity, ownerId))
                throw new UnauthorizedAccessException("Attachment does not belong to this owner.");

            if (entity.IsDeleted)
                return;

            var remotePath = entity.RemotePath;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedBy = user.UserName;

            repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(remotePath))
            {
                await _fTPClientService.DeleteFileAsync(remotePath);
            }
        }


        #endregion


        public Task<IReadOnlyList<AttachmentResponseDTO>> AddAsync(
            AttachmentContext context,
            int ownerId,
            IList<IFormFile> files,
            CancellationToken ct = default)
        {
            return context switch
            {
                AttachmentContext.Research => AddInternalAsync<ResearchAttachment>(
                    ownerId,
                    files,
                    buildRemotePath: userId => $"{userId}/{Guid.NewGuid()}",
                    setOwner: (a, id) => a.ResearchId = id,
                    ct),

                AttachmentContext.Thesis => AddInternalAsync<ThesesAttachment>(
                    ownerId,
                    files,
                    buildRemotePath: userId => $"{userId}/{Guid.NewGuid()}",
                    setOwner: (a, id) => a.ThesisId = id,
                    ct),

                _ => throw new ArgumentOutOfRangeException(nameof(context))
            };
        }

        public Task<AttachmentDownloadDTO> GetAsync(AttachmentContext context, int ownerId, Guid attachmentId, CancellationToken ct = default)
        {
            return context switch
            {
                AttachmentContext.Research => GetInternalAsync<ResearchAttachment>(
                    ownerId,
                    attachmentId,
                    matchOwner: (a, id) => a.ResearchId == id,
                    ct),

                AttachmentContext.Thesis => GetInternalAsync<ThesesAttachment>(
                    ownerId,
                    attachmentId,
                    matchOwner: (a, id) => a.ThesisId == id,
                    ct),

                _ => throw new ArgumentOutOfRangeException(nameof(context))
            };
        }

        public Task<AttachmentResponseDTO> UpdateAsync(AttachmentContext context, int ownerId, Guid attachmentId, IFormFile newFile, CancellationToken ct = default)
        {
            return context switch
            {
                AttachmentContext.Research => UpdateInternalAsync<ResearchAttachment>(
                    ownerId, attachmentId, newFile,
                    matchOwner: (a, id) => a.ResearchId == id,
                    getOwnerFolder: a => a.ResearchId.ToString(), 
                    ct),

                AttachmentContext.Thesis => UpdateInternalAsync<ThesesAttachment>(
                    ownerId, attachmentId, newFile,
                    matchOwner: (a, id) => a.ThesisId == id,
                    getOwnerFolder: a => a.ThesisId.ToString(),
                    ct),

                _ => throw new ArgumentOutOfRangeException(nameof(context))
            };
        }

        public Task DeleteAsync(AttachmentContext context, int ownerId, Guid attachmentId, CancellationToken ct = default)
        {
            return context switch
            {
                AttachmentContext.Research => DeleteInternalAsync<ResearchAttachment>(
                    ownerId,
                    attachmentId,
                    matchOwner: (a, id) => a.ResearchId == id,
                    ct),

                AttachmentContext.Thesis => DeleteInternalAsync<ThesesAttachment>(
                    ownerId,
                    attachmentId,
                    matchOwner: (a, id) => a.ThesisId == id,
                    ct),

                _ => throw new ArgumentOutOfRangeException(nameof(context))
            };
        }
    }
}
