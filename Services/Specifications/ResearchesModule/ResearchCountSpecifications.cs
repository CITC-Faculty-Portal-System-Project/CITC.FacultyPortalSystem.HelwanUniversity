using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ResearchCountSpecifications : BaseSpecifications<Research, int>
    {
        public ResearchCountSpecifications
            (ResearchSpecificationParameters parameters , Guid facultyMemberId) 
            : base(BuildCriteria(parameters, facultyMemberId))
        {
        }

        public ResearchCountSpecifications(Guid facultyMemberId)
        : base(r => !r.IsDeleted &&
           r.Contributions.Any(c =>
                c.ContributorId == facultyMemberId &&
                c.IsConfirmed == true &&
                !c.IsDeleted))
        {
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
                    c.IsConfirmed)
                && (!mappedPublisherType.HasValue || r.PublisherType == mappedPublisherType.Value)

                && (!mappedPublicationType.HasValue || r.PublicationType == mappedPublicationType.Value)

                && (!mappedSource.HasValue || r.Source == mappedSource.Value)

                && (!mappedDrivedFrom.HasValue || r.ResearchDerivedFrom == mappedDrivedFrom.Value)

                &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || r.Title.Contains(parameters.Search)
                    || r.JournalOrConfernce.Contains(parameters.Search));
        }
    }
}
