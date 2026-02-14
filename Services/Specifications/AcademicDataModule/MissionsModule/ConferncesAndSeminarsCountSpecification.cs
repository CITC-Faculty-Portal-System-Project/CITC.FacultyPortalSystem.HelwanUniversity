using Domain.Entities.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Specifications.AcademicDataModule.MissionsModule
{
    internal class ConferncesAndSeminarsCountSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsCountSpecification(SeminarsAndConferncesSpecificationParameters parameters, string facultyMemberEmail)
            : base(cas =>
                  !cas.IsDeleted &&
                    cas.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.LocalOrInternational == null || !parameters.LocalOrInternational.Any() ||
                   parameters.LocalOrInternational.Select(e => (Domain.Enums.LocalOrInternational)e)
                   .Contains(cas.LocalOrInternational)) &&
                  (parameters.ConferenceOrSeminar == null || !parameters.ConferenceOrSeminar.Any() ||
                   parameters.ConferenceOrSeminar.Select(e => (Domain.Enums.ConferenceOrSeminar)e)
                   .Contains(cas.Type)) &&
                  (parameters.RoleOfParticipationIds == null || !parameters.RoleOfParticipationIds.Any() ||
                   parameters.RoleOfParticipationIds.Contains(cas.RoleOfParticipationId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   cas.Name.Contains(parameters.Search) ||
                   cas.OrganizingAuthority.Contains(parameters.Search) ||
                   cas.Venue.Contains(parameters.Search))
            )
        {

        }
    }
}
