using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression
{
    public class AcademicQualificationVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic)
        {
            var settings = config.AcademicQualifications;

            if (!settings.ShowAcademicQualifications && isPublic == false)
            {
                response.AcademicQualifications.Clear();
                return;
            }

            if (!settings.ShowQualification && !settings.ShowGrade && !settings.ShowDispatchType && !settings.ShowSpecialization && !settings.ShowUniversityOrFaculty && !settings.ShowCountryOrCity && !settings.ShowDateOfObtainingTheQualification && isPublic == false)
            {
                response.AcademicQualifications.Clear();
                return;
            }

            foreach (var aq in response.AcademicQualifications ?? [])
            {
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowQualificationForPublic, () => aq.Qualification = null!);
                    HideIfFalse(settings.ShowGradeForPublic, () => aq.Grade = null!);
                    HideIfFalse(settings.ShowDispatchTypeForPublic, () => aq.DispatchType = null!);
                    HideIfFalse(settings.ShowSpecializationForPublic, () => aq.Specialization = string.Empty);
                    HideIfFalse(settings.ShowUniversityOrFacultyForPublic, () => aq.UniversityOrFaculty = null);
                    HideIfFalse(settings.ShowCountryOrCityForPublic, () => aq.CountryOrCity = null);
                    HideIfFalse(settings.ShowDateOfObtainingTheQualificationForPublic, () => aq.DateOfObtainingTheQualification = null);

                }


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
