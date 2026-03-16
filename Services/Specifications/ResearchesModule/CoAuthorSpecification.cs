using Domain.Entities.AcademicDataModule.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class CoAuthorSpecification : BaseSpecifications<CoAuthor, int>
    {
        public CoAuthorSpecification
            (string profileLink) : base(ca => !ca.IsDeleted && ca.ScholarProfileLink == profileLink)
        {
        }
    }
}
