
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThsesSupervisingCountSpecifications : BaseSpecifications<Supervising, int>
    {
        public ThsesSupervisingCountSpecifications
                (ThesesSupervisingSpecificationParameters parameters , Guid facultyMemberId) 
                    : base(ts => !ts.IsDeleted && ts.FacultyMemberId == facultyMemberId
                    && (parameters.Type == null || (Shared.Enums.ResearchesModule.ThesisType)ts.Type == parameters.Type) ||
                  (parameters.Type == null || (Shared.Enums.ResearchesModule.FacultyMemberRoleInSupervisingThesis)ts.FacultyMemberRole == parameters.Role) ||
                  (parameters.GradeIds == null || !parameters.GradeIds.Any() ||
                   parameters.GradeIds.Contains(ts.GradeId)) &&
                    (string.IsNullOrEmpty(parameters.Search) ||
                   ts.Title.Contains(parameters.Search) ||
                   ts.StudentName.Contains(parameters.Search)
                   ))
        {
        }
    }
}
