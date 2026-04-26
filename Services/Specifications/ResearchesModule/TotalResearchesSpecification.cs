using Domain.Entities.AcademicDataModule.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class TotalResearchesSpecification : BaseSpecifications<Research, int>
    {
        public TotalResearchesSpecification
            (): base(r => !r.IsDeleted && r.Contributions!.Any(c => c.IsConfirmed == true))
        {
        }
    }
}
