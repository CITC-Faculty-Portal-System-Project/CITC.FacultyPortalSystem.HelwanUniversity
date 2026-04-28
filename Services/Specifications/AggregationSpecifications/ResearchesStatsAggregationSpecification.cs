using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchesStatsAggregationSpecification
        : AggregationSpecification<Research, ResearchesStatsDTO>
    {
        public ResearchesStatsAggregationSpecification()
        {
            SetCriteria(r =>
                !r.IsDeleted &&
                r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<ResearchesStatsDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            var total = filtered.Count();

            var internalCount = filtered
                .Count(r => r.PublicationType == PublicationType.Local);

            var externalCount = filtered
                .Count(r => r.PublicationType == PublicationType.International);

            var result = new ResearchesStatsDTO
            {
                TotalResearchesNumber = total,
                InternalResearches = internalCount,
                ExternalResearches = externalCount
            };

            return new List<ResearchesStatsDTO> { result }.AsQueryable();
        }
    }
}