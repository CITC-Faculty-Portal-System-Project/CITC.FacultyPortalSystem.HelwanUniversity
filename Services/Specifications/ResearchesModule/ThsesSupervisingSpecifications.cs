using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.FacultyMemberDataModule;
using Shared.SpecificationParameters.ResearchesModule;
using Shared.Enums.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class ThsesSupervisingSpecifications : BaseSpecifications<Supervising, int>
    {
        public ThsesSupervisingSpecifications(int id)
            : base(ts => !ts.IsDeleted && ts.Id == id)
        {
            AddIncludes(ts => ts.Grade!);
        }

        public ThsesSupervisingSpecifications(
            ThesesSupervisingSpecificationParameters parameters,
            Guid facultyMemberId)
            : base(BuildCriteria(parameters, facultyMemberId))
        {
            switch (parameters.Sort)
            {
                case ThesesSupervisingSortingOptions.TitleASC:
                    AddOrderBy(ts => ts.Title);
                    break;

                case ThesesSupervisingSortingOptions.TitleDESC:
                    AddOrderByDescending(ts => ts.Title);
                    break;

                case ThesesSupervisingSortingOptions.StudentNameASC:
                    AddOrderBy(ts => ts.StudentName);
                    break;

                case ThesesSupervisingSortingOptions.StudentNameDESC:
                    AddOrderByDescending(ts => ts.StudentName);
                    break;

                case ThesesSupervisingSortingOptions.RegistrationDateASC:
                    AddOrderBy(ts => ts.RegistrationDate!);
                    break;

                case ThesesSupervisingSortingOptions.RegistrationDateDESC:
                    AddOrderByDescending(ts => ts.RegistrationDate!);
                    break;

                case ThesesSupervisingSortingOptions.SupervisionFormationDateASC:
                    AddOrderBy(ts => ts.SupervisionFormationDate!);
                    break;

                case ThesesSupervisingSortingOptions.SupervisionFormationDateDESC:
                    AddOrderByDescending(ts => ts.SupervisionFormationDate!);
                    break;

                case ThesesSupervisingSortingOptions.DiscussionDateASC:
                    AddOrderBy(ts => ts.DiscussionDate!);
                    break;

                case ThesesSupervisingSortingOptions.DiscussionDateDESC:
                    AddOrderByDescending(ts => ts.DiscussionDate!);
                    break;

                case ThesesSupervisingSortingOptions.GrantingDateASC:
                    AddOrderBy(ts => ts.GrantingDate!);
                    break;

                case ThesesSupervisingSortingOptions.GrantingDateDESC:
                    AddOrderByDescending(ts => ts.GrantingDate!);
                    break;

                default:
                    break;
            }

            applyPagination(parameters.PageSize, parameters.PageIndex);
            AddIncludes(ts => ts.Grade!);
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
                && ts.FacultyMemberId == facultyMemberId

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