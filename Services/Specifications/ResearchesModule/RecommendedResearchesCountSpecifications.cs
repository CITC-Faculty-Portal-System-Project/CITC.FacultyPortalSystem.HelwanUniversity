using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedResearchesCountSpecifications : BaseSpecifications<Research , int>
    {
        public RecommendedResearchesCountSpecifications(RecommendedResearchesSpecificationParameters parameters , Guid facultyMemberId)
            :base(r =>
                  (!r.IsDeleted &&
                    r.Contributions!.Any(c => c.ContributorId == facultyMemberId)) 
                    && r.IsConfirmed == false &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   r.Title.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.JournalOrConfernce.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.PubYear.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))){
                         
            }
    }
}
