using Shared.Dtos.AttachmentsModule;

namespace Shared.Dtos.FacultyMemberDataModule
{
    public record PersonalDataResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string NationalNumber {  get; set; } = string.Empty;

        public LookupItemDto Title { get; set; } = null!;
        public LookupItemDto Gender { get; set; } = null!;
        public LookupItemDto MaritalStatus { get; set; } = null!;
        public LookupItemDto University { get; set; } = null!;
        public LookupItemDto Department { get; set; } = null!;
        public LookupItemDto Authority { get; set; } = null!;
        public LookupItemDto Field { get; set; } = null!;

        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? GeneralSpecialization { get; set; }
        public string? AccurateSpecialization { get; set; }
        public string? NameInComposition { get; set; }
        public string? CompositionTopics { get; set; }
        public AttachmentResponseDTO? ProfilePicture { get; set; }

    }
}
