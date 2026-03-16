namespace Shared.Dtos.CVGenerationModule.ScientificProgression
{
    public record CVAcademicQualificationsDTO
    {
        public int Id { get; set; }
        public LookupItemDto? Qualification { get; set; } 
        public string? Specialization { get; set; } 
        public LookupItemDto? Grade { get; set; } 
        public LookupItemDto? DispatchType { get; set; } 
        public string? UniversityOrFaculty { get; set; } 
        public string? CountryOrCity { get; set; }
        public DateOnly? DateOfObtainingTheQualification { get; set; }
    }
}
