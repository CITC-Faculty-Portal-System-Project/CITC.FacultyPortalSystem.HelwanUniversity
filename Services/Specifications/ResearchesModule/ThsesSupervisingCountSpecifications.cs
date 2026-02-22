
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThsesSupervisingCountSpecifications : BaseSpecifications<Supervising, int>
    {
        public ThsesSupervisingCountSpecifications
                (ThesesSupervisingSpecificationParameters parameters , Guid facultyMemberId) 
                    : base(ts => !ts.IsDeleted && ts.FacultyMemberId == facultyMemberId
                    && (string.IsNullOrEmpty(parameters.Search) ||
                   ts.Title.Contains(parameters.Search) ||
                   ts.StudentName.Contains(parameters.Search)
                   ))
        {
        }
    }
}
