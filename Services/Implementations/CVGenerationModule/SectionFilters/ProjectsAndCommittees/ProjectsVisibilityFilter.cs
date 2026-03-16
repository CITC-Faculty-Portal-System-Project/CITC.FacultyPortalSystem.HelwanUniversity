using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ProjectsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.Projects;

            if (!settings.ShowProjects)
            {
                response.Projects.Clear();
                return;
            }

            if (!settings.ShowNameOfProject && !settings.ShowTypeOfProject && !settings.ShowParticipationRole && !settings.ShowFinancingAuthority && !settings.ShowProjectStartDate && !settings.ShowProjectEndDate)
            {
                response.Projects.Clear();
                return;
            }

            foreach(var p in response.Projects ?? [])
            {
                HideIfFalse(settings.ShowNameOfProject, () => p.NameOfProject = null!);
                HideIfFalse(settings.ShowTypeOfProject, () => p.TypeOfProject = null!);
                HideIfFalse(settings.ShowParticipationRole, () => p.ParticipationRole = null!);
                HideIfFalse(settings.ShowFinancingAuthority, () => p.FinancingAuthority = null!);
                HideIfFalse(settings.ShowProjectStartDate, () => p.StartDate = null!);
                HideIfFalse(settings.ShowProjectEndDate, () => p.EndDate = null!);
            }
        }
    }
}
