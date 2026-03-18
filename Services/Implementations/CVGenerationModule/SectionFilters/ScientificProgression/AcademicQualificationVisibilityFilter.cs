using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class AcademicQualificationVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config)
        {
            var settings = config.AcademicQualifications;

            if (!settings.ShowAcademicQualifications)
            {
                response.AcademicQualifications.Clear();
                return;
            }

            if (!settings.ShowQualification && !settings.ShowGrade && !settings.ShowDispatchType && !settings.ShowSpecialization && !settings.ShowUniversityOrFaculty && !settings.ShowCountryOrCity && !settings.ShowDateOfObtainingTheQualification)
            {
                response.AcademicQualifications.Clear();
                return;
            }

            foreach (var aq in response.AcademicQualifications ?? [])
            {
                HideIfFalse(settings.ShowQualification, () => aq.Qualification = null!);
                HideIfFalse(settings.ShowGrade, () => aq.Grade = null!);
                HideIfFalse(settings.ShowDispatchType, () => aq.DispatchType = null!);
                HideIfFalse(settings.ShowSpecialization, () => aq.Specialization = string.Empty);
                HideIfFalse(settings.ShowUniversityOrFaculty, () => aq.UniversityOrFaculty = null);
                HideIfFalse(settings.ShowCountryOrCity, () => aq.CountryOrCity = null);
                HideIfFalse(settings.ShowDateOfObtainingTheQualification, () => aq.DateOfObtainingTheQualification = null);
            }
        }
    }
}
