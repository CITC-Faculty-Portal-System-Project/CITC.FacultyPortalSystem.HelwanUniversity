using Domain.Entities;
using Shared.ReportsAndDashboard;
using System.Linq;

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

            return query
                .GroupBy(x => new
                {
                    x.FacultyId,
                    x.Faculty!.NameAR,
                    x.Faculty!.NameEN
                })
                .Select(g => new FacultyUsersStatisticsDTO
                {
                    FacultyNameAR = g.Key.NameAR,
                    FacultyNameEN = g.Key.NameEN,
                    TotalNumberOfUsers = g.Count()
                });
        }
    }
}