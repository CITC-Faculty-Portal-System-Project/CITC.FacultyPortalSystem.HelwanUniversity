using Domain.Entities.AcademicDataModule.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ResearcherInterestSpecification : BaseSpecifications<ScientificInterest, int>
    {
        public ResearcherInterestSpecification
            (string interestName) 
            : base(ri => string.Equals(ri.Name , interestName) && !ri.IsDeleted)
        {
        }
    }
}
