using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Specifications.AcademicDataModule.WritingsAndPatentsModule
{
    internal class PatentsCountSpecifications : BaseSpecifications<Patents, int>
    {
        public PatentsCountSpecifications(PatentsSpecificationParameters parameters, string facultyMemberEmail)
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
        }
    }
}
