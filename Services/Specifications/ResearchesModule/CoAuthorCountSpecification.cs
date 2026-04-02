using Domain.Entities.AcademicDataModule.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class CoAuthorCountSpecification : BaseSpecifications<CoAuthor, int>
    {
        public CoAuthorCountSpecification
            (string profileLink) 
            :base(ca => !ca.IsDeleted && ca.ScholarProfileLink == profileLink)
        {
        }
    }
}
