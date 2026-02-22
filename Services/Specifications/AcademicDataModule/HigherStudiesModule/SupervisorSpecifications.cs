using Domain.Entities.AcademicDataModule.HigherStuidesModule;

namespace Services.Specifications.AcademicDataModule.HigherStudiesModule
{
    internal class SupervisorSpecifications : BaseSpecifications<ThesisComittee , int>
    {
        public SupervisorSpecifications(Thesis theses) :
            base(s => s.Theses == theses)
        {
            AddIncludes(s => s.Theses);
        }
    }
}
