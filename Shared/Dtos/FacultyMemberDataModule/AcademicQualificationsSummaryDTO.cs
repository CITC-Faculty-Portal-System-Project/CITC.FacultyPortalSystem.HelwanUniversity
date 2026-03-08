namespace Shared.Dtos.FacultyMemberDataModule
{
    public record AcademicQualificationsSummaryDTO
    {
        public LookupItemDto Qualification { get; set; } = null!;
        public string Specialization { get; set; } = string.Empty;
        public string? UniversityOrFaculty { get; set; }
        public DateOnly DateOfObtainingTheQualification { get; set; }
    }
}
