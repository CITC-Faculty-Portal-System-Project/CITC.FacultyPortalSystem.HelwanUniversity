using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule
{
    public record ProjectUpdateDto
    {
        public LocalOrInternational LocalOrInternational { get; set; }
        public string NameOfProject { get; set; } = string.Empty;
        public Guid TypeOfProjectId { get; set; }
        public Guid ParticipationRoleId { get; set; }
        public string FinancingAuthority { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
