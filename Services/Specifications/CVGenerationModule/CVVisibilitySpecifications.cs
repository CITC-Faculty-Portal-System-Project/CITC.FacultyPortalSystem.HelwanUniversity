using Domain.Entities.CVGenerationModule;

namespace Services.Specifications.CVGenerationModule
{
    internal class CVVisibilitySpecifications : BaseSpecifications<CVVisibilitySettings, Guid>
    {
        public CVVisibilitySpecifications(Guid facultyMemberId)
            : base(s => s.FacultyMemberId == facultyMemberId)
        {
        }
    }
}
