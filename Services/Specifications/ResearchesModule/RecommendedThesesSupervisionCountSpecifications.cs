using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedThesesSupervisionCountSpecifications : BaseSpecifications<Supervising, int>
    {
        public RecommendedThesesSupervisionCountSpecifications
            (ThesesSupervisingSpecificationParameters parameters , Guid memberId) 
            :base(BuildCriteria(parameters , memberId))
        {
        }

        private static Expression<Func<Supervising, bool>> BuildCriteria(
             ThesesSupervisingSpecificationParameters parameters,
             Guid facultyMemberId)
        {
            Domain.Enums.ThesisType? mappedType = null;
            if (parameters.Type.HasValue)
            {
                mappedType = Enum.Parse<Domain.Enums.ThesisType>(
                    parameters.Type.Value.ToString(),
                    ignoreCase: true);
            }

            Domain.Enums.FacultyMemberRoleInSupervisingThesis? mappedRole = null;
            if (parameters.Role.HasValue)
            {
                mappedRole = Enum.Parse<Domain.Enums.FacultyMemberRoleInSupervisingThesis>(
                    parameters.Role.Value.ToString(),
                    ignoreCase: true);
            }

            return ts =>
                !ts.IsDeleted
                && ts.FacultyMemberId == facultyMemberId && ts.isConfirmed == false

                && (!mappedType.HasValue || ts.Type == mappedType.Value)

                && (!mappedRole.HasValue || ts.FacultyMemberRole == mappedRole.Value)

                && (parameters.GradeIds == null || !parameters.GradeIds.Any()
                    || parameters.GradeIds.Contains(ts.GradeId))

                && (string.IsNullOrEmpty(parameters.Search)
                    || ts.Title.Contains(parameters.Search)
                    || ts.StudentName.Contains(parameters.Search));
        }
    }
}
