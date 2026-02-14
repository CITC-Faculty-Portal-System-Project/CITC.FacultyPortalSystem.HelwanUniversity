namespace Shared.Dtos.AcademicDataModule.ScientificProgressionModule
{
    public record JobRankCreateDto
    {
        public Guid JobRankId { get; set; } 
        public DateOnly DateOfJobRank { get; set; }
        public string Notes { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }
    }
}
