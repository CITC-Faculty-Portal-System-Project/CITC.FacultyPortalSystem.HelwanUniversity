namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record SceintificMissionsFetchingDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? UniversityFaculty { get; set; }
        public string CountryCity { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string NationalNumber { get; set; } = string.Empty;

    }
}
