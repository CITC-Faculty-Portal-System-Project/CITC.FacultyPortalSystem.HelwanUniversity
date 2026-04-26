using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.ReportsAndDashboard;

public class ResearchAggregationSpecification
{
    public IQueryable<ResearchesPerFacultyDTO> Apply(
        IQueryable<Faculty> faculties,
        IQueryable<Research> researches)
    {
        var validPersonalData = researches
            .Where(r => !r.IsDeleted && r.Contributions!.Any(c => c.IsConfirmed))
            .SelectMany(r => r.Contributions!)
            .Where(c => c.IsConfirmed)
            .Select(c => c.Contributor!.PersonalData!);

        return faculties
            .GroupJoin(
                validPersonalData,
                f => f.Id,
                pd => pd.FacultyId,
                (f, pdGroup) => new ResearchesPerFacultyDTO
                {
                    FacultyNameAR = f.NameAR,
                    FacultyNameEN = f.NameEN,
                    TotalNumberOfResearches = pdGroup.Count()
                });
    }
}