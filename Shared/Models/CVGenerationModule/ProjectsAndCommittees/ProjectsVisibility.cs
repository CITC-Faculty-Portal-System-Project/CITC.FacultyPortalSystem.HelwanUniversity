namespace Shared.Models.CVGenerationModule.ProjectsAndCommittees
{
    public class ProjectsVisibility
    {
        public bool ShowProjects { get; set; } = true;
        public bool ShowNameOfProject { get; set; } = true;
        public bool ShowTypeOfProject { get; set; } = true;
        public bool ShowParticipationRole { get; set; } = true;
        public bool ShowFinancingAuthority { get; set; } = true;
        public bool ShowProjectStartDate { get; set; } = true;
        public bool ShowProjectEndDate { get; set; } = true;
    }
}
