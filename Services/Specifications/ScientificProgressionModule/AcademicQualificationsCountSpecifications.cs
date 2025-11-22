using Domain.Entities.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AcademicQualificationsCountSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsCountSpecifications(AcademicQualificationsSpecificationParamters parameters)
            : base(aq =>
                  (!aq.IsDeleted &&
                    aq.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   aq.Qualification.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   aq.Qualification.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        { 
        }
    }
}
