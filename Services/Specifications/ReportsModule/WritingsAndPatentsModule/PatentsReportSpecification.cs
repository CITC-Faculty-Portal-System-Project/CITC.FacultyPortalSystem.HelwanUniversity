using Microsoft.EntityFrameworkCore;
using Services.Specifications;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsAndPatentsModule;

namespace Services.Specifications.ReportsModule.WritingsAndPatentsModule
{
    public class PatentsReportSpecification
        : AggregationSpecification<FacultyMember, PatentsReportReponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;
        private readonly LocalOrInternational? _localOrInternational;

        public PatentsReportSpecification(
            BasePatentsReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

            if(parameters.LocalOrInternational.HasValue)
                _localOrInternational = parameters.LocalOrInternational.Value;

            SetCriteria(fd =>
                !fd.IsDeleted
                &&
                (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && (
                        parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                        || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                    )

                    ||
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                    && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    ||
                    (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)

                    ||
                    (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                )
                &&
                (
                    _localOrInternational == null
                    || fd.Patents!.Any(p =>
                        !p.IsDeleted &&
                        p.LocalOrInternational == (Domain.Enums.LocalOrInternational)_localOrInternational)
                )
                &&
                (
                    string.IsNullOrWhiteSpace(search)
                    || fd.PersonalData!.NameAr.Contains(search)
                    || fd.PersonalData!.NameEn.Contains(search)
                )
            );

            switch (parameters.Sort)
            {
                case PatentsReportSortingOptions.FacultyMemberNameASC:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case PatentsReportSortingOptions.FacultyMemberNameDESC:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case PatentsReportSortingOptions.NoOfPatentsASC:
                    AddOrderBy(fd => fd.Patents!.Count(p => !p.IsDeleted));
                    break;

                case PatentsReportSortingOptions.NoOfPatentsDESC:
                    AddOrderByDescending(fd => fd.Patents!.Count(p => !p.IsDeleted));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
            AddIncludes(fd => fd.Patents!);
        }

        public override IQueryable<PatentsReportReponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            var result = query
                .Where(Criteria!)
                .Select(fd => new PatentsReportReponseDTO
                {
                    FacultyMemberName =
                        (fd.PersonalData!.Title!.ValueAr ?? "")
                        + (fd.PersonalData.NameAr ?? ""),

                    Patents = new List<FacultyMemberPatentsAnsalysisDTO>
                    {
                        new FacultyMemberPatentsAnsalysisDTO
                        {
                            Type = Shared.Enums.AcademicDataModule.MissionsModule.LocalOrInternational.Local,
                            NoOfPatents = fd.Patents!.Count(p =>
                                !p.IsDeleted &&
                                p.LocalOrInternational == Domain.Enums.LocalOrInternational.Local)
                        },
                        new FacultyMemberPatentsAnsalysisDTO
                        {
                            Type = Shared.Enums.AcademicDataModule.MissionsModule.LocalOrInternational.International,
                            NoOfPatents = fd.Patents!.Count(p =>
                                !p.IsDeleted &&
                                p.LocalOrInternational == Domain.Enums.LocalOrInternational.International)
                        }
                    }
                });

            return result;
        }
    }
}