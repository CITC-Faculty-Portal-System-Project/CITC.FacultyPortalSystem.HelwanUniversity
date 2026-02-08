using Domain.Entities.Attachments;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AttachmentsModule;

namespace Services.MappingProfiles
{
    public class AttachmentsMappingProfile : Profile
    {
        public AttachmentsMappingProfile() {

            CreateMap<AttachmentReference , AttachmentReferenceDTO>();
            CreateMap<AttachmentReferenceDTO, AttachmentReference>();
            CreateMap<AttachmentReference, AttachmentResponseDTO>();
            CreateMap<AttachmentReference, AttachmentReadDTO>()
                     .ForMember(d => d.FacultyMembersCount,
                     o => o.MapFrom(s => s.FacultyMembers!.Count));

            CreateMap<AttachmentReadDTO, AttachmentReferenceDTO>();
            CreateMap<AttachmentReadDTO, AttachmentReference>();

            CreateMap<AttachmentReference, ConferencesAndSeminarsAttachmentsReadDTO>();


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
