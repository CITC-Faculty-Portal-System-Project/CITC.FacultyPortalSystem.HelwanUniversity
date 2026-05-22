using Shared.SpecificationParameters.ReportsAndDashboard.FacultyMembersDataModule;
using System.Linq.Expressions;

namespace Services.Specifications.ReportsModule.FacultyMembersDataModule
{
    internal class FacultyMembersDataReportCountSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMembersDataReportCountSpecifications
            (FacultyMembersDataReportSpecificatonParameters parameters) 
            : base(fd => !fd.IsDeleted
   && (
        (parameters.FacultyIds != null && parameters.FacultyIds.Any()
            && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value))
        ||
        (parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
            && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId))
    )
            && (string.IsNullOrEmpty(parameters.Search) ||
               fd.PersonalData!.NameAr.Contains(parameters.Search) ||
               fd.PersonalData!.NameEn.Contains(parameters.Search) ||
               fd.PersonalData!.Department.NameAR.Contains(parameters.Search) ||
               fd.PersonalData!.Department.NameEN.Contains(parameters.Search) ||
               fd.ContactData!.OfficialEmail.Contains(parameters.Search) ||
               fd.ContactData!.MainPhoneNumber.Contains(parameters.Search)))
        {
        }
    }
}
