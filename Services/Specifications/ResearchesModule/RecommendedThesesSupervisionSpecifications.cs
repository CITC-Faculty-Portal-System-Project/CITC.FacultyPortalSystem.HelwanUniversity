using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedThesesSupervisionSpecifications : BaseSpecifications<Thesis, int>
    {
        public RecommendedThesesSupervisionSpecifications
            (ThesesSpecificationParameters parameters , Guid memberId) 
            :base(BuildCriteria(parameters , memberId))
        {
            switch (parameters.Sort)
            {
                case ThesesSortingOptions.TitleASC:
                    AddOrderBy(ts => ts.Title);
                    break;
                case ThesesSortingOptions.TitleDESC:
                    AddOrderByDescending(ts => ts.Title);
                    break;
                case ThesesSortingOptions.EnrollmentDateASC:
                    AddOrderBy(ts => ts.EnrollmentDate);
                    break;
                case ThesesSortingOptions.EnrollmentDateDESC:
                    AddOrderByDescending(ts => ts.EnrollmentDate);
                    break;
                case ThesesSortingOptions.RegisterationDateASC:
                    AddOrderBy(ts => ts.RegistrationDate!);
                    break;
                case ThesesSortingOptions.RegisterationDateDESC:
                    AddOrderByDescending(ts => ts.RegistrationDate!);
                    break;
                default:
                    break;
            }



            AddIncludes(t => t.Researches!);
            AddIncludes(t => t.Attachments!);
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(t => t
                        .Include(t => t.ComitteeMembers!)
                        .ThenInclude(t => t.JobLevel));

            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public RecommendedThesesSupervisionSpecifications(int id , Guid memberId)
            :base(rth => rth.Id == id && !rth.IsDeleted 
                && rth.ComitteeMembers!.Any(cm => cm.MemberId == memberId && !cm.isConfirmed)) 
        {
            AddIncludes(t => t.Researches!);
            AddIncludes(t => t.Attachments!);
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(t => t
                        .Include(t => t.ComitteeMembers!)
                        .ThenInclude(t => t.JobLevel));

        }

        private static Expression<Func<Thesis, bool>> BuildCriteria(
        ThesesSpecificationParameters parameters,
        Guid facultyMemberId)
        {
            Domain.Enums.ThesisType? mappedType = null;
            if (parameters.Type.HasValue)
            {
                mappedType = Enum.Parse<Domain.Enums.ThesisType>(
                    parameters.Type.Value.ToString(),
                    ignoreCase: true);
            }

            return rth =>
                !rth.IsDeleted &&
                rth.ComitteeMembers!.Any(cm => cm.MemberId == facultyMemberId && !cm.isConfirmed)

                && (!mappedType.HasValue || rth.Type == mappedType.Value)

                && (parameters.GradeIds == null || !parameters.GradeIds.Any()
                    || parameters.GradeIds.Contains(rth.GradeId))

                && (string.IsNullOrEmpty(parameters.Search)
                    || rth.Title.Contains(parameters.Search));
        }

    }
}

