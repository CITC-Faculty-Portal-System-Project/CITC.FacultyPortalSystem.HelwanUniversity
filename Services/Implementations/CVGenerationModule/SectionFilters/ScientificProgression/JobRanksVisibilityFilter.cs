using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class JobRanksVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.JobRanks;

            if(!settings.ShowJobRanks && isPublic == false)
            {
                response.JobRanks.Clear();
                return;
            }

            if (!settings.ShowJobRank && !settings.ShowDateOfJobRank && isPublic == false)
            {
                response.JobRanks.Clear();
                return;
            }

            foreach(var jr in response.JobRanks ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowJobRankForPublic, () => jr.JobRank = null!);
                    HideIfFalse(settings.ShowDateOfJobRankForPublic, () => jr.DateOfJobRank = null!);

                }

                HideIfFalse(settings.ShowJobRank, () => jr.JobRank = null!);
                HideIfFalse(settings.ShowDateOfJobRank, () => jr.DateOfJobRank = null!);
            }
        }
    }
}
