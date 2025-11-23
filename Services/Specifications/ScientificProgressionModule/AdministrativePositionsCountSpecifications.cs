using Domain.Entities.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AdministrativePositionsCountSpecifications : BaseSpecifications<AdministrativePositions, int>
    {
        public AdministrativePositionsCountSpecifications(AdministrativePositionsSpecificationParameters parameters)
            : base(ap =>
                  (!ap.IsDeleted &&
                    ap.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ap.Position.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
