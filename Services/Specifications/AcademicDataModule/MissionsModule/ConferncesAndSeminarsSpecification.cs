using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.SpecificationParameters.AcademicDataModule.MissionsModule;

namespace Services.Specifications.AcademicDataModule.MissionsModule
{
    internal class ConferncesAndSeminarsSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsSpecification(SeminarsAndConferncesSpecificationParameters parameters, string facultyMemberEmail)
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
            AddIncludes(cas => cas.RoleOfParticipation);
            AddIncludes(cas => cas.Attachments!);
           
            switch (parameters.Sort)
            {
                case SeminarsAndConferencesSortingOptions.NameAsc:
                    AddOrderBy(cas => cas.Name);
                    break;
                case SeminarsAndConferencesSortingOptions.NameDesc:
                    AddOrderByDescending(cas => cas.Name);
                    break;
                case SeminarsAndConferencesSortingOptions.DateAsc:
                    AddOrderBy(cas => cas.StartDate);
                    break;
                case SeminarsAndConferencesSortingOptions.DateDesc:
                    AddOrderByDescending(cas => cas.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ConferncesAndSeminarsSpecification(int id) : base(cas => !cas.IsDeleted && cas.Id == id)
        {
            AddIncludes(cas => cas.RoleOfParticipation);
            AddIncludes(cas => cas.Attachments!);

        }

    }
}
