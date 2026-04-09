namespace Shared.Models.CVGenerationModule.ProjectsAndCommittees
{
    public class ProjectsVisibility
    {
        public bool ShowProjects { get; set; } = true;
        public bool ShowProjectsForPublic { get; set; } = true;
        public bool ShowNameOfProject { get; set; } = true;
        public bool ShowNameOfProjectForPublic { get; set; } = true;
        public bool ShowTypeOfProject { get; set; } = true;
        public bool ShowTypeOfProjectForPublic { get; set; } = true;
        public bool ShowParticipationRole { get; set; } = true;
        public bool ShowParticipationRoleForPublic { get; set; } = true;
        public bool ShowFinancingAuthority { get; set; } = true;
        public bool ShowFinancingAuthorityForPublic { get; set; } = true;
        public bool ShowProjectStartDate { get; set; } = true;
        public bool ShowProjectStartDateForPublic { get; set; } = true;
        public bool ShowProjectEndDate { get; set; } = true;
        public bool ShowProjectEndDateForPublic { get; set; } = true;
    }
}
