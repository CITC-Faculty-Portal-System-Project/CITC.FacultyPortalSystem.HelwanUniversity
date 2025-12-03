using Domain.Entities.ScientificProgressionModule;
using Shared.Enums.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AcademicQualificationsSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsSpecifications(AcademicQualificationsSpecificationParamters parameters, string facultyMemberEmail)
            : base(aq => 
                  (!aq.IsDeleted &&
                    aq.FacultyMember!.Email == facultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) || 
                   aq.Qualification.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   aq.Qualification.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            ) 
        {
            AddIncludes(aq => aq.Qualification);
            AddIncludes(aq => aq.Grade);
            AddIncludes(aq => aq.DispatchType);

            switch (parameters.Sort)
            {
                case AcademicQualificationsSortingOptions.DateAsc:
                    AddOrderBy(aq => aq.DateOfObtainingTheQualification);
                    break;
                case AcademicQualificationsSortingOptions.DateDesc:
                    AddOrderByDescending(aq => aq.DateOfObtainingTheQualification);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public AcademicQualificationsSpecifications(int id) : base(aq => !aq.IsDeleted && aq.Id == id)
        {
            AddIncludes(aq => aq.Qualification);
            AddIncludes(aq => aq.Grade);
            AddIncludes(aq => aq.DispatchType);
        }
    }
}
