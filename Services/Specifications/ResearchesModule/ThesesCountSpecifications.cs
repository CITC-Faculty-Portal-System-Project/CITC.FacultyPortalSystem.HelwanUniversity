using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThesesCountSpecifications : BaseSpecifications<Thesis, int>
    {
        public ThesesCountSpecifications
            (ThesesSpecificationParameters parameters , Guid facultyMemberId) 
                : base(BuildCriteria(parameters , facultyMemberId))
        {
        
        }

        private static Expression<Func<Thesis, bool>> BuildCriteria(
        ThesesSpecificationParameters parameters,
        Guid facultyMemberId)
        {
            Domain.Enums.ThesisType? mappedType = null;
            if (parameters.Type.HasValue)
            {
                mappedType = Enum.Parse<Domain.Enums.ThesisType>(
                    parameters.Type.Value.ToString(),
                    ignoreCase: true);
            }

            return t =>
                !t.IsDeleted
                && t.FacultyMemberId == facultyMemberId

                && (!mappedType.HasValue || t.Type == mappedType.Value)

                && (parameters.GradeIds == null || !parameters.GradeIds.Any()
                    || parameters.GradeIds.Contains(t.GradeId))

                && (string.IsNullOrEmpty(parameters.Search)
                    || t.Title.Contains(parameters.Search)
                    || t.UniversityOrFaculty!.Contains(parameters.Search));
        }
    }
}
