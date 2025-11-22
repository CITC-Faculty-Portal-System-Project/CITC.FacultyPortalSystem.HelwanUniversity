using Domain.Entities.ScientificProgressionModule;
using Shared.Enums.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AdministrativePositionsSpecifications : BaseSpecifications<AdministrativePositions, int>
    {
        public AdministrativePositionsSpecifications(AdministrativePositionsSpecificationParameters parameters) 
            : base(ap =>
                  (!ap.IsDeleted &&
                    ap.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ap.Position.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

            switch (parameters.Sort)
            {
                case AdministrativePositionsSortingOptions.DateAsc:
                    AddOrderBy(ap => ap.StartDate);
                    break;
                case AdministrativePositionsSortingOptions.DateDesc:
                    AddOrderByDescending(ap => ap.StartDate);
                    break;
                case AdministrativePositionsSortingOptions.NameAsc:
                    AddOrderBy(ap => ap.Position);
                    break;
                case AdministrativePositionsSortingOptions.NameDesc:
                    AddOrderByDescending(ap => ap.Position);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public AdministrativePositionsSpecifications(int id) : base(ap => !ap.IsDeleted && ap.Id == id)
        {

        }
    }
}
