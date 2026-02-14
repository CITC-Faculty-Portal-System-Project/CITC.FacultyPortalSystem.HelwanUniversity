using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Specifications.AcademicDataModule.ScientificProgressionModule
{
    internal class AdministrativePositionsCountSpecifications : BaseSpecifications<AdministrativePositions, int>
    {
        public AdministrativePositionsCountSpecifications(AdministrativePositionsSpecificationParameters parameters, string facultyMemberEmail)
            : base(ap =>
                  !ap.IsDeleted &&
                    ap.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   ap.Position.Contains(parameters.Search))
            )
        {

        }
    }
}
