using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule;

namespace Services.Specifications.AcademicDataModule.ScientificProgressionModule
{
    internal class AcademicQualificationsCountSpecifications : BaseSpecifications<AcademicQualifications, int>
    {
        public AcademicQualificationsCountSpecifications(AcademicQualificationsSpecificationParamters parameters, string facultyMemberEmail)
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
        }

        public AcademicQualificationsCountSpecifications(Guid facultyMemberId)
            : base(aq =>
                  !aq.IsDeleted &&
                    aq.FacultyMemberId == facultyMemberId
            )
        {
        }
    }
}
