using Microsoft.AspNetCore.Hosting;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Services.Abstraction.Contracts.AttachmentsModule;
using Shared.Dtos.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule.Pdf
{
    public class ProfessionalPdfDocumentCV : IDocument
    {
        private readonly TextStyle ArabicStyle = TextStyle.Default.FontFamily("Cairo").FontSize(9);
        private readonly string MainColor = "#19355a"; 
        private readonly string AccentColor = "#b38e19"; 
        private readonly string SidebarText = "#cbd5e1";
        private CVResponseDTO _cv;
        private IWebHostEnvironment _env;
        private byte[]? _profileImage;
        private const float SidebarWidth = 200; 

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public ProfessionalPdfDocumentCV(CVResponseDTO cv, IAttachmentService attachmentService , IWebHostEnvironment env)
        {
            _cv = cv;
            _env = env;

            if (cv.ProfilePictureId != null)
            {
                var image = attachmentService
                    .GetAsync(
                        Abstraction.Enums.AttachmentContext.ProfilePicture,
                        cv.PersonalDataId,
                        cv.ProfilePictureId.Value)
                    .GetAwaiter()
                    .GetResult();

                _profileImage = image?.AttachmentData;
            }
        }
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0); 
                page.ContentFromRightToLeft();

                page.Background().Row(row =>
                {
                    row.ConstantItem(SidebarWidth).Background(MainColor);
                    row.RelativeItem().Background(Colors.White);
                });

                page.Content().Row(row =>
                {
                    row.ConstantItem(SidebarWidth).PaddingVertical(40).PaddingHorizontal(20).Column(sb =>
                    {
                        sb.Item()
                     .AlignCenter()
                     .Width(80)
                     .Height(80)
                     .Background("#4d5a6d")
                     .Border(2)
                     .BorderColor(AccentColor)
                     .CornerRadius(40)
                     .AlignCenter()
                     .AlignMiddle()
                     .Element(container =>
                     {
                         if (_cv.ProfilePictureId != null) 
                         {
                             container
                                     .Image(_profileImage)
                                      .FitArea();
                         }
                         else
                         {
                             container
                                 .Text(GetInitials(_cv.NameAr))
                                 .FontSize(24)
                                 .Bold()
                                 .FontColor(AccentColor);
                         }
                     });
                        sb.Item().PaddingTop(15).AlignCenter().Text(_cv.NameAr ?? "").FontSize(16).ExtraBold().FontColor(Colors.White);

                        if (_cv.Title != null)
                            sb.Item().AlignCenter().Text(_cv.Title.ValueAr ?? "").FontSize(9).SemiBold().FontColor(AccentColor);

                        DrawSidebarSection(sb, "بيانات الاتصال");
                        if (!string.IsNullOrEmpty(_cv.OfficialEmail)) DrawSidebarText(sb, _cv.OfficialEmail);
                        if (!string.IsNullOrEmpty(_cv.MainPhoneNumber)) DrawSidebarText(sb, $"هاتف: {_cv.MainPhoneNumber}");
                        if (!string.IsNullOrEmpty(_cv.WorkPhoneNumber)) DrawSidebarText(sb, $"هاتف عمل: {_cv.WorkPhoneNumber}");
                        if (!string.IsNullOrEmpty(_cv.FaxNumber)) DrawSidebarText(sb, $"فاكس: {_cv.FaxNumber}");
                        if (_cv.BirthDate.HasValue) DrawSidebarText(sb, $"تاريخ الميلاد: {_cv.BirthDate.Value:yyyy/MM/dd}");

                        if (_cv.Skills?.Any() == true)
                        {
                            DrawSidebarSection(sb, "المهارات");
                            sb.Item().PaddingTop(5).Row(r =>
                            {
                                r.Spacing(4);
                                foreach (var s in _cv.Skills)
                                    r.AutoItem().PaddingHorizontal(6).PaddingVertical(2).Background("#33b38e19").Border(0.5f).BorderColor(AccentColor).Text(s ?? "").FontSize(7).FontColor(AccentColor);
                            });
                        }

                        if (HasSocialMedia())
                        {
                            DrawSidebarSection(sb, "التواصل الاجتماعي");

                            if (!string.IsNullOrEmpty(_cv.PersonalWebsite)) DrawSidebarText(sb, _cv.PersonalWebsite);
                            if (!string.IsNullOrEmpty(_cv.LinkedIn)) DrawSidebarText(sb, $"LinkedIn: {_cv.LinkedIn}");
                            if (!string.IsNullOrEmpty(_cv.GoogleScholar)) DrawSidebarText(sb, $"Scholar: {_cv.GoogleScholar}");
                            if (!string.IsNullOrEmpty(_cv.Scopus)) DrawSidebarText(sb, $"Scopus: {_cv.Scopus}");
                            if (!string.IsNullOrEmpty(_cv.YouTube)) DrawSidebarText(sb, $"YouTube: {_cv.YouTube}");
                            if (!string.IsNullOrEmpty(_cv.Facebook)) DrawSidebarText(sb, $"Facebook: {_cv.Facebook}");
                            if (!string.IsNullOrEmpty(_cv.Instagram)) DrawSidebarText(sb, $"Instagram: {_cv.Instagram}");
                            if (!string.IsNullOrEmpty(_cv.X)) DrawSidebarText(sb, $"X: {_cv.X}");
                        }
                    });

                    row.RelativeItem().PaddingVertical(40).PaddingHorizontal(30).Column(mainCol =>
                    {
                        if (_cv.Department != null || _cv.University != null || _cv.Authority != null)
                        {
                            mainCol.Item().PaddingBottom(20).Background("#f0f4f8").Padding(12).Text(t =>
                            {
                                var parts = new List<string>();
                                if (_cv.Department != null) parts.Add(_cv.Department ?? "");
                                if (_cv.Authority != null) parts.Add(_cv.Authority.ValueAr ?? "");
                                if (_cv.University != null) parts.Add(_cv.University.ValueAr ?? "");

                                t.Span(string.Join(" · ", parts)).FontSize(9).FontColor("#334155").SemiBold();
                            });
                        }

                        if (!string.IsNullOrEmpty(_cv.BioSummary))
                        {
                            DrawMainTitle(mainCol, "نبذة تعريفية");
                            mainCol.Item().Text(_cv.BioSummary).FontSize(9).LineHeight(1.6f).FontColor("#374151");
                        }

                        RenderMainList(mainCol, "الخبرات العامة", _cv.GeneralExperiences, ge =>
                            (ge.ExperienceTitle ?? "",
                             $"{ge.Authority ?? ""} · {ge.CountryOrCity ?? ""} · {ge.StartDate:yyyy/MM/dd} – {(ge.EndDate.HasValue ? ge.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "الخبرات التدريسية", _cv.TeachingExperiences, te =>
                            (te.CourseName ?? "",
                             $"{te.AcademicLevel ?? ""} · {te.UniversityOrFaculty ?? ""} · {te.StartDate:yyyy/MM/dd} – {(te.EndDate.HasValue ? te.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المؤهلات العلمية", _cv.AcademicQualifications, aq =>
                            ($"{aq.Qualification?.ValueAr ?? ""} — {aq.Specialization ?? ""}",
                             $"{aq.Grade?.ValueAr ?? ""} · {aq.UniversityOrFaculty ?? ""} · {aq.CountryOrCity ?? ""} · {aq.DateOfObtainingTheQualification:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "الدرجات الوظيفية", _cv.JobRanks, jr =>
                            (jr.JobRank?.ValueAr ?? "",
                             jr.DateOfJobRank?.ToString("yyyy/MM/dd") ?? ""));

                        RenderMainList(mainCol, "المناصب الإدارية", _cv.AdministrativePositions, ap =>
                            (ap.Position ?? "",
                             $"{ap.StartDate:yyyy/MM/dd} – {(ap.EndDate.HasValue ? ap.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المؤتمرات والندوات", _cv.ConferencesAndSeminars, cs =>
                            (cs.Name ?? "",
                             $"{cs.RoleOfParticipation?.ValueAr ?? ""} · {cs.OrganizingAuthority ?? ""} · {cs.Venue ?? ""} · {cs.StartDate:yyyy/MM/dd} – {(cs.EndDate.HasValue ? cs.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المهمات العلمية", _cv.ScientificMissions, sm =>
                            (sm.MissionName ?? "",
                             $"{sm.UniversityOrFaculty ?? ""} · {sm.CountryOrCity ?? ""} · {sm.StartDate:yyyy/MM/dd} – {(sm.EndDate.HasValue ? sm.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "البرامج التدريبية", _cv.TrainingPrograms, tp =>
                            (tp.TrainingProgramName ?? "",
                             $"{tp.Venue ?? ""} · {tp.StartDate:yyyy/MM/dd} – {(tp.EndDate.HasValue ? tp.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "اللجان والجمعيات", _cv.CommitteesAndAssociations, ca =>
                            (ca.NameOfCommitteeOrAssociation ?? "",
                             $"{ca.TypeOfCommitteeOrAssociation?.ValueAr ?? ""} . {ca.DegreeOfSubscription?.ValueAr ?? ""} · {ca.StartDate:yyyy/MM/dd} – {(ca.EndDate.HasValue ? ca.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المشاركة في المجلات العلمية", _cv.ParticipationInMagazines, pm =>
                            (pm.NameOfMagazine ?? "",
                             $"{pm.TypeOfParticipation?.ValueAr ?? ""} · {pm.WebsiteOfMagazine ?? ""}"));

                        RenderMainList(mainCol, "تحكيم المقالات", _cv.ReviewingArticles, ra =>
                            (ra.TitleOfArticle ?? "",
                             $"{ra.Authority ?? ""} · {ra.ReviewingDate:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "المشاريع", _cv.Projects, p =>
                            (p.NameOfProject ?? "",
                             $"{p.TypeOfProject?.ValueAr ?? ""} · {p.ParticipationRole?.ValueAr ?? ""} · {p.StartDate:yyyy/MM/dd} – {(p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المساهمات لخدمة المجتمع", _cv.ContributionsToCommunityService, ctcs =>
                            (ctcs.ContributionTitle ?? "",
                             ctcs.DateOfContribution?.ToString("yyyy/MM/dd") ?? ""));

                        RenderMainList(mainCol, "المساهمات للجامعة", _cv.ContributionsToUniversity, ctu =>
                            (ctu.ContributionTitle ?? "",
                             $"{ctu.TypeOfContribution?.ValueAr ?? ""} · {ctu.DateOfContribution:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "المشاركة في أعمال الجودة", _cv.ParticipationInQualityWork, piqw =>
                            (piqw.ParticipationTitle ?? "",
                             $"{piqw.StartDate:yyyy/MM/dd} – {(piqw.EndDate.HasValue ? piqw.EndDate.Value.ToString("yyyy/MM/dd") : "الآن")}"));

                        RenderMainList(mainCol, "المؤلفات العلمية", _cv.ScientificWritings, sw =>
                            (sw.Title ?? "",
                             $"{sw.AuthorRole?.ValueAr ?? ""} · {sw.PublishingHouse ?? ""} · ISBN: {sw.ISBN ?? ""} · {sw.PublishingDate:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "براءات الاختراع", _cv.Patents, p =>
                            (p.NameOfPatent ?? "",
                             $"{p.AccreditingAuthorityOrCountry ?? ""} · {p.AccreditationDate:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "الجوائز والمكافآت", _cv.PrizesAndRewards, pr =>
                            (pr.Prize?.ValueAr ?? "",
                             $"{pr.AwardingAuthority ?? ""} · {pr.DateReceived:yyyy/MM/dd}"));

                        RenderMainList(mainCol, "مظاهر التقدير العلمي", _cv.ManifestationsOfScientificAppreciation, msa =>
                            (msa.TitleOfAppreciation ?? "",
                             $"{msa.IssuingAuthority ?? ""} · {msa.DateOfAppreciation:yyyy/MM/dd}"));
                    });
                });

                page.Footer().PaddingRight(280).PaddingBottom(20).Text(x =>
                {
                    x.Span("صفحة ").Style(ArabicStyle.FontSize(8)).FontColor(Colors.Grey.Medium);
                    x.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void DrawSidebarSection(ColumnDescriptor sb, string title)
        {
            sb.Item().PaddingTop(20).PaddingBottom(10).BorderBottom(1).BorderColor("#26FFFFFF").PaddingBottom(5).Text(title).FontSize(8).Bold().FontColor(AccentColor);
        }

        private void DrawSidebarText(ColumnDescriptor sb, string text)
        {
            sb.Item().PaddingTop(2).Text(text).FontSize(8).FontColor(SidebarText);
        }

        private void DrawMainTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(22).BorderBottom(1.5f).BorderColor(MainColor).PaddingBottom(5).Text(title).FontSize(10).ExtraBold().FontColor(MainColor);
        }

        private void RenderMainList<T>(ColumnDescriptor col, string title, IEnumerable<T>? list, Func<T, (string head, string sub)> mapper)
        {
            if (list?.Any() != true) return;
            DrawMainTitle(col, title);
            foreach (var item in list)
            {
                var (h, s) = mapper(item);
                col.Item().PaddingTop(8).Column(c =>
                {
                    c.Item().Text(h).FontSize(9).Bold().FontColor("#1e293b");
                    c.Item().PaddingTop(2).Text(s).FontSize(8).FontColor("#64748b").LineHeight(1.3f);
                });
            }
        }

        private string GetInitials(string? name) => string.IsNullOrWhiteSpace(name) ? "" : name.Substring(0, 1).ToUpper();

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