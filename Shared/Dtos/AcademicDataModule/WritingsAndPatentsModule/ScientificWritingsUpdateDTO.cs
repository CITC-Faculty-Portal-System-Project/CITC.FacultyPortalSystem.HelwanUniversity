namespace Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule
{
    public record ScientificWritingsUpdateDTO
    {
        public string Title { get; set; } = string.Empty;
        public Guid AuthorRoleId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string PublishingHouse { get; set; } = string.Empty;
        public DateOnly PublishingDate { get; set; }
        public string? Description { get; set; }
    }
}
