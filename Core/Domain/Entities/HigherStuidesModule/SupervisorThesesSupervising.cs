namespace Domain.Entities.HigherStuidesModule
{
    public class SupervisorThesesSupervising : BaseEntity<int>
    {
        public int SupervisorId { get; set; }
        public Supervisor? Supervisor { get; set; }

        public int ThesesId { get; set; }
        public Thesis? Theses { get; set; }
    }
}
