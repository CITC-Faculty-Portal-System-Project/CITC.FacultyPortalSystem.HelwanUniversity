using Shared.Dtos.AttachmentsModule;

namespace Shared.Dtos.AcademicDataModule.PrizesModule
{
    public record ManifestationsOfScientificAppreciationResponseDTO
    {
        public int Id { get; set; }
        public string TitleOfAppreciation { get; set; } = string.Empty;
        public string IssuingAuthority { get; set; } = string.Empty;
        public DateOnly DateOfAppreciation { get; set; }
        public string? Description { get; set; }
        public ICollection<AttachmentResponseDTO>? Attachments { get; set; }

    }
}
