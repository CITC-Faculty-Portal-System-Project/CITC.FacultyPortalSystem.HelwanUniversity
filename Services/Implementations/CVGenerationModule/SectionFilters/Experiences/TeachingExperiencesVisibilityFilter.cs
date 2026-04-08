using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.Experiences
{
    public class TeachingExperiencesVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.TeachingExperiences;

            if (!settings.ShowTeachingExperiences && isPublic == false)
            {
                response.TeachingExperiences.Clear();
                return;
            }

            if(!settings.ShowCourseName && !settings.ShowAcademicLevel && !settings.ShowUniversityOrFaculty && !settings.ShowTeachingExperienceStartDate && !settings.ShowTeachingExperienceEndDate && isPublic == false)
            {
                response.TeachingExperiences.Clear();
                return;
            }

            foreach(var te in response.TeachingExperiences ?? [])
            {
               
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowCourseNameForPublic, () => te.CourseName = null!);
                    HideIfFalse(settings.ShowAcademicLevelForPublic, () => te.AcademicLevel = null!);
                    HideIfFalse(settings.ShowUniversityOrFacultyForPublic, () => te.UniversityOrFaculty = null!);
                    HideIfFalse(settings.ShowTeachingExperienceStartDateForPublic, () => te.StartDate = null);
                    HideIfFalse(settings.ShowTeachingExperienceEndDateForPublic, () => te.EndDate = null);
                }

                HideIfFalse(settings.ShowCourseName, () => te.CourseName = null!);
                HideIfFalse(settings.ShowAcademicLevel, () => te.AcademicLevel = null!);
                HideIfFalse(settings.ShowUniversityOrFaculty, () => te.UniversityOrFaculty = null!);
                HideIfFalse(settings.ShowTeachingExperienceStartDate, () => te.StartDate = null);
                HideIfFalse(settings.ShowTeachingExperienceEndDate, () => te.EndDate = null);
            }
        }
    }
}
