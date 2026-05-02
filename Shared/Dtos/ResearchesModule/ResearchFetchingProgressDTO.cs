namespace Shared.Dtos.ResearchesModule
{
    public record ResearchFetchingProgressDTO
    {
        public string Status { get; set; } = string.Empty;
        public int Current { get; set; }
        public int Total { get; set; }
        public int Percentage { get; set; }
        public string Started_At { get; set; } = string.Empty;
        public string Estimated_Finish { get; set; } = string.Empty;
        public string Finished_At { get; set; } = string.Empty;
    }
}
