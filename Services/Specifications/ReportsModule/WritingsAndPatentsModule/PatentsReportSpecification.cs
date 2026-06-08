using Domain.Enums;
using Services.Specifications;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsAndPatentsModule;

namespace Services.Specifications.ReportsModule.WritingsAndPatentsModule
{
    public class PatentsReportSpecification
    : AggregationSpecification<FacultyMember, PatentsReportReponseDTO>
    {

        public PatentsReportSpecification(
            BasePatentsReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            SetCriteria(fd =>
                !fd.IsDeleted
                 && (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && (
                        parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                        || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                    )

                    || parameters.FacultyIds != null && parameters.FacultyIds.Any()
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                    && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                    && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)

                    || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                    && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                )

                &&
                (
                    parameters.LocalOrInternational == null
                    || fd.Patents!.Any(p =>
                        !p.IsDeleted &&
                        p.LocalOrInternational == (Domain.Enums.LocalOrInternational)parameters.LocalOrInternational)
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

            if (mode == ReportMode.Table)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
            AddIncludes(fd => fd.Patents!);
        }

        public override IQueryable<PatentsReportReponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            return query
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
                        Type = (Shared.Enums.AcademicDataModule.MissionsModule.LocalOrInternational)LocalOrInternational.Local,
                        NoOfPatents = fd.Patents!.Count(p =>
                            !p.IsDeleted &&
                            p.LocalOrInternational == LocalOrInternational.Local)
                    },
                    new FacultyMemberPatentsAnsalysisDTO
                    {
                        Type = (Shared.Enums.AcademicDataModule.MissionsModule.LocalOrInternational)LocalOrInternational.International,
                        NoOfPatents = fd.Patents!.Count(p =>
                            !p.IsDeleted &&
                            p.LocalOrInternational == LocalOrInternational.International)
                    }
                    }
                });
        }
    }
}