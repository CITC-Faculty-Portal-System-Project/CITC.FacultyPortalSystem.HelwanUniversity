using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class ConferncesAndSeminarsCountSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsCountSpecification(SeminarsAndConferncesSpecificationParameters parameters, string facultyMemberEmail)
            : base(cas =>
                  (!cas.IsDeleted &&
                    cas.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   cas.Name.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   cas.OrganizingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   cas.Venue.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
