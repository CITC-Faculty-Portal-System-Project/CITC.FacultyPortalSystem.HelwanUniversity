using Shared.Dtos.ReportsAndDashboard.ExpereincesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ExperiencesModule;

namespace Services.Specifications.ReportsModule.ExperiencesModule
{
    public class ExperienceReportSpecification
        : AggregationSpecification<FacultyMember, ExpereinceReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        public ExperienceReportSpecification(
            BaseExperincesSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

            SetCriteria(fd =>
                !fd.IsDeleted
                &&
                (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && (
                        parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                        || parameters.DepartmentIds.Contains(fd.PersonalData.DeptId)
                    )

                    ||
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                    && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    ||
                    (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && parameters.DepartmentIds != null
                    && parameters.DepartmentIds.Any()
                    && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)

                    ||
                    (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                )
                &&
                (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData.NameEn.Contains(search)
                ));

            switch (parameters.Sorting)
            {
                case ExpereinceSortingOptions.FacultyMemberNameASC:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case ExpereinceSortingOptions.FacultyMemberNameDESC:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case ExpereinceSortingOptions.ExperienceCountAsc:
                    AddOrderBy(fd =>
                        fd.GeneralExperiences.Count(g => !g.IsDeleted)
                        + fd.TeachingExperiences.Count(t => !t.IsDeleted));
                    break;

                case ExpereinceSortingOptions.ExperienceCountDesc:
                    AddOrderByDescending(fd =>
                        fd.GeneralExperiences.Count(g => !g.IsDeleted)
                        + fd.TeachingExperiences.Count(t => !t.IsDeleted));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
            AddIncludes(fd => fd.GeneralExperiences);
            AddIncludes(fd => fd.TeachingExperiences);
        }

        public override IQueryable<ExpereinceReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            var baseQuery = query.Where(Criteria!);

            var projected = baseQuery.Select(fd => new ExpereinceReportResponseDTO
            {
                FacultyMemberName =
                    (fd.PersonalData!.Title!.ValueAr ?? "")
                    + (fd.PersonalData.NameAr ?? ""),

                Experiences = new List<FacultyMemberExperienceGroupingDTO>
                {
                    new FacultyMemberExperienceGroupingDTO
                    {
                        ExperienceType = "General Experience",
                        ExperienceCount = fd.GeneralExperiences.Count(x => !x.IsDeleted)
                    },
                    new FacultyMemberExperienceGroupingDTO
                    {
                        ExperienceType = "Teaching Experience",
                        ExperienceCount = fd.TeachingExperiences.Count(x => !x.IsDeleted)
                    }
                }
            });

            return projected;
        }
    }
}