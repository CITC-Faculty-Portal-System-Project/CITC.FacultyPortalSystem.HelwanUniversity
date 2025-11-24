namespace Shared.Dtos.ScientificProgressionModule
{
    public record AdministrativePositionDto
    {
        public int Id { get; set; }
        public string Position { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}
