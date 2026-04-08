using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class ProjectsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic)
        {
            var settings = config.Projects;

            if (!settings.ShowProjects && isPublic == false)
            {
                response.Projects.Clear();
                return;
            }

            if (!settings.ShowNameOfProject && !settings.ShowTypeOfProject && !settings.ShowParticipationRole && !settings.ShowFinancingAuthority && !settings.ShowProjectStartDate && !settings.ShowProjectEndDate && isPublic == false)
            {
                response.Projects.Clear();
                return;
            }

            foreach(var p in response.Projects ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowNameOfProjectForPublic, () => p.NameOfProject = null!);
                    HideIfFalse(settings.ShowTypeOfProjectForPublic, () => p.TypeOfProject = null!);
                    HideIfFalse(settings.ShowParticipationRoleForPublic, () => p.ParticipationRole = null!);
                    HideIfFalse(settings.ShowFinancingAuthorityForPublic, () => p.FinancingAuthority = null!);
                    HideIfFalse(settings.ShowProjectStartDateForPublic, () => p.StartDate = null!);
                    HideIfFalse(settings.ShowProjectEndDateForPublic, () => p.EndDate = null!);


                }

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
