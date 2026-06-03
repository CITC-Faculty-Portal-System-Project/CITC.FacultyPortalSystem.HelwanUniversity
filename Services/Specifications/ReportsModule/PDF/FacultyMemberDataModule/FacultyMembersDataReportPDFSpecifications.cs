using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;

namespace Services.Specifications.ReportsModule.PDF.FacultyMemberDataModule
{
    public class FacultyMembersDataReportPDFSpecifications
     : AggregationSpecification<FacultyMember, FacultyMembersDataReportResponseDTO>
    {
        public FacultyMembersDataReportPDFSpecifications(
            FacultyMembersDataReportSpecificatonParameters parameters)
        {
            SetCriteria(fd =>
                !fd.IsDeleted
                && (
                    parameters.FacultyIds != null && parameters.FacultyIds.Any()
                        && parameters.FacultyIds.Contains(fd.PersonalData!.FacultyId!.Value)
                    ||
                    parameters.DepartmentIds != null && parameters.DepartmentIds.Any()
                        && parameters.DepartmentIds.Contains(fd.PersonalData!.DeptId)
                )
               );

            switch (parameters.Sorting)
            {
                case FacultyMembersReportSortingOptions.NameAsc:
                    AddOrderBy(fd => fd.PersonalData!.NameAr);
                    break;
                case FacultyMembersReportSortingOptions.NameDesc:
                    AddOrderByDescending(fd => fd.PersonalData!.NameAr);
                    break;
                case FacultyMembersReportSortingOptions.NoOfInternationalResearchesASC:
                    AddOrderBy(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.International));
                    break;
                case FacultyMembersReportSortingOptions.NoOfInternationalResearchesDesc:
                    AddOrderByDescending(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.International));
                    break;
                case FacultyMembersReportSortingOptions.NoOfLocalResearchesAsc:
                    AddOrderBy(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.Local));
                    break;
                case FacultyMembersReportSortingOptions.NoOfLocalResearchesDesc:
                    AddOrderByDescending(fd => fd.ResearchContributions!
                        .Count(r => r.Research!.PublicationType == PublicationType.Local));
                    break;
                case FacultyMembersReportSortingOptions.NoOfPatentsAsc:
                    AddOrderBy(fd => fd.Patents.Count());
                    break;
                case FacultyMembersReportSortingOptions.NoOfPatentsDesc:
                    AddOrderByDescending(fd => fd.Patents.Count());
                    break;
                case FacultyMembersReportSortingOptions.NoOfAwardsAsc:
                    AddOrderBy(fd => fd.PrizesAndRewards.Count());
                    break;
                case FacultyMembersReportSortingOptions.NoOfAwardsDesc:
                    AddOrderByDescending(fd => fd.PrizesAndRewards.Count());
                    break;
            }

        }

        public override IQueryable<FacultyMembersDataReportResponseDTO>
            Apply(IQueryable<FacultyMember> query)
        {
            return query
                .Where(Criteria!)
                .Select(fd => new FacultyMembersDataReportResponseDTO
                {
                    Name = (fd.PersonalData!.Title!.ValueAr ?? "") + ". " + (fd.PersonalData.NameAr ?? ""),
                    Faculty = fd.PersonalData.Faculty!.NameAR ?? "",
                    Department = fd.PersonalData.Department!.NameAR ?? "",
                    Email = fd.ContactData!.OfficialEmail ?? "",
                    PhoneNumber = fd.ContactData.MainPhoneNumber ?? "",
                    NoOfInternationalResearches = fd.ResearchContributions!
                        .Count(r => !r.IsDeleted && !r.Research!.IsDeleted
                                 && r.Research.PublicationType == PublicationType.International),
                    NoOfLocalResearches = fd.ResearchContributions!
                        .Count(r => !r.IsDeleted && !r.Research!.IsDeleted
                                 && r.Research.PublicationType == PublicationType.Local),
                    NoOfPatents = fd.Patents.Count(p => !p.IsDeleted),
                    NoOfAwards = fd.PrizesAndRewards.Count(pr => !pr.IsDeleted)
                });
        }
    }
}
