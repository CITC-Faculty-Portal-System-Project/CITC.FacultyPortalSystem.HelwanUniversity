namespace Shared.Dtos.ScientificProgressionModule
{
    public record JobRankResponseDto
    {
        public LookupItemDto JobRank { get; set; } = null!;
        public DateOnly DateOfJobRank { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
