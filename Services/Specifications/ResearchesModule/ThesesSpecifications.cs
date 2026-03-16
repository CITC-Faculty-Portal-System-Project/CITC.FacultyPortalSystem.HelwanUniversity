using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThesesSpecifications : BaseSpecifications<Thesis, int>
    {
        public ThesesSpecifications(int id , Guid facultyMemberId) 
                : base(t => t.Id == id && !t.IsDeleted && t.FacultyMemberId == facultyMemberId)
        {
            AddIncludes(t => t.Researches!);
            AddIncludes(t => t.Attachments!);
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(t => t
                        .Include(t => t.ComitteeMembers!)
                        .ThenInclude(t => t.JobLevel));

            AddIncludes(t => t.Supervisings!);

        }


        public ThesesSpecifications(ThesesSpecificationParameters parameters , Guid facultyMemberId)
            :base(BuildCriteria(parameters, facultyMemberId))
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
                case ThesesSortingOptions.DiscussionDateASC:
                    AddOrderBy(ts => ts.DiscussionDate!);
                    break;
                case ThesesSortingOptions.DiscussionDateDESC:
                    AddOrderByDescending(ts => ts.DiscussionDate!);
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

            return t =>
                !t.IsDeleted
                && t.FacultyMemberId == facultyMemberId

                && (!mappedType.HasValue || t.Type == mappedType.Value)

                && (parameters.GradeIds == null || !parameters.GradeIds.Any()
                    || parameters.GradeIds.Contains(t.GradeId))

                && (string.IsNullOrEmpty(parameters.Search)
                    || t.Title.Contains(parameters.Search)
                    || t.UniversityOrFaculty!.Contains(parameters.Search));
        }
    }
}
