using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record TrainingProgramsCreateDto
    {
        public TrainingProgramType Type { get; set; }
        public TrainingProgramParticipationType ParticipationType { get; set; }
        public string TrainingProgramName { get; set; } = string.Empty;
        public string OrganizingAuthority { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }
    }
}
