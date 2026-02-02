using Domain.Entities.AcademicDataModule.HigherStuidesModule;

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
