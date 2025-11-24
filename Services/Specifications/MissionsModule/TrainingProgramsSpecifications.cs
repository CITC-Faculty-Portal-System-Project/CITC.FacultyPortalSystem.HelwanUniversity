using Domain.Entities.MissionsModule;
using Shared.Enums.MissionsModule;
using Shared.SpecificationParameters.MissionsModule;

namespace Services.Specifications.MissionsModule
{
    internal class TrainingProgramsSpecifications : BaseSpecifications<TrainingPrograms, int>
    {
        public TrainingProgramsSpecifications(TrainingProgramsSpecificationParameters parameters) 
            : base(tp =>
                  (!tp.IsDeleted &&
                    tp.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   tp.TrainingProgramName.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.OrganizingAuthority.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   tp.Venue.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

            switch (parameters.Sort)
            {
                case TrainingProgramsSortingOptions.NameAsc:
                    AddOrderBy(tp => tp.TrainingProgramName);
                    break;
                case TrainingProgramsSortingOptions.NameDesc:
                    AddOrderByDescending(tp => tp.TrainingProgramName);
                    break;
                case TrainingProgramsSortingOptions.DateAsc:
                    AddOrderBy(tp => tp.StartDate);
                    break;
                case TrainingProgramsSortingOptions.DateDesc:
                    AddOrderByDescending(tp => tp.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public TrainingProgramsSpecifications(int id) : base(tp => !tp.IsDeleted && tp.Id == id)
        {

        }
    }
}
