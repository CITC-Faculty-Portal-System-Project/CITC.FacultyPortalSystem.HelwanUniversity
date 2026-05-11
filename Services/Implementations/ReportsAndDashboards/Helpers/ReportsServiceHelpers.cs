using Shared.Dtos.ReportsAndDashboard;
using System.Text;

namespace Services.ReportsAndDashboard.Helpers
{
    public static class ReportsServiceHelpers
    {
        public static string GenerateDashboardAnalysis(ResearchesDashboardDTO dashboard)
        {
            var analysis = new StringBuilder();

            analysis.AppendLine($@"
        بلغ إجمالي عدد الأبحاث الدولية {dashboard.InternationalResearchesNo} بحثًا،
        بينما بلغ عدد الأبحاث المحلية {dashboard.LocalResearchesNo} بحثًا،
        بإجمالي اهتمامات بحثية وصل إلى {dashboard.TotalNumberOfInterests}
        اهتمامًا موزعة على {dashboard.TotalDepartments} قسمًا أكاديميًا.");

            var totalResearches = dashboard.InternationalResearchesNo + dashboard.LocalResearchesNo;

            if (totalResearches > 0)
            {
                double internationalPercentage =
                    (dashboard.InternationalResearchesNo * 100.0) / totalResearches;

                double localPercentage =
                    (dashboard.LocalResearchesNo * 100.0) / totalResearches;

                analysis.AppendLine($@"
            تمثل الأبحاث الدولية نسبة
            {internationalPercentage:F1}%
            من إجمالي الإنتاج البحثي،
            بينما تمثل الأبحاث المحلية نسبة
            {localPercentage:F1}%.");
            }

            if (dashboard.UniversityTopFiveResearchers.Any())
            {
                var topResearcher = dashboard.UniversityTopFiveResearchers
                    .OrderByDescending(x => x.TotalResearchesNo)
                    .First();

                analysis.AppendLine($@"
            يُعد الباحث
            {topResearcher.ResearcherNameAR}
            من أبرز الباحثين على مستوى الجامعة،
            حيث بلغ عدد أبحاثه
            {topResearcher.TotalResearchesNo}
            بحثًا بدرجة تقييم
            {topResearcher.Score:F2}.");

                analysis.AppendLine("وضمت قائمة الباحثين الأكثر نشاطًا:");

                foreach (var researcher in dashboard.UniversityTopFiveResearchers)
                {
                    analysis.AppendLine($@"
                - {researcher.ResearcherNameAR}
                - {researcher.DepartmentAR}
                - عدد الأبحاث: {researcher.TotalResearchesNo}");
                }
            }

            if (dashboard.TopFiveResearchersInterestsStats.Any())
            {
                var topInterest = dashboard.TopFiveResearchersInterestsStats
                    .OrderByDescending(x => x.ResearchersNumber)
                    .First();

                analysis.AppendLine($@"
            جاء مجال
            {topInterest.InterestName}
            كأكثر المجالات البحثية اهتمامًا،
            حيث يعمل به
            {topInterest.ResearchersNumber}
            باحثًا.");

                analysis.AppendLine("أكثر الاهتمامات البحثية انتشارًا:");

                foreach (var interest in dashboard.TopFiveResearchersInterestsStats)
                {
                    analysis.AppendLine($@"
                - {interest.InterestName}
                - عدد الباحثين: {interest.ResearchersNumber}");
                }
            }

            if (dashboard.CitationsStats.Any())
            {
                var citations = dashboard.CitationsStats.First();

                analysis.AppendLine($@"
            بلغ إجمالي عدد الاستشهادات العلمية
            {citations.TotalCitationsNo}
            استشهادًا علميًا،
            مما يعكس التأثير الأكاديمي للإنتاج البحثي.");

                if (citations.DetailedCitesStats.Any())
                {
                    var highestYear = citations.DetailedCitesStats
                        .OrderByDescending(x => x.TotalCites)
                        .First();

                    analysis.AppendLine($@"
                سجل عام
                {highestYear.Year}
                أعلى عدد من الاستشهادات بإجمالي
                {highestYear.TotalCites}
                استشهادًا.");
                }
            }

            analysis.AppendLine(@"
        تشير المؤشرات العامة إلى وجود نشاط بحثي متنامٍ داخل الجامعة،
        مع تنوع واضح في المجالات البحثية وارتفاع مساهمة الباحثين
        في النشر العلمي والاستشهادات الأكاديمية.");

            return analysis.ToString();
        }


        public static string GenerateOperationalNotes(AdminDashboardResponseDTO stats)
        {
            var notes = new StringBuilder();

            var topFacultyUsers = stats.UsersPerFaculty
                .OrderByDescending(x => x.TotalNumberOfUsers)
                .FirstOrDefault();

            if (topFacultyUsers is not null)
            {
                notes.Append($@"
        • سجلت كلية {topFacultyUsers.FacultyNameAR}
        أعلى عدد مستخدمين بإجمالي
        {topFacultyUsers.TotalNumberOfUsers} مستخدمًا.\n");
            }

            if (stats.TotalUsersNumber > 0)
            {
                double managersRatio =
                    (stats.TotalSystemManagersNumber * 100.0) /
                    stats.TotalUsersNumber;

                notes.Append($@"
        • تمثل حسابات مديري النظام نسبة
        {managersRatio:F1}%
        من إجمالي مستخدمي البوابة.\n");
            }


            var totalTickets =
                stats.TicketsStats.OpenedTicketsNo +
                stats.TicketsStats.ClosedTicketsNo;

            if (totalTickets > 0)
            {
                double solvedRate =
                    (stats.TicketsStats.ClosedTicketsNo * 100.0) /
                    totalTickets;

                notes.Append($@"
        • بلغ معدل معالجة المشكلات الفنية
        {solvedRate:F1}%
        من إجمالي الطلبات المسجلة.\n");
            }


            var topProblemModule = stats.TicketsStats.ModulesProblems
                .OrderByDescending(x => x.NumberOfProblems)
                .FirstOrDefault();

            if (topProblemModule is not null)
            {
                notes.Append($@"
        • أكثر الوحدات تعرضًا للمشكلات الفنية هي
        {topProblemModule.ModuleName}
        بعدد
        {topProblemModule.NumberOfProblems}
        مشكلة مسجلة.\n");
            }


            var highPriority = stats.TicketsStats.TicketsPriorityStats
                .OrderByDescending(x => x.NumberOfTickets)
                .FirstOrDefault();

            if (highPriority is not null)
            {
                notes.Append($@"
        • جاءت التذاكر ذات الأولوية
        {highPriority.PriorityName}
        كأعلى فئة من حيث عدد البلاغات بعدد
        {highPriority.NumberOfTickets}
        تذكرة.\n");
            }

            return notes.ToString();
        }



        public static string GenerateScientificRecommendations(AdminDashboardResponseDTO stats)
        {
            var recommendations = new StringBuilder();

            recommendations.Append($@"
    • بلغ إجمالي الإنتاج البحثي داخل النظام
    {stats.ResearchesStats.TotalResearchesNumber}
    بحثًا علميًا، مما يعكس نشاطًا أكاديميًا ملحوظًا.\n");

            if (stats.ResearchesStats.TotalResearchesNumber > 0)
            {
                double externalRatio =
                    (stats.ResearchesStats.ExternalResearches * 100.0) /
                    stats.ResearchesStats.TotalResearchesNumber;

                recommendations.Append($@"
        • يوصى بالتوسع في دعم الأبحاث الخارجية،
        حيث تمثل حاليًا نسبة
        {externalRatio:F1}%
        من إجمالي الأبحاث المسجلة.\n");
            }
            var topFacultyResearch = stats.ResearchesPerFaculty
                .OrderByDescending(x => x.TotalNumberOfResearches)
                .FirstOrDefault();

            if (topFacultyResearch is not null)
            {
                recommendations.Append($@"
        • سجلت كلية
        {topFacultyResearch.FacultyNameAR}
        أعلى معدل إنتاج بحثي بعدد
        {topFacultyResearch.TotalNumberOfResearches}
        بحثًا، ويوصى بتعميم التجارب الناجحة الخاصة بها على باقي الكليات.\n");
            }

            if (stats.ResearchesMonthlyRate.Any())
            {
                var bestMonth = stats.ResearchesMonthlyRate
                    .OrderByDescending(x => x.TotalNumberOfResearches)
                    .First();

                recommendations.Append($@"
        • شهد شهر
        {bestMonth.MonthAR}
        أعلى معدل نشر علمي بإجمالي
        {bestMonth.TotalNumberOfResearches}
        بحثًا.\n");
            }


            recommendations.Append(@"
    • ضرورة تحديث قواعد بيانات المجلات العلمية وربطها بمنصات الفهرسة العالمية.
    • التوسع في برامج دعم الباحثين والنشر الدولي لتحسين التصنيف الأكاديمي للجامعة.
    • تشجيع التعاون البحثي بين الكليات المختلفة لزيادة الإنتاج العلمي المشترك.");

            return recommendations.ToString();
        }

        public static string GenerateTableRows<T>(
    IEnumerable<T> data,
    Func<T, string> rowTemplate)
        {
            var sb = new StringBuilder();

            foreach (var item in data)
            {
                sb.Append(rowTemplate(item));
            }

            return sb.ToString();
        }

    }
}