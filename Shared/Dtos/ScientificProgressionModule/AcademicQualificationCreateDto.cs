using System.Linq;

namespace Shared.Dtos.ScientificProgressionModule
{
    public record AcademicQualificationCreateDto
    {
        public Guid QualificationId { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public Guid? GradeId { get; set; }
        public Guid DispatchId { get; set; }
        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly DateOfObtainingTheQualification { get; set; }

        public Guid FacultyMemberId { get; set; }
    }
}
