using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThesesCountSpecifications : BaseSpecifications<Thesis, int>
    {
        public ThesesCountSpecifications
            (ThesesSpecificationParameters parameters , Guid facultyMemberId) 
                : base(t => !t.IsDeleted 
                    && t.FacultyMemberId == facultyMemberId && 
                    (string.IsNullOrEmpty(parameters.Search) ||
                   t.Title.Contains(parameters.Search)))
                {
        
        }
    }
}
