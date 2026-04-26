using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchesMonthlyRateSpecification
        : AggregationSpecification<Research, ResearchesMonthlyRateDTO>
    {
        public ResearchesMonthlyRateSpecification()
        {
            SetCriteria(r =>
                !r.IsDeleted &&
                r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<ResearchesMonthlyRateDTO> Apply(IQueryable<Research> query)
        {
            var currentYear = DateTime.Now.Year;

            if (Criteria != null)
                query = query.Where(Criteria);

            var monthlyData = query
                .Where(r => r.CreatedAt.Year == currentYear)
                .GroupBy(r => r.CreatedAt.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                });

            var months = Enumerable.Range(1, 12).Select(m => new
            {
                Month = m
            });

            var result = from m in months
                         join d in monthlyData
                         on m.Month equals d.Month into gj
                         from sub in gj.DefaultIfEmpty()
                         select new ResearchesMonthlyRateDTO
                         {
                             MonthEN = GetMonthNameEN(m.Month),
                             MonthAR = GetMonthNameAR(m.Month),
                             TotalNumberOfResearches = sub != null ? sub.Count : 0
                         };

            return result.AsQueryable();
        }

        private static string GetMonthNameEN(int month) => month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => ""
        };

        private static string GetMonthNameAR(int month) => month switch
        {
            1 => "يناير",
            2 => "فبراير",
            3 => "مارس",
            4 => "أبريل",
            5 => "مايو",
            6 => "يونيو",
            7 => "يوليو",
            8 => "أغسطس",
            9 => "سبتمبر",
            10 => "أكتوبر",
            11 => "نوفمبر",
            12 => "ديسمبر",
            _ => ""
        };
    }
}