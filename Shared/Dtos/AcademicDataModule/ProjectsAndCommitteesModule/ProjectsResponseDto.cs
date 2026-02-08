using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule
{
    public record ProjectsResponseDto
    {
        public int Id { get; set; }
        public LocalOrInternational LocalOrInternational { get; set; }
        public string NameOfProject { get; set; } = string.Empty;
        public LookupItemDto TypeOfProject { get; set; } = null!;
        public LookupItemDto ParticipationRole { get; set; } = null!;
        public string FinancingAuthority { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
