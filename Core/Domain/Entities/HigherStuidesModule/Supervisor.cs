namespace Domain.Entities.HigherStuidesModule
{
    public class Supervisor : BaseEntity<int>
    {
        public SupervisorRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid JobLevelId { get; set; }
        public Lookup? JobLevel { get; set; }
        public string Authority { get; set; } = string.Empty;
        public ICollection<SupervisorThesesSupervising>? Theses { get; set; }
    }
}