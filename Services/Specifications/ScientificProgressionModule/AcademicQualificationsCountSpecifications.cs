using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AcademicQualificationsCountSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsCountSpecifications(AcademicQualificationsSpecificationParamters parameters, string facultyMemberEmail)
            : base(aq =>
                  (!aq.IsDeleted &&
                    aq.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   aq.Qualification.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   aq.Qualification.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        { 
        }
    }
}
