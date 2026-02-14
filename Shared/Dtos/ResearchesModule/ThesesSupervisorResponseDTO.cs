using Shared.Enums.HigherStudiesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesSupervisorResponseDTO
    {
        public int Id { get; set; }
        public SupervisorRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid JobLevelId { get; set; }
        public LookupItemDto? JobLevel { get; set; }
        public string Authority { get; set; } = string.Empty;

    }
}
