namespace Shared.Models.CVGenerationModule.Experiences
{
    public class GeneralExperienceVisibility
    {
        public bool ShowGeneralExperiences { get; set; } = true;
        public bool ShowGeneralExperiencesForPublic { get; set; } = true;
        public bool ShowExperienceTitle { get; set; } = true;
        public bool ShowExperienceTitleForPublic { get; set; } = true;
        public bool ShowAuthority { get; set; } = true;
        public bool ShowAuthorityForPublic { get; set; } = true;
        public bool ShowCountryOrCity { get; set; } = true;
        public bool ShowCountryOrCityForPublic { get; set; } = true;
        public bool ShowStartDate { get; set; } = true;
        public bool ShowStartDateForPublic { get; set; } = true;
        public bool ShowEndDate { get; set; } = true;
        public bool ShowEndDateForPublic { get; set; } = true;
    }
}
