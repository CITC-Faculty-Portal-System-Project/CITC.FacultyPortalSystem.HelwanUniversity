using Domain.Entities.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.CVModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.CVModule;

namespace Services.Specifications.ReportsModule.CVModule
{
    public class CVReportSpecification
        : AggregationSpecification<FacultyMember, CVReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        public CVReportSpecification(
            BaseCVReportSpecificationParameters parameters,
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
                && fd.PersonalData != null
                && fd.CVPreferences != null
                && fd.CVPreferences.Any()
                &&
                (
                    parameters.FacultyIds == null || !parameters.FacultyIds.Any()
                    ||
                    (
                        fd.PersonalData.FacultyId.HasValue &&
                        parameters.FacultyIds.Contains(fd.PersonalData.FacultyId.Value)
                    )
                )
                &&
                (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData.Faculty.NameAR.Contains(search)
                    || fd.PersonalData.Faculty.NameEN.Contains(search)
                )
            );

            switch (parameters.Sort)
            {
                case CVReportSortingOptions.FacultyNameAsc:
                    AddOrderBy(fd => fd.PersonalData!.Faculty!.NameAR);
                    break;

                case CVReportSortingOptions.FacultyNameDesc:
                    AddOrderByDescending(fd => fd.PersonalData!.Faculty!.NameAR);
                    break;

                case CVReportSortingOptions.NoOfCVsASC:
                    AddOrderBy(fd =>
                        fd.CVPreferences!.Count());
                    break;

                case CVReportSortingOptions.NoOfCvsDESC:
                    AddOrderByDescending(fd =>
                        fd.CVPreferences!.Count());
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.Faculty!.NameAR);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Faculty);
            AddIncludes(fd => fd.PersonalData!.Department);
            AddIncludes(fd => fd.CVPreferences!);
        }

        public override IQueryable<CVReportResponseDTO> Apply(
            IQueryable<FacultyMember> query)
        {
            var baseQuery = query.Where(Criteria!);

            var projected = baseQuery
                .GroupBy(fd => new
                {
                    fd.PersonalData!.FacultyId,
                    FacultyName = fd.PersonalData.Faculty.NameAR
                })
                .Select(fg => new CVReportResponseDTO
                {
                    FacultyName = fg.Key.FacultyName,

                    NoOfCVs = fg.SelectMany(fd => fd.CVPreferences!).Count(),

                    DepartmentCVs = fg
                        .GroupBy(fd => new
                        {
                            fd.PersonalData!.DeptId,
                            DepartmentName = fd.PersonalData.Department.NameAR
                        })
                        .Select(dg => new DepartmentCVReportResponseDTO
                        {
                            DepartmentName = dg.Key.DepartmentName,
                            NoOfCVs = dg.SelectMany(fd => fd.CVPreferences!).Count()
                        })
                        .ToList()
                });

            return projected;
        }
    }
}