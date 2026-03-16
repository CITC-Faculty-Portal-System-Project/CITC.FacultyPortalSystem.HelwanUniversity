using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Shared.Enums.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Specifications.AcademicDataModule.WritingsAndPatentsModule
{
    internal class PatentsSpecifications : BaseSpecifications<Patents, int>
    {
        public PatentsSpecifications(PatentsSpecificationParameters parameters, string facultyMemberEmail)
            : base(p =>
                  !p.IsDeleted &&
                    p.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.LocalOrInternational == null || !parameters.LocalOrInternational.Any() ||
                   parameters.LocalOrInternational.Select(e => (Domain.Enums.LocalOrInternational)e)
                   .Contains(p.LocalOrInternational)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   p.NameOfPatent.Contains(parameters.Search) ||
                   p.AccreditingAuthorityOrCountry.Contains(parameters.Search))
            )
        {
            switch (parameters.Sort)
            {
                case PatentsSortingOptions.ApplyingDateAsc:
                    AddOrderBy(p => p.ApplyingDate);
                    break;
                case PatentsSortingOptions.ApplyingDateDesc:
                    AddOrderByDescending(p => p.ApplyingDate);
                    break;
                case PatentsSortingOptions.AccreditationDateAsc:
                    AddOrderBy(p => p.AccreditationDate!);
                    break;
                case PatentsSortingOptions.AccreditationDateDesc:
                    AddOrderByDescending(p => p.AccreditationDate!);
                    break;
                case PatentsSortingOptions.NameAsc:
                    AddOrderBy(p => p.NameOfPatent);
                    break;
                case PatentsSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.NameOfPatent);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
            AddIncludes(p => p.Attachments!);
        }
        public PatentsSpecifications(int id) : base(p => p.Id == id && !p.IsDeleted)
        {
            AddIncludes(p => p.Attachments!);

        }
    }
}
