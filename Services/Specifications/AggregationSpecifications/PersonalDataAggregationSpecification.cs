using Domain.Entities;
using Shared.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class PersonalDataAggregationSpecification
        : AggregationSpecification<PersonalData, FacultyUsersStatisticsDTO>
    {
        public PersonalDataAggregationSpecification()
        {
            SetCriteria(pd => !pd.IsDeleted);
        }

        public override IQueryable<FacultyUsersStatisticsDTO> Apply(IQueryable<PersonalData> query)
        {
            if (Criteria != null)
                query = query.Where(Criteria);

            // جيب كل الكليات من DB
            var faculties = query
                .Where(pd => pd.Faculty != null)
                .Select(pd => new { pd.Faculty!.Id, pd.Faculty.NameAR, pd.Faculty.NameEN })
                .Distinct()
                .ToList();

            var userCounts = query
                .GroupBy(pd => pd.FacultyId)
                .Select(g => new { FacultyId = g.Key, Count = g.Count() })
                .ToList();

            var result = faculties
                .GroupJoin(
                    userCounts,
                    f => f.Id,
                    uc => uc.FacultyId,
                    (f, uc) => new FacultyUsersStatisticsDTO
                    {
                        FacultyNameAR = f.NameAR,
                        FacultyNameEN = f.NameEN,
                        TotalNumberOfUsers = uc.FirstOrDefault()?.Count ?? 0
                    })
                .ToList();

            return result.AsQueryable();
        }
    }
}