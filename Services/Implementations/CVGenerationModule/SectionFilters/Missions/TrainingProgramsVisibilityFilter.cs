using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Missions
{
    public class TrainingProgramsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config , bool isPublic)
        {
            var settings = config.TrainingPrograms;

            if (!settings.ShowTrainingPrograms && isPublic == false)
            {
                response.TrainingPrograms.Clear();
                return;
            }

            if (!settings.ShowTrainingProgramName && !settings.ShowTrainingProgramVenue && !settings.ShowTrainingProgramStartDate && !settings.ShowTrainingProgramEndDate && isPublic == false)
            {
                response.TrainingPrograms.Clear();
                return;
            }

            foreach(var tp in response.TrainingPrograms ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowTrainingProgramNameForPublic, () => tp.TrainingProgramName = null!);
                    HideIfFalse(settings.ShowTrainingProgramVenueForPublic, () => tp.Venue = null!);
                    HideIfFalse(settings.ShowTrainingProgramStartDateForPublic, () => tp.StartDate = null);
                    HideIfFalse(settings.ShowTrainingProgramEndDateForPublic, () => tp.EndDate = null);

                }

                HideIfFalse(settings.ShowTrainingProgramName, () => tp.TrainingProgramName = null!);
                HideIfFalse(settings.ShowTrainingProgramVenue, () => tp.Venue = null!);
                HideIfFalse(settings.ShowTrainingProgramStartDate, () => tp.StartDate = null);
                HideIfFalse(settings.ShowTrainingProgramEndDate, () => tp.EndDate = null);

            }
        }
    }
}
