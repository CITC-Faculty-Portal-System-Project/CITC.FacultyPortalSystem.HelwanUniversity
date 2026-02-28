using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedThesesSupervisionSpecifications : BaseSpecifications<Supervising, int>
    {
        public RecommendedThesesSupervisionSpecifications
            (ThesesSupervisingSpecificationParameters parameters , Guid memberId) 
            :base(BuildCriteria(parameters , memberId))
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



            AddIncludes(ts => ts.Grade!);
            AddIncludeWithChain(ts => 
                        ts.Include(t => t.Thesis)
                        .ThenInclude(t => t!.ComitteeMembers!
                            .Where(cm => cm.MemberId == memberId)));
            
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public RecommendedThesesSupervisionSpecifications(int id , Guid memberId)
            :base(rth => rth.Id == id && !rth.IsDeleted 
                && rth.FacultyMemberId == memberId && !rth.isConfirmed) 
        {
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(ts =>
                        ts.Include(t => t.Thesis)
                        .ThenInclude(t => t!.ComitteeMembers!
                            .Where(cm => cm.MemberId == memberId)));

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
                && ts.FacultyMemberId == facultyMemberId && !ts.isConfirmed

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

