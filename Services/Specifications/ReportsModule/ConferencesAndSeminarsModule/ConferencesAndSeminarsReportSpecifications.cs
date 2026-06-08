using Domain.Entities.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ConferencesAndSeminarsModule;

namespace Services.Specifications.ReportsModule.ConferencesAndSeminarsModule
{
    public class ConferencesAndSeminarsReportSpecification
        : AggregationSpecification<FacultyMember, ConferenceAndSeminarsReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        public ConferencesAndSeminarsReportSpecification(
            BaseConferencesAndSeminarsReportSpecifiactionParamters parameters,
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
                 && (
                     parameters.FacultyIds != null && parameters.FacultyIds.Any()
                     && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                         && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                             || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))

                     || parameters.FacultyIds != null && parameters.FacultyIds.Any()
                         && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                         && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)

                     || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                         && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                         && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)

                     || (parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                         && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                 )
                 && fd.ConferencesAndSeminars!.Any(c =>
                     !c.IsDeleted
                     && (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)
                 )
                 && (
                     string.IsNullOrWhiteSpace(search)
                     || fd.PersonalData!.NameAr.Contains(search)
                     || fd.PersonalData!.NameEn.Contains(search)
                 )
             );

            switch (parameters.Sort)
            {
                case ConferencesAndSeminarsSortingOptions.NoOfConferencesOrSeminarsAsc:
                    AddOrderBy(fd => fd.ConferencesAndSeminars!
                        .Count(c =>
                            !c.IsDeleted &&
                            (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)));
                    break;

                case ConferencesAndSeminarsSortingOptions.NoOfConferencesOrSeminarsDesc:
                    AddOrderByDescending(fd => fd.ConferencesAndSeminars!
                        .Count(c =>
                            !c.IsDeleted &&
                            (parameters.Type == null || c.Type == (Domain.Enums.ConferenceOrSeminar)parameters.Type)));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);
            AddIncludes(fd => fd.ConferencesAndSeminars!);
        }

        public override IQueryable<ConferenceAndSeminarsReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            var result = query
                .Where(Criteria!)
                .Select(fd => new ConferenceAndSeminarsReportResponseDTO
                {
                    FacultyMemberName =
                        (fd.PersonalData!.Title!.ValueAr ?? "")
                        + (fd.PersonalData.NameAr ?? ""),

                    ConferencesAndSeminars = new List<FacultyMemberConferencesAndSeminarsAnalysisDTO>
                    {
                        new FacultyMemberConferencesAndSeminarsAnalysisDTO
                        {
                            Type = Shared.Enums.AcademicDataModule.MissionsModule.ConferenceOrSeminar.Conference,
                            NoOfConferencesOrSeminars = fd.ConferencesAndSeminars!
                                .Count(c => !c.IsDeleted && c.Type == Domain.Enums.ConferenceOrSeminar.Conference)
                        },
                        new FacultyMemberConferencesAndSeminarsAnalysisDTO
                        {
                            Type = Shared.Enums.AcademicDataModule.MissionsModule.ConferenceOrSeminar.Seminar,
                            NoOfConferencesOrSeminars = fd.ConferencesAndSeminars!
                                .Count(c => !c.IsDeleted && c.Type == Domain.Enums.ConferenceOrSeminar.Seminar)
                        }
                    }
                });

            if (_isPaginated)
            {
                result = result
                    .Skip((_pageIndex - 1) * _pageSize)
                    .Take(_pageSize);
            }

            return result;
        }
    }
}