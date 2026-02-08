using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Enums.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;
using System.Security.Cryptography;

namespace Services.Specifications.AcademicDataModule.ScientificProgressionModule
{
    internal class AcademicQualificationsSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsSpecifications(AcademicQualificationsSpecificationParamters parameters, string facultyMemberEmail)
            : base(aq => 
                  !aq.IsDeleted &&
                    aq.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.QualificationIds == null || !parameters.QualificationIds.Any() ||
                   parameters.QualificationIds.Contains(aq.QualificationId)) &&
                  (parameters.GradeIds == null || !parameters.GradeIds.Any() ||
                  (aq.GradeId.HasValue && parameters.GradeIds.Contains(aq.GradeId.Value))) &&
                  (parameters.DispatchIds == null || !parameters.DispatchIds.Any() ||
                   parameters.DispatchIds.Contains(aq.DispatchId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   aq.Qualification.ValueAr.Contains(parameters.Search) ||
                   aq.Qualification.ValueEn.Contains(parameters.Search) ||
                   aq.Specialization.Contains(parameters.Search))
            ) 
        {
            AddIncludes(aq => aq.Qualification);
            AddIncludes(aq => aq.Grade);
            AddIncludes(aq => aq.DispatchType);
            AddIncludes(aq => aq.Attachment);

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
            AddIncludes(aq => aq.Attachment);
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
