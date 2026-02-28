using Domain.Entities.AcademicDataModule.PrizesModule;
using Shared.Enums.AcademicDataModule.PrizesModule;
using Shared.SpecificationParameters.AcademicDataModule.PrizesModule;

namespace Services.Specifications.AcademicDataModule.PrizesModule
{
    internal class ManifestationsOfScientificAppreciationSpecifications : BaseSpecifications<ManifestationsOfScientificAppreciation, int>
    {
        public ManifestationsOfScientificAppreciationSpecifications(ManifestationsOfScientificAppreciationSpecificationParameters parameters, string facultyMemberEmail)
            : base(msa =>
                  !msa.IsDeleted &&
                    msa.FacultyMember!.Email == facultyMemberEmail &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   msa.TitleOfAppreciation.Contains(parameters.Search) ||
                   msa.IssuingAuthority.Contains(parameters.Search))
            )
        {
            switch (parameters.Sort)
            {
                case ManifestationsOfScientificAppreciationSortingOptions.DateAsc:
                    AddOrderBy(msa => msa.DateOfAppreciation);
                    break;
                case ManifestationsOfScientificAppreciationSortingOptions.DateDesc:
                    AddOrderByDescending(msa => msa.DateOfAppreciation);
                    break;
                case ManifestationsOfScientificAppreciationSortingOptions.NameAsc:
                    AddOrderBy(msa => msa.TitleOfAppreciation);
                    break;
                case ManifestationsOfScientificAppreciationSortingOptions.NameDesc:
                    AddOrderByDescending(msa => msa.TitleOfAppreciation);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
            AddIncludes(m => m.Attachments!);

        }

        public ManifestationsOfScientificAppreciationSpecifications(int id) : base(msa => !msa.IsDeleted && msa.Id == id)
        {
            AddIncludes(m => m.Attachments!);

        }
    }
}
