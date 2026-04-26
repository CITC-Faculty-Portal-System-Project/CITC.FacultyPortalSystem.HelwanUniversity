using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.ReportsAndDashboard;

public class PersonalDataAggregationSpecification
{
    public IQueryable<FacultyUsersStatisticsDTO> Apply(
        IQueryable<Faculty> faculties,
        IQueryable<PersonalData> personalDataQuery)
    {
        var query = personalDataQuery.Where(pd => !pd.IsDeleted);

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