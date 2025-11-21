namespace Shared.Dtos.FacultyMemberDataModule
{
    public record IdentificationCardDto
    {
        public string? ORCID { get; set; } = null;
        public string? EKB { get; set; } = null;
        public string? ResearcherId { get; set; } = null;
        public string? ResearcherGate { get; set; } = null;
        public string? AcademiaEdu { get; set; } = null;
    }
}
