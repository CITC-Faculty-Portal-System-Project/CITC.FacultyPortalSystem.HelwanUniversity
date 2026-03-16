namespace Shared.Dtos.CVGenerationModule.Missions
{
    public record CVTrainingProgramsDTO
    {
        public int Id { get; set; }
        public string? TrainingProgramName { get; set; } 
        public string? Venue { get; set; } 
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
