using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Domain.Entities.FacultyMemberDataModule;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ProjectsAndComiteesModule;

namespace Services.Specifications.ReportsModule.ProjectsAndComiteesModule
{
    public class ParticipationInMagazinesReportSpecification
        : AggregationSpecification<FacultyMember, ParticipationInMagazinesReportResponseDTO>
    {
        private readonly bool _isPaginated;
        private readonly int _pageIndex;
        private readonly int _pageSize;

        private readonly List<Guid>? _typesOfParticipation;

        public ParticipationInMagazinesReportSpecification(
            BaseParticipationInMagazineReportSpecificationParameters parameters,
            ReportMode mode,
            int pageIndex = 1,
            int pageSize = 9,
            string? search = null)
        {
            _isPaginated = mode == ReportMode.Table;
            _pageIndex = pageIndex;
            _pageSize = pageSize;

            if (parameters.TypesOfParticipation is not null)
                _typesOfParticipation = parameters.TypesOfParticipation;

            SetCriteria(fd =>
                !fd.IsDeleted

                &&
                (
                    (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && (parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                            || parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!)))

                    || (parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any())
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId!))

                    || ((parameters.FacultyIds == null || !parameters.FacultyIds.Any())
                        && (parameters.DepartmentIds == null || !parameters.DepartmentIds.Any()))
                )

                &&
                (
                    _typesOfParticipation == null
                    || !_typesOfParticipation.Any()
                    || fd.ParticipationInMagazines.Any(p =>
                        _typesOfParticipation.Contains(p.TypeOfParticipationId))
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
                case ParticipationInMagazineReportSortingOptions.FacultyMemberNameASC:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;

                case ParticipationInMagazineReportSortingOptions.FacultyMemberNameDESC:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;

                case ParticipationInMagazineReportSortingOptions.NoOfParticipationsASC:
                    AddOrderBy(fd => fd.ParticipationInMagazines.Count(p => !p.IsDeleted));
                    break;

                case ParticipationInMagazineReportSortingOptions.NoOfParticipationsDESC:
                    AddOrderByDescending(fd => fd.ParticipationInMagazines.Count(p => !p.IsDeleted));
                    break;

                default:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
            }

            if (_isPaginated)
                applyPagination(pageSize, pageIndex);

            AddIncludes(fd => fd.PersonalData!);
            AddIncludes(fd => fd.PersonalData!.Title);

            AddIncludeWithChain(fd => fd
                .Include(x => x.ParticipationInMagazines)
                    .ThenInclude(p => p.TypeOfParticipation));
        }

        public override IQueryable<ParticipationInMagazinesReportResponseDTO> Apply(IQueryable<FacultyMember> query)
        {
            var baseQuery = query.Where(Criteria!);

            var projected = baseQuery.Select(fd => new ParticipationInMagazinesReportResponseDTO
            {
                FacultyMemberName =
                    (fd.PersonalData!.Title!.ValueAr ?? "") +
                    (fd.PersonalData.NameAr ?? ""),

                Participations =
                    fd.ParticipationInMagazines
                        .Where(p =>
                            !p.IsDeleted &&
                            (
                                _typesOfParticipation == null ||
                                !_typesOfParticipation.Any() ||
                                _typesOfParticipation.Contains(p.TypeOfParticipationId)
                            )
                        )
                        .GroupBy(p => p.TypeOfParticipation.ValueAr)
                        .Select(g => new FacultyMemberParticipationInMagazinesReportAnalysisDTO
                        {
                            ParticipationType = g.Key,
                            NoOfParticipations = g.Count()
                        })
                        .ToList()
            });

            return projected;
        }
    }
}