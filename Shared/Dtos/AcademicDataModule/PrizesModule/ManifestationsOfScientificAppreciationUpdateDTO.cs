namespace Shared.Dtos.AcademicDataModule.PrizesModule
{
    public record ManifestationsOfScientificAppreciationUpdateDTO
    {
        public string TitleOfAppreciation { get; set; } = string.Empty;
        public string IssuingAuthority { get; set; } = string.Empty;
        public DateOnly DateOfAppreciation { get; set; }
        public string? Description { get; set; }
    }
}
