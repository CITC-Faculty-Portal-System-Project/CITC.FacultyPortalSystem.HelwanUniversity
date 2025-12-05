using Domain.Entities.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AdministrativePositionsSpecifications : BaseSpecifications<AdministrativePositions, int>
    {
        public AdministrativePositionsSpecifications(AdministrativePositionsSpecificationParameters parameters, string facultyMemberEmail) 
            : base(ap =>
                  (!ap.IsDeleted &&
                    ap.FacultyMember!.Email == facultyMemberEmail) &&
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

        public AdministrativePositionsSpecifications(AdminstrativePostionsFetchingDTO dTO) 
            : base(ap => !ap.IsDeleted 
            && ap.FacultyMember.NationalNumber == dTO.NationalNumber && ap.EndDate == DateOnly.Parse(dTO.EndDate)
            && ap.StartDate == DateOnly.Parse(dTO.StartDate) && ap.Position == dTO.Name)
        {
            AddIncludes(ap => ap.FacultyMember);
        }
    }
}
