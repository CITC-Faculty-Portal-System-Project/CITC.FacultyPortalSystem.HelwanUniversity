using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedResearchesSpecifications : BaseSpecifications<Research, int>
    {
        public RecommendedResearchesSpecifications(ResearchSpecificationParameters parameters, Guid facultyMemberId)
                : base(BuildCriteria(parameters , facultyMemberId))
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
                    AddOrderBy(r => r.PubYear!.All(char.IsDigit) ? int.Parse(r.PubYear!) : int.MaxValue);
                    break;
                case ResearchesSortingOptions.PubYearDESC:
                    AddOrderByDescending(r => r.PubYear!.All(char.IsDigit) ? int.Parse(r.PubYear!) : int.MaxValue);
                    break;
                case ResearchesSortingOptions.CitesASC:
                    AddOrderBy(r => r.NoOfCititations!);
                    break;
                case ResearchesSortingOptions.CitesDESC:
                    AddOrderByDescending(r => r.NoOfCititations!);

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


        private static Expression<Func<Research, bool>> BuildCriteria(
      ResearchSpecificationParameters parameters,
      Guid facultyMemberId)
        {
            Domain.Enums.PublisherType? mappedPublisherType = null;
            if (parameters.PublisherType.HasValue)
            {
                mappedPublisherType = Enum.Parse<Domain.Enums.PublisherType>(
                    parameters.PublisherType.Value.ToString(),
                    ignoreCase: true);
            }

            Domain.Enums.ResearchSource? mappedSource = null;
            if (parameters.Source.HasValue)
            {
                mappedSource = Enum.Parse<Domain.Enums.ResearchSource>(
                    parameters.Source.Value.ToString(),
                    ignoreCase: true);
            }

            Domain.Enums.ResearchDerivedFrom? mappedDrivedFrom = null;
            if (parameters.DerivedFrom.HasValue)
            {
                mappedDrivedFrom = Enum.Parse<Domain.Enums.ResearchDerivedFrom>(
                    parameters.DerivedFrom.Value.ToString(),
                    ignoreCase: true);
            }

            Domain.Enums.PublicationType? mappedPublicationType = null;
            if (parameters.PublicationType.HasValue)
            {
                mappedPublicationType = Enum.Parse<Domain.Enums.PublicationType>(
                    parameters.PublicationType.Value.ToString(),
                    ignoreCase: true);
            }

            return r =>
                !r.IsDeleted
                &&
                r.Contributions!.Any(c =>
                    !c.IsDeleted &&
                    c.ContributorId == facultyMemberId &&
                    !c.IsConfirmed) 
                
                && (!mappedPublisherType.HasValue || r.PublisherType == mappedPublisherType.Value)

                && (!mappedPublicationType.HasValue || r.PublicationType == mappedPublicationType.Value)

                && (!mappedSource.HasValue || r.Source == mappedSource.Value)

                && (!mappedDrivedFrom.HasValue || r.ResearchDerivedFrom == mappedDrivedFrom.Value)

                &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || r.Title.Contains(parameters.Search)
                    || r.JournalOrConfernce.Contains(parameters.Search)
                    || (r.PubYear != null && r.PubYear.Contains(parameters.Search)));
        }
    }
}
