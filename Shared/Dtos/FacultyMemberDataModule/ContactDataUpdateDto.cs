namespace Shared.Dtos.FacultyMemberDataModule
{
    public record ContactDataUpdateDto
    {
        public string? WorkPhoneNumber { get; set; }
        public string? HomePhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }
        public string? AlternativeEmail { get; set; }
        public string? FaxNumber { get; set; }
        public string? Address { get; set; }
    }
}