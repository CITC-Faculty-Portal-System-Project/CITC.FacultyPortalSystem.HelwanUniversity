using Domain.Entities.UniversityFacultiesAndDepartments;
using System.Linq.Expressions;

namespace Services.Specifications.AggregationSpecifications
{
    internal class FacultySpecification : BaseSpecifications<Faculty, int>
    {
        public FacultySpecification
            () : base(f => !f.IsDeleted)
        {
            AddIncludes(f => f.FacultyMembersPersonalData!);
        }
    }
}
