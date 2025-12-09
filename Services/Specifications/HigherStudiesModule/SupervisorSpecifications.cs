using Domain.Entities.HigherStuidesModule;

namespace Services.Specifications.HigherStudiesModule
{
    internal class SupervisorSpecifications : BaseSpecifications<Supervisor , int>
    {
        public SupervisorSpecifications(Thesis theses) :
            base(s => s.Theses == theses)
        {
            AddIncludes(s => s.Theses);
        }
    }
}
