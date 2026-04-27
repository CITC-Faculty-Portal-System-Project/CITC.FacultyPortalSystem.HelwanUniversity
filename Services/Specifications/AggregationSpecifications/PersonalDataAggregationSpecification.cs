using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class PersonalDataAggregationSpecification
        : AggregationSpecification<PersonalData, FacultyUsersStatisticsDTO>
    {
        private readonly IQueryable<Faculty> _faculties;

        public PersonalDataAggregationSpecification(IQueryable<Faculty> faculties)
        {
            _faculties = faculties;
            SetCriteria(pd => !pd.IsDeleted);
        }

        public override IQueryable<FacultyUsersStatisticsDTO> Apply(IQueryable<PersonalData> query)
        {
            return _faculties
                .Where(f => !f.IsDeleted)
                .Select(f => new FacultyUsersStatisticsDTO
                {
                    FacultyNameAR = f.NameAR,
                    FacultyNameEN = f.NameEN,
                    TotalNumberOfUsers = f.FacultyMembersPersonalData != null
                        ? f.FacultyMembersPersonalData.Count(pd => !pd.IsDeleted)
                        : 0
                });
        }
    }
}