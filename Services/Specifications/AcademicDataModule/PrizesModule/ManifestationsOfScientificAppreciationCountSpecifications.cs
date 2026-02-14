using Domain.Entities.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Specifications.AcademicDataModule.PrizesModule
{
    internal class ManifestationsOfScientificAppreciationCountSpecifications : BaseSpecifications<ManifestationsOfScientificAppreciation, int>
    {
        public ManifestationsOfScientificAppreciationCountSpecifications(ManifestationsOfScientificAppreciationSpecificationParameters parameters, string facultyMemberEmail)
            : base(msa =>
                  !msa.IsDeleted &&
                    msa.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   msa.TitleOfAppreciation.Contains(parameters.Search) ||
                   msa.IssuingAuthority.Contains(parameters.Search))
            )
        {

        }
    }
}
