using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AdministrativePositionsCountSpecifications : BaseSpecifications<AdministrativePositions, int>
    {
        public AdministrativePositionsCountSpecifications(AdministrativePositionsSpecificationParameters parameters, string facultyMemberEmail)
            : base(ap =>
                  (!ap.IsDeleted &&
                    ap.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ap.Position.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
