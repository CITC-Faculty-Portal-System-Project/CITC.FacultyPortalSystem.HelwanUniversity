using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using Shared.Enums.ProjectsAndCommitteesModule;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedResearchesSpecifications : BaseSpecifications<Research, int>
    {
        public RecommendedResearchesSpecifications(RecommendedResearchesSpecificationParameters parameters, Guid facultyMemberId)
                : base(r => !r.IsDeleted &&
                    r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)!.IsConfirmed == false 
                    && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   r.Title.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.JournalOrConfernce.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.PubYear.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase)))
        {


            AddIncludes(r => r.Contributions!);
            switch (parameters.Sort)
            {
                case ResearchesSortingOptions.TitleASC:
                    AddOrderBy(r => r.Title);
                    break;
                case ResearchesSortingOptions.TitleDESC:
                    AddOrderByDescending(r => r.Title);
                    break;
                case ResearchesSortingOptions.JournalASC:
                    AddOrderBy(r => r.JournalOrConfernce);
                    break;
                case ResearchesSortingOptions.JournalDESC:
                    AddOrderByDescending(r => r.JournalOrConfernce);
                    break;
                case ResearchesSortingOptions.PubYearASC:
                    AddOrderBy(r => Convert.ToInt32(r.PubYear));
                    break;
                case ResearchesSortingOptions.PubYearDESC:
                    AddOrderByDescending(r => Convert.ToInt32(r.PubYear));
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public RecommendedResearchesSpecifications(int researchId , Guid facultyMemberId)
            : base(r => !r.IsDeleted &&
                    r.Id == researchId && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted &&

            r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)!.IsConfirmed == false)
        {
            AddIncludeWithChain(r => r.Include(r => r.Contributions!)
                                     .ThenInclude(r => r.Contributor));
        }
    }
}
