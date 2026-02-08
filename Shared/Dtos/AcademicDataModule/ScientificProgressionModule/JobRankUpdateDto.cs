namespace Shared.Dtos.AcademicDataModule.ScientificProgressionModule
{
    public record JobRankUpdateDto
    {
        public Guid JobRankId { get; set; }
        public DateOnly DateOfJobRank { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
