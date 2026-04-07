using Domain.Entities.EntitesAttachments;
using Shared.Dtos.AttachmentsModule;

namespace Services.MappingProfiles
{
    public class AttachmentsMappingProfile : Profile
    {
        public AttachmentsMappingProfile() {


            CreateMap<AttachmentReferenceDTO, ResearchAttachment>()
                .IncludeBase<AttachmentReferenceDTO, BaseAttachmentEntity>()
                    .ForMember(d => d.ResearchId, opt => opt.Ignore());


            CreateMap<AttachmentReferenceDTO, ThesesAttachment>()
                .IncludeBase<AttachmentReferenceDTO, BaseAttachmentEntity>()
                .ForMember(d => d.ThesisId, opt => opt.Ignore());

            CreateMap<AttachmentReferenceDTO, ConversationAttachment>()
              .IncludeBase<AttachmentReferenceDTO, BaseAttachmentEntity>()
              .ForMember(d => d.ConversationId, opt => opt.Ignore());


            CreateMap<ResearchAttachment, AttachmentResponseDTO>();
            CreateMap<ConversationAttachment, AttachmentResponseDTO>();
            CreateMap<ThesesAttachment, AttachmentResponseDTO>();

            CreateMap<BaseAttachmentEntity, AttachmentReferenceDTO>();

            CreateMap<ResearchAttachment, AttachmentReferenceDTO>()
                .IncludeBase<BaseAttachmentEntity, AttachmentReferenceDTO>();

            CreateMap<ThesesAttachment, AttachmentReferenceDTO>()
                .IncludeBase<BaseAttachmentEntity, AttachmentReferenceDTO>();

            CreateMap<AttachmentReferenceDTO, BaseAttachmentEntity>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<AttachmentUploadDTO, AttachmentReferenceDTO>()
            .ForMember(d => d.FileName, o => o.MapFrom(s => s.File.FileName))
            .ForMember(d => d.ContentType, o => o.MapFrom(s => s.File.ContentType))
            .ForMember(d => d.Size, o => o.MapFrom(s => s.File.Length))

            .ForMember(d => d.Hash, o => o.MapFrom(s => s.Encrypted.Hash))
            .ForMember(d => d.Nonce, o => o.MapFrom(s => s.Encrypted.Nonce))
            .ForMember(d => d.Tag, o => o.MapFrom(s => s.Encrypted.Tag))
            .ForMember(d => d.KeyRef, o => o.MapFrom(s => s.Encrypted.KeyRef))
            .ForMember(d => d.WrappedDek, o => o.MapFrom(s => s.Encrypted.WrappedDek))

            .ForMember(d => d.StorageProvider, o => o.MapFrom(_ => "FTP"))
            .ForMember(d => d.RemotePath, o => o.MapFrom(s => s.RemotePath + s.File.FileName));
        }
    }
}
