using Microsoft.AspNetCore.Hosting;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule.Pdf
{
    public class AcademicPdfDocumentCV(CVResponseDTO _cv , IWebHostEnvironment _env) : IDocument
    {
        private readonly TextStyle ArabicStyle = TextStyle.Default.FontFamily("Cairo").FontSize(9);
        private readonly string MainColor = "#19355a";
        private readonly string AccentColor = "#b38e19";
        private readonly string CardBg = "#f8fafc";
        private readonly string BodyBg = "#fdf8ec";

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            var fontPath = Path.Combine(_env.ContentRootPath, "fonts", "Cairo-Regular.ttf");
            FontManager.RegisterFont(File.OpenRead(fontPath));
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(BodyBg);
                page.ContentFromRightToLeft();

                page.Content().Column(mainCol =>
                {
                    mainCol.Item().Height(10).Background(AccentColor);

                    mainCol.Item().Background(MainColor).Row(row =>
                    {
                        row.RelativeItem(55).Padding(30).Column(c =>
                        {
                            c.Item().Text(_cv.NameAr ?? "").FontSize(22).ExtraBold().FontColor(Colors.White).FontFamily("Cairo");
                            if (_cv.Title != null)
                                c.Item().PaddingBottom(5).Text(_cv.Title.ValueAr ?? "").FontSize(11).Bold().FontColor(AccentColor);

                            if (!string.IsNullOrEmpty(_cv.BioSummary))
                                c.Item().Text(_cv.BioSummary).FontSize(8).FontColor("#cbd5e1").LineHeight(1.5f);
                        });


                        row.ConstantItem(1).PaddingVertical(30).Background("#26FFFFFF");

                        row.RelativeItem(42).Padding(30).Column(c =>
                        {
                            if (_cv.Department != null) DrawHeaderMeta(c, "القسم: ", _cv.Department ?? "");
                            if (_cv.Authority != null) DrawHeaderMeta(c, "الجهة: ", _cv.Authority.ValueAr ?? "");
                            if (_cv.University != null) DrawHeaderMeta(c, "الجامعة: ", _cv.University.ValueAr ?? "");

                            c.Item().PaddingTop(5).Column(contactCol => {
                                if (!string.IsNullOrEmpty(_cv.OfficialEmail)) contactCol.Item().Text(_cv.OfficialEmail).Style(ArabicStyle).FontColor(Colors.White);
                                if (!string.IsNullOrEmpty(_cv.MainPhoneNumber)) contactCol.Item().Text($"هاتف: {_cv.MainPhoneNumber}").Style(ArabicStyle).FontColor(Colors.White);
                                if (!string.IsNullOrEmpty(_cv.WorkPhoneNumber)) contactCol.Item().Text($"هاتف عمل: {_cv.WorkPhoneNumber}").Style(ArabicStyle).FontColor(Colors.White);
                                if (!string.IsNullOrEmpty(_cv.FaxNumber)) contactCol.Item().Text($"فاكس: {_cv.FaxNumber}").Style(ArabicStyle).FontColor(Colors.White);
                                if (_cv.BirthDate.HasValue) contactCol.Item().Text($"تاريخ الميلاد: {_cv.BirthDate.Value:yyyy/MM/dd}").FontSize(8).FontColor(Colors.White);
                            });

                            if (_cv.Skills?.Any() == true)
                            {
                                c.Item().PaddingTop(8).Row(r => {
                                    r.Spacing(4);
                                    foreach (var s in _cv.Skills.Take(4))
                                        r.AutoItem().PaddingHorizontal(6).PaddingVertical(2).Background("#33b38e19").Border(0.5f).BorderColor(AccentColor).Text(s ?? "").FontSize(7).Bold().FontColor(AccentColor);
                                });
                            }
                        });
                    });

                    mainCol.Item().Padding(25).Column(contentCol =>
                    {
                        RenderSection(contentCol, "الخبرات العامة", _cv.GeneralExperiences, ge =>
                            (ge.ExperienceTitle ?? "",
                            new[] { ("الجهة", ge.Authority ?? ""), ("البلد/المدينة", ge.CountryOrCity ?? ""), ("الفترة", $"{ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "الخبرات التدريسية", _cv.TeachingExperiences, te =>
                            (te.CourseName ?? "",
                            new[] { ("المستوى الأكاديمي", te.AcademicLevel ?? ""), ("الجامعة/الكلية", te.UniversityOrFaculty ?? ""), ("الفترة", $"{te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المؤهلات العلمية", _cv.AcademicQualifications, aq =>
                            ($"{aq.Qualification?.ValueAr ?? ""}{(string.IsNullOrEmpty(aq.Specialization) ? "" : " — " + aq.Specialization)}",
                            new[] { ("التقدير", aq.Grade?.ValueAr ?? ""), ("الجامعة", aq.UniversityOrFaculty ?? ""), ("البلد/المدينة", aq.CountryOrCity ?? ""), ("تاريخ الحصول", aq.DateOfObtainingTheQualification?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "الدرجات الوظيفية", _cv.JobRanks, jr =>
                            (jr.JobRank?.ValueAr ?? "",
                            new[] { ("تاريخ الدرجة", jr.DateOfJobRank?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "المناصب الإدارية", _cv.AdministrativePositions, ap =>
                            (ap.Position ?? "",
                            new[] { ("الفترة", $"{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المؤتمرات والندوات", _cv.ConferencesAndSeminars, cs =>
                            (cs.Name ?? "",
                            new[] { ("دور المشاركة", cs.RoleOfParticipation?.ValueAr ?? ""), ("الجهة المنظمة", cs.OrganizingAuthority ?? ""), ("مكان الانعقاد", cs.Venue ?? ""), ("الفترة", $"{cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المهمات العلمية", _cv.ScientificMissions, sm =>
                            (sm.MissionName ?? "",
                            new[] { ("الجامعة/الكلية", sm.UniversityOrFaculty ?? ""), ("البلد/المدينة", sm.CountryOrCity ?? ""), ("الفترة", $"{sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "البرامج التدريبية", _cv.TrainingPrograms, tp =>
                            (tp.TrainingProgramName ?? "",
                            new[] { ("مكان الانعقاد", tp.Venue ?? ""), ("الفترة", $"{tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "اللجان والجمعيات", _cv.CommitteesAndAssociations, ca =>
                            (ca.NameOfCommitteeOrAssociation ?? "",
                            new[] { ("النوع", ca.TypeOfCommitteeOrAssociation?.ValueAr ?? ""), ("درجة الاشتراك", ca.DegreeOfSubscription?.ValueAr ?? ""), ("الفترة", $"{ca.StartDate:yyyy/MM/dd} – {(ca.EndDate.HasValue ? ca.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المشاركة في المجلات العلمية", _cv.ParticipationInMagazines, pm =>
                            (pm.NameOfMagazine ?? "",
                            new[] { ("نوع المشاركة", pm.TypeOfParticipation?.ValueAr ?? ""), ("رابط المجلة", pm.WebsiteOfMagazine ?? "") }));

                        RenderSection(contentCol, "تحكيم المقالات", _cv.ReviewingArticles, ra =>
                            (ra.TitleOfArticle ?? "",
                            new[] { ("الجهة", ra.Authority ?? ""), ("تاريخ التحكيم", ra.ReviewingDate?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "المشاريع", _cv.Projects, p =>
                            (p.NameOfProject ?? "",
                            new[] { ("نوع المشروع", p.TypeOfProject?.ValueAr ?? ""), ("دور المشاركة", p.ParticipationRole?.ValueAr ?? ""), ("الفترة", $"{p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المساهمات لخدمة المجتمع", _cv.ContributionsToCommunityService, ctcs =>
                            (ctcs.ContributionTitle ?? "",
                            new[] { ("التاريخ", ctcs.DateOfContribution?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "المساهمات للجامعة", _cv.ContributionsToUniversity, ctu =>
                            (ctu.ContributionTitle ?? "",
                            new[] { ("نوع المساهمة", ctu.TypeOfContribution?.ValueAr ?? ""), ("التاريخ", ctu.DateOfContribution?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "المشاركة في أعمال الجودة", _cv.ParticipationInQualityWork, piqw =>
                            (piqw.ParticipationTitle ?? "",
                            new[] { ("الفترة", $"{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}") }));

                        RenderSection(contentCol, "المؤلفات العلمية", _cv.ScientificWritings, sw =>
                            (sw.Title ?? "",
                            new[] { ("دور المؤلف", sw.AuthorRole?.ValueAr ?? ""), ("دار النشر", sw.PublishingHouse ?? ""), ("ISBN", sw.ISBN ?? ""), ("تاريخ النشر", sw.PublishingDate?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "براءات الاختراع", _cv.Patents, p =>
                            (p.NameOfPatent ?? "",
                            new[] { ("جهة الاعتماد", p.AccreditingAuthorityOrCountry ?? ""), ("تاريخ الاعتماد", p.AccreditationDate?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "الجوائز والمكافآت", _cv.PrizesAndRewards, pr =>
                            (pr.Prize?.ValueAr ?? "",
                            new[] { ("الجهة المانحة", pr.AwardingAuthority ?? ""), ("تاريخ الاستلام", pr.DateReceived?.ToString("yyyy/MM/dd") ?? "") }));

                        RenderSection(contentCol, "مظاهر التقدير العلمي", _cv.ManifestationsOfScientificAppreciation, msa =>
                            (msa.TitleOfAppreciation ?? "",
                            new[] { ("جهة الإصدار", msa.IssuingAuthority ?? ""), ("التاريخ", msa.DateOfAppreciation?.ToString("yyyy/MM/dd") ?? "") }));

                        if (HasSocialMedia())
                        {
                            var socialFields = new List<(string label, string val)>();

                            if (!string.IsNullOrEmpty(_cv.PersonalWebsite)) socialFields.Add(("الموقع الشخصي", _cv.PersonalWebsite));
                            if (!string.IsNullOrEmpty(_cv.LinkedIn)) socialFields.Add(("LinkedIn", _cv.LinkedIn));
                            if (!string.IsNullOrEmpty(_cv.GoogleScholar)) socialFields.Add(("Google Scholar", _cv.GoogleScholar));
                            if (!string.IsNullOrEmpty(_cv.Scopus)) socialFields.Add(("Scopus", _cv.Scopus));
                            if (!string.IsNullOrEmpty(_cv.YouTube)) socialFields.Add(("YouTube", _cv.YouTube));
                            if (!string.IsNullOrEmpty(_cv.Facebook)) socialFields.Add(("Facebook", _cv.Facebook));
                            if (!string.IsNullOrEmpty(_cv.Instagram)) socialFields.Add(("Instagram", _cv.Instagram));
                            if (!string.IsNullOrEmpty(_cv.X)) socialFields.Add(("X (Twitter)", _cv.X));

                            RenderSection(contentCol, "التواصل الاجتماعي", new[] { "SocialMediaCard" }, _ =>
                                ("روابط التواصل", socialFields.ToArray()));
                        }
                    });
                });

                page.Footer().AlignCenter().Text(x => {
                    x.Span("صفحة ").Style(ArabicStyle.FontSize(8));
                    x.CurrentPageNumber().FontSize(8);
                });
            });
        }

        private void DrawHeaderMeta(ColumnDescriptor c, string label, string value)
        {
            c.Item().Text(t => {
                t.Span(label).Bold().FontColor(AccentColor).FontSize(9);
                t.Span(value).FontColor(Colors.White).FontSize(9);
            });
        }

        private void RenderSection<T>(ColumnDescriptor col, string title, IEnumerable<T>? list, Func<T, (string entryTitle, (string label, string val)[] fields)> mapper)
        {
            if (list?.Any() != true) return;

            col.Item().PaddingTop(20).PaddingBottom(10).Row(row => {
                row.AutoItem().PaddingRight(8).Height(3).Width(32).Background(AccentColor);
                row.AutoItem().PaddingRight(8).Text(title).FontSize(10).ExtraBold().FontColor(MainColor);
                row.RelativeItem().PaddingTop(6).Height(1).Background(Colors.Grey.Lighten2);
            });

            foreach (var item in list)
            {
                var (entryTitle, fields) = mapper(item);
                col.Item().PaddingBottom(8).Background(CardBg).Border(1).BorderColor(Colors.Grey.Lighten3).BorderRight(3).BorderColor(AccentColor).Padding(12).Column(c => {
                    if (!string.IsNullOrEmpty(entryTitle))
                        c.Item().PaddingBottom(4).Text(entryTitle).FontSize(9).Bold().FontColor(MainColor);

                    foreach (var f in fields.Where(x => !string.IsNullOrEmpty(x.val)))
                    {
                        c.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).PaddingVertical(2).Row(r => {
                            r.ConstantItem(100).Text(f.label).FontSize(8).Bold().FontColor(AccentColor);
                            r.RelativeItem().Text(f.val).FontSize(8).FontColor("#1e293b");
                        });
                    }
                });
            }
        }

        private bool HasSocialMedia() =>
        !string.IsNullOrEmpty(_cv.PersonalWebsite) ||
        !string.IsNullOrEmpty(_cv.LinkedIn) ||
        !string.IsNullOrEmpty(_cv.GoogleScholar) ||
        !string.IsNullOrEmpty(_cv.Scopus) ||
        !string.IsNullOrEmpty(_cv.YouTube) ||
        !string.IsNullOrEmpty(_cv.Facebook) ||
        !string.IsNullOrEmpty(_cv.Instagram) ||
        !string.IsNullOrEmpty(_cv.X);
    }
}