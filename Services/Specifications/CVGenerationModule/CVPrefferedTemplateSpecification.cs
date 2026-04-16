using Domain.Entities.CVGenerationModule;
using System.Linq.Expressions;

namespace Services.Specifications.CVGenerationModule
{
    internal class CVPrefferedTemplateSpecification : BaseSpecifications<SavedCVPreferences, int>
    {
        public CVPrefferedTemplateSpecification
            (Guid facultyMemberId) 
          : base(cv => cv.FacultyMemberId == facultyMemberId)
        {
        }

        public CVPrefferedTemplateSpecification
            (string templateName  , Guid facultyMemberId)
          : base(cv => cv.FacultyMemberId == facultyMemberId && string.Equals(cv.TemplateName , templateName))
        {
        }
    }
}
