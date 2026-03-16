using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class JobRanksVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.JobRanks;

            if(!settings.ShowJobRanks)
            {
                response.JobRanks.Clear();
                return;
            }

            if (!settings.ShowJobRank && !settings.ShowDateOfJobRank)
            {
                response.JobRanks.Clear();
                return;
            }

            foreach(var jr in response.JobRanks ?? [])
            {
                HideIfFalse(settings.ShowJobRank, () => jr.JobRank = null!);
                HideIfFalse(settings.ShowDateOfJobRank, () => jr.DateOfJobRank = null!);
            }
        }
    }
}
