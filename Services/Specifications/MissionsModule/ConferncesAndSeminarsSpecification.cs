using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class ConferncesAndSeminarsSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsSpecification(SeminarsAndConferncesSpecificationParameters parameters, string facultyMemberEmail)
            : base(cas =>
                  (!cas.IsDeleted &&
                    cas.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   cas.Name.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   cas.OrganizingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   cas.Venue.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {
            AddIncludes(cas => cas.RoleOfParticipation);
           
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

        }

    }
}
