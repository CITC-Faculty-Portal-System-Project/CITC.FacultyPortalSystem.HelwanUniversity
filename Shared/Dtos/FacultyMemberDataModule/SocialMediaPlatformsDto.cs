namespace Shared.Dtos.FacultyMemberDataModule
{
    public record SocialMediaPlatformsDto
    {
        public string? LinkedIn { get; set; } =  null;
        public string? Instagram { get; set; } = null;
        public string? PersonalWebsite { get; set; } = null;
        public string? GoogleScholar { get; set; } = null;
        public string? Scopus { get; set; } = null;
        public string? Facebook { get; set; } = null;
        public string? X { get; set; } = null;
        public string? YouTube { get; set; } = null;
    }
}
