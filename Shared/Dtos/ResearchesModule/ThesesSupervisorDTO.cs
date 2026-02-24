using Shared.Enums.AcademicDataModule.HigherStudiesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesSupervisorDTO
    {
        public Guid? MemberId { get; set; }
        public bool isConfirmed { get; set; }
        public SupervisorRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid JobLevelId { get; set; }
        public string Authority { get; set; } = string.Empty;
        public int? ThesesId { get; set; }
    }
}
