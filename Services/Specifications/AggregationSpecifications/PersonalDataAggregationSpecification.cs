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

            var faculties = query
                .Select(x => x.Faculty!)
                .Distinct();

            return faculties
                .GroupJoin(
                    query,
                    f => f.Id,
                    pd => pd.FacultyId,
                    (f, pdGroup) => new FacultyUsersStatisticsDTO
                    {
                        FacultyNameAR = f.NameAR,
                        FacultyNameEN = f.NameEN,
                        TotalNumberOfUsers = pdGroup.Count()
                    });
        }
    }
}