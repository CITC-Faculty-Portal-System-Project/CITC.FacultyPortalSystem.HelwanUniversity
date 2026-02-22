using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedResearchesCountSpecifications : BaseSpecifications<Research , int>
    {
        public RecommendedResearchesCountSpecifications(RecommendedResearchesSpecificationParameters parameters , Guid facultyMemberId)
            :base(r =>
                !r.IsDeleted
                &&
                r.Contributions!.Any(c =>
                    !c.IsDeleted &&
                    c.ContributorId == facultyMemberId &&
                    !c.IsConfirmed)
                &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || r.Title.Contains(parameters.Search)
                    || r.JournalOrConfernce.Contains(parameters.Search)
                    || (r.PubYear != null && r.PubYear.Contains(parameters.Search))
                ))
        { }
                         
            }
    }

