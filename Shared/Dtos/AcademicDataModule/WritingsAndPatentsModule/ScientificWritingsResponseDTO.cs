using System.Linq;

namespace Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule
{
    public record ScientificWritingsResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public LookupItemDto AuthorRole { get; set; } = null!;
        public string ISBN { get; set; } = string.Empty;
        public string PublishingHouse { get; set; } = string.Empty;
        public DateOnly PublishingDate { get; set; }
        public string? Description { get; set; }
    }
}
