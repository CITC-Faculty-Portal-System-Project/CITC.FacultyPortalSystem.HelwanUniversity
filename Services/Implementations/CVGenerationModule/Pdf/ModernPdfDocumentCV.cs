using Microsoft.AspNetCore.Hosting;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule.Pdf
{
    public class ModernPdfDocumentCV(CVResponseDTO _cv , IWebHostEnvironment _env) : IDocument
    {
        private readonly TextStyle ArabicStyle = TextStyle.Default.FontFamily("Cairo").FontSize(10);
        private readonly string MainColor = "#19355a";
        private readonly string AccentColor = "#b38e19";
        private readonly string SidebarColor = "#f0f4f8";

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            var fontPath = Path.Combine(_env.ContentRootPath, "fonts", "Cairo-Regular.ttf");
            FontManager.RegisterFont(File.OpenRead(fontPath));
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor("#f8fafc");
                page.ContentFromRightToLeft();

                // الحل الجذري: رسم خلفية الأعمدة في طبقة الـ Background لتمتد لآخر الصفحة الحالية فقط
                page.Background().Row(row =>
                {
                    row.RelativeItem(1).Background(SidebarColor); // خلفية السايد بار (يمين)
                    row.RelativeItem(2).Background(Colors.White);  // خلفية المحتوى (يسار)
                });

                page.Content().Column(mainColumn =>
                {
                    // 1. Header - يظهر مرة واحدة فقط في أول صفحة
                    mainColumn.Item().Background(MainColor).Padding(35).Column(headerCol =>
                    {
                        headerCol.Item().Text(_cv.NameAr).FontSize(24).Bold().FontColor(Colors.White).FontFamily("Cairo");
                        if (_cv.Title != null) headerCol.Item().PaddingBottom(5).Text(_cv.Title.ValueAr).FontSize(13).FontColor(AccentColor).Bold();

                        headerCol.Item().Row(r =>
                        {
                            r.Spacing(8);
                            var st = TextStyle.Default.FontColor("#cbd5e1").FontSize(8).FontFamily("Cairo");
                            if (_cv.Department != null) r.AutoItem().Text(_cv.Department).Style(st);
                            if (_cv.Authority != null) { r.AutoItem().Text("·").Style(st); r.AutoItem().Text(_cv.Authority.ValueAr).Style(st); }
                            if (_cv.University != null) { r.AutoItem().Text("·").Style(st); r.AutoItem().Text(_cv.University.ValueAr).Style(st); }
                            if (_cv.BirthDate.HasValue) { r.AutoItem().Text("·").Style(st); r.AutoItem().Text($"تاريخ الميلاد: {_cv.BirthDate.Value:yyyy/MM/dd}").Style(st); }
                        });
                    });

                    // 2. Body Layout
                    mainColumn.Item().Row(row =>
                    {
                        // --- SIDEBAR (اليمين) ---
                        // ShowOnce تضمن عدم تكرار العناوين والبيانات في الصفحات التالية إذا انتهت
                        row.RelativeItem(1).ShowOnce().Padding(20).Column(sidebarCol =>
                        {
                            DrawSectionTitle(sidebarCol, "بيانات الاتصال");
                            if (!string.IsNullOrEmpty(_cv.OfficialEmail)) DrawContactRow(sidebarCol, "البريد: ", _cv.OfficialEmail);
                            if (!string.IsNullOrEmpty(_cv.MainPhoneNumber)) DrawContactRow(sidebarCol, "الهاتف: ", _cv.MainPhoneNumber);
                            if (!string.IsNullOrEmpty(_cv.WorkPhoneNumber)) DrawContactRow(sidebarCol, "هاتف العمل: ", _cv.WorkPhoneNumber);
                            if (!string.IsNullOrEmpty(_cv.FaxNumber)) DrawContactRow(sidebarCol, "الفاكس: ", _cv.FaxNumber);

                            if (_cv.Skills?.Any() == true)
                            {
                                DrawSectionTitle(sidebarCol, "المهارات");
                                sidebarCol.Item().PaddingTop(5).Column(c =>
                                {
                                    foreach (var s in _cv.Skills)
                                        c.Item().PaddingBottom(3).Text($"• {s}").FontSize(9).Style(ArabicStyle);
                                });
                            }

                            if (HasSocialMedia())
                            {
                                DrawSectionTitle(sidebarCol, "التواصل الاجتماعي");
                                if (!string.IsNullOrEmpty(_cv.PersonalWebsite)) DrawSocialRow(sidebarCol, "الموقع الشخصي", _cv.PersonalWebsite);
                                if (!string.IsNullOrEmpty(_cv.LinkedIn)) DrawSocialRow(sidebarCol, "LinkedIn", _cv.LinkedIn);
                                if (!string.IsNullOrEmpty(_cv.GoogleScholar)) DrawSocialRow(sidebarCol, "Scholar", _cv.GoogleScholar);
                                if (!string.IsNullOrEmpty(_cv.YouTube)) DrawSocialRow(sidebarCol, "Youtube", _cv.YouTube);
                                if (!string.IsNullOrEmpty(_cv.Facebook)) DrawSocialRow(sidebarCol, "Facebook", _cv.Facebook);
                                if (!string.IsNullOrEmpty(_cv.X)) DrawSocialRow(sidebarCol, "X", _cv.X);
                            }
                        });

                        // --- MAIN CONTENT (اليسار) ---
                        row.RelativeItem(2).Padding(25).Column(contentCol =>
                        {
                            if (!string.IsNullOrEmpty(_cv.BioSummary))
                            {
                                DrawSectionTitle(contentCol, "نبذة تعريفية");
                                contentCol.Item().PaddingBottom(10).Text(_cv.BioSummary).FontSize(10).LineHeight(1.6f);
                            }

                            RenderList(contentCol, "الخبرات العامة", _cv.GeneralExperiences, ge => (ge.ExperienceTitle ?? "", $"{ge.Authority} · {ge.CountryOrCity} · {ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "الخبرات التدريسية", _cv.TeachingExperiences, te => (te.CourseName ?? "", $"{te.AcademicLevel ?? ""} · {te.UniversityOrFaculty} · {te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المؤهلات العلمية", _cv.AcademicQualifications, aq => ($"{aq.Qualification?.ValueAr ?? ""}{(string.IsNullOrEmpty(aq.Specialization) ? "" : " — " + aq.Specialization)}", $"{aq.Grade?.ValueAr ?? ""} · {aq.UniversityOrFaculty} · {aq.CountryOrCity} · {aq.DateOfObtainingTheQualification:yyyy/MM/dd}"));
                            RenderList(contentCol, "الدرجات الوظيفية", _cv.JobRanks, jr => (jr.JobRank?.ValueAr ?? "", jr.DateOfJobRank?.ToString("yyyy/MM/dd") ?? ""));
                            RenderList(contentCol, "المناصب الإدارية", _cv.AdministrativePositions, ap => (ap.Position ?? "", $"{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المؤتمرات والندوات", _cv.ConferencesAndSeminars, cs => (cs.Name ?? "", $"{cs.RoleOfParticipation?.ValueAr ?? ""} · {cs.OrganizingAuthority} · {cs.Venue} · {cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المهمات العلمية", _cv.ScientificMissions, sm => (sm.MissionName ?? "", $"{sm.UniversityOrFaculty} · {sm.CountryOrCity} · {sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "البرامج التدريبية", _cv.TrainingPrograms, tp => (tp.TrainingProgramName ?? "", $"{tp.Venue} · {tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "اللجان والجمعيات", _cv.CommitteesAndAssociations, ca => (ca.NameOfCommitteeOrAssociation ?? "", $"{ca.TypeOfCommitteeOrAssociation?.ValueAr ?? ""} . {ca.DegreeOfSubscription?.ValueAr ?? ""} · {ca.StartDate:yyyy/MM/dd} – {(ca.EndDate.HasValue ? ca.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المشاركة في المجلات العلمية", _cv.ParticipationInMagazines, pm => (pm.NameOfMagazine ?? "", $"{pm.TypeOfParticipation?.ValueAr ?? ""} · {pm.WebsiteOfMagazine}"));
                            RenderList(contentCol, "تحكيم المقالات", _cv.ReviewingArticles, ra => (ra.TitleOfArticle ?? "", $"{ra.Authority} · {ra.ReviewingDate:yyyy/MM/dd}"));
                            RenderList(contentCol, "المشاريع", _cv.Projects, p => (p.NameOfProject ?? "", $"{p.TypeOfProject?.ValueAr} · {p.ParticipationRole?.ValueAr ?? ""} · {p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المساهمات لخدمة المجتمع", _cv.ContributionsToCommunityService, ctcs => (ctcs.ContributionTitle ?? "", ctcs.DateOfContribution?.ToString("yyyy/MM/dd") ?? ""));
                            RenderList(contentCol, "المساهمات للجامعة", _cv.ContributionsToUniversity, ctu => (ctu.ContributionTitle ?? "", $"{ctu.TypeOfContribution?.ValueAr ?? ""} · {ctu.DateOfContribution:yyyy/MM/dd}"));
                            RenderList(contentCol, "المشاركة في اعمال الجودة", _cv.ParticipationInQualityWork, piqw => (piqw.ParticipationTitle ?? "", $"{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));
                            RenderList(contentCol, "المؤلفات العلمية", _cv.ScientificWritings, sw => (sw.Title ?? "", $"{sw.AuthorRole?.ValueAr ?? ""} · {sw.PublishingHouse} · ISBN: {sw.ISBN} · {sw.PublishingDate:yyyy/MM/dd}"));
                            RenderList(contentCol, "براءات الاختراع", _cv.Patents, p => (p.NameOfPatent ?? "", $"{p.AccreditingAuthorityOrCountry} · {p.AccreditationDate?.ToString("yyyy/MM/dd") ?? ""}"));
                            RenderList(contentCol, "الجوائز والمكافآت", _cv.PrizesAndRewards, pr => (pr.Prize?.ValueAr ?? "", $"{pr.AwardingAuthority} · {pr.DateReceived:yyyy/MM/dd}"));
                            RenderList(contentCol, "مظاهر التقدير العلمي", _cv.ManifestationsOfScientificAppreciation, msa => (msa.TitleOfAppreciation ?? "", $"{msa.IssuingAuthority} · {msa.DateOfAppreciation?.ToString("yyyy/MM/dd") ?? ""}"));
                        });
                    });
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("صفحة ").Style(ArabicStyle.FontSize(8));
                    x.CurrentPageNumber().FontSize(8);
                });
            });
        }

        private void DrawSectionTitle(ColumnDescriptor column, string title)
        {
            column.Item().PaddingTop(15).PaddingBottom(10).Row(row => {
                row.AutoItem().BorderRight(4).BorderColor(AccentColor).PaddingRight(10).Text(title).FontSize(11).Bold().FontColor(MainColor).FontFamily("Cairo");
            });
        }

        private void DrawContactRow(ColumnDescriptor column, string label, string value)
        {
            column.Item().PaddingBottom(5).Text(t => {
                t.Span(label).FontSize(8).FontColor("#64748b").Bold();
                t.Span(value).FontSize(8).FontColor("#1e293b");
            });
        }

        private void DrawSocialRow(ColumnDescriptor column, string label, string value)
        {
            column.Item().PaddingBottom(2).Text(t => {
                t.Span($"{label}: ").FontSize(8).FontColor(MainColor).Bold();
                t.Span(value).FontSize(8).FontColor("#1e293b");
            });
        }

        private void RenderList<T>(ColumnDescriptor col, string title, List<T> list, Func<T, (string title, string meta)> mapper)
        {
            if (list?.Any() != true) return;
            DrawSectionTitle(col, title);
            foreach (var item in list)
            {
                var (t, m) = mapper(item);
                col.Item().PaddingBottom(8).BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Column(c => {
                    c.Item().Text(t).FontSize(10).Bold().Style(ArabicStyle);
                    c.Item().Text(m).FontSize(8).FontColor(Colors.Grey.Medium);
                });
            }
        }

        private bool HasSocialMedia() => !string.IsNullOrEmpty(_cv.PersonalWebsite) || !string.IsNullOrEmpty(_cv.LinkedIn) || !string.IsNullOrEmpty(_cv.YouTube) || !string.IsNullOrEmpty(_cv.Facebook);
    }
}