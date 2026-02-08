using Shared.Enums.HigherStudiesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesSupervisorDTO
    {
        public SupervisorRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid JobLevelId { get; set; }
        public LookupItemDto? JobLevel { get; set; }
        public string Authority { get; set; } = string.Empty;

    }
}
