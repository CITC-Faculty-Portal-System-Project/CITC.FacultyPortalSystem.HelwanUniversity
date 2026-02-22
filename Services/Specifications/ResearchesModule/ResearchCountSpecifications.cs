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
            : base(r =>
                !r.IsDeleted
                &&
                r.Contributions!.Any(c =>
                    !c.IsDeleted &&
                    c.ContributorId == facultyMemberId &&
                    c.IsConfirmed)
                &&
                (
                    string.IsNullOrEmpty(parameters.Search)
                    || r.Title.Contains(parameters.Search)
                    || r.JournalOrConfernce.Contains(parameters.Search)
                    || (r.PubYear != null && r.PubYear.Contains(parameters.Search))
                ))
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
    }
}
