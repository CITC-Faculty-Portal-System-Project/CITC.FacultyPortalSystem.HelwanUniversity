using Shared.Enums.HigherStudiesModule;
namespace Shared.Dtos.HigherStudiesModule
{
    public record SupervisorCreateDTO
    {
        public SupervisorRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid JobLevelId { get; set; }
        public string Authority { get; set; } = string.Empty;
        public int ThesesId { get; set; }

    }
}
