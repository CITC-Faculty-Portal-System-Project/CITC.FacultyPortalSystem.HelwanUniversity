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
            : base(r => !r.IsDeleted &&
                    r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)!.IsConfirmed == true && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   r.Title.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.JournalOrConfernce.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.PubYear.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase)))
        {
        }
    }
}
