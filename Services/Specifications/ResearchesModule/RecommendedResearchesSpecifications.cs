using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedResearchesSpecifications : BaseSpecifications<Research, int>
    {
        public RecommendedResearchesSpecifications(RecommendedResearchesSpecificationParameters parameters, Guid facultyMemberId)
                : base(r =>
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
        {


            AddIncludeWithChain(q => q
                            .Include(r => r.Contributions!
                            .Where(c => c.MemberAcademicName != facultyMemberId.ToString()))
            );
            
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
            AddIncludes(r => r.Cites!);

        }

        public RecommendedResearchesSpecifications(int researchId , Guid facultyMemberId)
            : base(r => !r.IsDeleted &&
                    r.Id == researchId && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted &&

            r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)!.IsConfirmed == false)
        {
            AddIncludes(r => r.Contributions!);
        }


        public RecommendedResearchesSpecifications(string title)
            : base(r => !r.IsDeleted &&
                    r.Title == title) 
        {
            AddIncludes(r => r.Contributions!);
            AddIncludes(r => r.Cites!);
        }
    }
}
