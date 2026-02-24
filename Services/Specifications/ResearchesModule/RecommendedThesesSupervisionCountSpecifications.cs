using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedThesesSupervisionCountSpecifications : BaseSpecifications<Thesis, int>
    {
        public RecommendedThesesSupervisionCountSpecifications
            (ThesesSpecificationParameters parameters , Guid memberId) 
            :base(rth => !rth.IsDeleted &&
                rth.ComitteeMembers!.SingleOrDefault(cm => cm.MemberId == memberId)!
                  .isConfirmed == false &&
                  
            (string.IsNullOrEmpty(parameters.Search)
                    || rth.Title.Contains(parameters.Search)
                    || rth.Link!.Contains(parameters.Search)
            )
            )
        {
        }
    }
}
