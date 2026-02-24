using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class RecommendedThesesSupervisionCountSpecifications : BaseSpecifications<Thesis, int>
    {
        public RecommendedThesesSupervisionCountSpecifications
            (ThesesSpecificationParameters parameters , Guid memberId) 
            :base(BuildCriteria(parameters , memberId))
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

            return rth =>
                !rth.IsDeleted &&
                rth.ComitteeMembers!.Any(cm => cm.MemberId == facultyMemberId && !cm.isConfirmed)

                && (!mappedType.HasValue || rth.Type == mappedType.Value)

                && (parameters.GradeIds == null || !parameters.GradeIds.Any()
                    || parameters.GradeIds.Contains(rth.GradeId))

                && (string.IsNullOrEmpty(parameters.Search)
                    || rth.Title.Contains(parameters.Search));
        }
    }
}
