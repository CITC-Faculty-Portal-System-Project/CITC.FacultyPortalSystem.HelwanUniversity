using Domain.Entities.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.ScientificProgressionModule;
using Shared.SpecificationParameters.ScientificProgressionModule;
using System.Security.Cryptography;

namespace Services.Specifications.ScientificProgressionModule
{
    internal class AcademicQualificationsSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsSpecifications(AcademicQualificationsSpecificationParamters parameters)
            : base(aq => 
                  (!aq.IsDeleted &&
                    aq.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
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


        public AcademicQualificationsSpecifications(AcademicQualificationFetchingDTO dTO) 
            : base(aq => aq.Qualification.ValueAr == dTO.Qualification && 
                  aq.Specialization == dTO.Specialization && aq.CountryOrCity == dTO.CountryCity
            && aq.UniversityOrFaculty == dTO.UniversityFaculty && aq.DispatchType.ValueAr == dTO.Dispatch
            && aq.Grade.ValueAr == dTO.Grade && aq.FacultyMember.NationalNumber == dTO.NationalNumber)
        {
            AddIncludes(aq => aq.Qualification);
            AddIncludes(aq => aq.Grade);
            AddIncludes(aq => aq.DispatchType);
            AddIncludes(aq => aq.FacultyMember);

        }
    }
}
