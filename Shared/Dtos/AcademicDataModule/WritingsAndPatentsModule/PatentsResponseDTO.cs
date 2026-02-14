using Shared.Enums.AcademicDataModule.WritingsAndPatentsModule;

namespace Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule
{
    public record PatentsResponseDTO
    {
        public int Id { get; set; }
        public LocalOrInternational LocalOrInternational { get; set; }
        public string NameOfPatent { get; set; } = string.Empty;
        public string AccreditingAuthorityOrCountry { get; set; } = string.Empty;
        public DateOnly ApplyingDate { get; set; }
        public DateOnly? AccreditationDate { get; set; }
        public string? Description { get; set; }
    }
}
