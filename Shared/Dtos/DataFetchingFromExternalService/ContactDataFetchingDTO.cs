namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ContactDataFetchingDTO
    {
        public string MainPhoneNumber { get; set; } = string.Empty;
        public string? WorkPhoneNumber { get; set; }
        public string? HomePhoneNumber { get; set; }
        public string OfficialEmail { get; set; } = string.Empty;
        public string? PersonalEmail { get; set; }
        public string? AlternativeEmail { get; set; }
        public string? FaxNumber { get; set; }
        public string? Address { get; set; }
        public string NationalNumber { get; set; } = string.Empty;

    }
}
