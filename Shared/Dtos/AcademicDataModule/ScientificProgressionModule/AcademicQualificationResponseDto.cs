using Shared.Dtos.AttachmentsModule;
using System.Linq;

namespace Shared.Dtos.AcademicDataModule.ScientificProgressionModule
{
    public record AcademicQualificationResponseDto
    {
        public int Id { get; set; }
        public LookupItemDto Qualification { get; set; } = null!;
        public string Specialization { get; set; } = string.Empty;
        public LookupItemDto Grade { get; set; } = null!;
        public LookupItemDto DispatchType { get; set; } = null!;
        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly DateOfObtainingTheQualification { get; set; }
        public ICollection<AttachmentResponseDTO>? Attachments { get; set; }

    }
}
