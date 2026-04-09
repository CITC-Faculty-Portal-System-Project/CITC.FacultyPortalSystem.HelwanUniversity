namespace Shared.Models.CVGenerationModule.Missions
{
    public class ScientificMissionsVisibility
    {
        public bool ShowScientificMissions { get; set; } = true;
        public bool ShowScientificMissionsForPublic { get; set; } = true;
        public bool ShowMissionName { get; set; } = true;
        public bool ShowMissionNameForPublic { get; set; } = true;
        public bool ShowMissionStartDate { get; set; } = true;
        public bool ShowMissionStartDateForPublic { get; set; } = true;
        public bool ShowMissionEndDate { get; set; } = true;
        public bool ShowMissionEndDateForPublic { get; set; } = true;

        public bool ShowMissionUniversityOrFaculty { get; set; } = true;
        public bool ShowMissionUniversityOrFacultyForPublic { get; set; } = true;
        public bool ShowMissionCountryOrCity { get; set; } = true;
        public bool ShowMissionCountryOrCityForPublic { get; set; } = true;
    }
}
