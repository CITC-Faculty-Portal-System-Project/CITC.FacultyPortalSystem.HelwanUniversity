using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.CVGenerationModule;

namespace Services.Implementations.CVGenerationModule.Pdf
{
    public class ModernPdfDocumentCV(CVResponseDTO _cv) : IDocument
    {
        // إعدادات الفونت والـ RTL
        private readonly TextStyle ArabicStyle = TextStyle.Default.FontFamily("Cairo").FontSize(10);
        private readonly string MainColor = "#19355a";
        private readonly string AccentColor = "#b38e19";

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20); // الهامش الخارجي للصفحة (الخلفية الرمادية)
                page.PageColor("#f8fafc"); // نفس لون body في الـ HTML
                page.ContentFromRightToLeft();

                page.Content().Decoration(decoration =>
                {
                    // رسم الـ Box الأبيض (الـ cv-wrap)
                    decoration.Before().PaddingBottom(0).Background(Colors.White);

                    decoration.Content().Column(mainCol =>
                    {
                        // --- 1. HEADER (نفس الألوان والبادنج) ---
                        mainCol.Item().Background("#19355a").Padding(35).Column(headerCol =>
                        {
                            headerCol.Item().Text(_cv.Name).FontSize(24).Bold().FontColor(Colors.White).FontFamily("Cairo");

                            if (_cv.Title != null)
                                headerCol.Item().PaddingBottom(5).Text(_cv.Title.ValueAr).FontSize(13).FontColor("#b38e19").Bold().FontFamily("Cairo");

                            headerCol.Item().Row(row =>
                            {
                                row.Spacing(8);
                                var metaStyle = TextStyle.Default.FontColor("#cbd5e1").FontSize(9).FontFamily("Cairo");

                                if (_cv.Department != null) row.AutoItem().Text(_cv.Department.ValueAr).Style(metaStyle);
                                if (_cv.University != null) { row.AutoItem().Text("·").Style(metaStyle); row.AutoItem().Text(_cv.University.ValueAr).Style(metaStyle); }
                                if (_cv.Authority != null) { row.AutoItem().Text("·").Style(metaStyle); row.AutoItem().Text(_cv.Authority.ValueAr).Style(metaStyle); }
                            });
                        });

                        // --- 2. BODY (تقسيم Sidebar و Main) ---
                        mainCol.Item().Row(row =>
                        {
                            // --- Main Content (68%) ---
                            row.RelativeItem(2).Padding(25).Column(col =>
                            {
                                if (!string.IsNullOrEmpty(_cv.BioSummary))
                                {
                                    DrawSectionTitle(col, "نبذة تعريفية");
                                    col.Item().PaddingBottom(15).Text(_cv.BioSummary).FontSize(10).LineHeight(1.6f).FontColor("#374151");
                                }

                                if (_cv.AcademicQualifications?.Any() == true)
                                {
                                    DrawSectionTitle(col, "المؤهلات العلمية");
                                    foreach (var aq in _cv.AcademicQualifications)
                                    {
                                        DrawEntry(col,
                                            $"{aq.Qualification?.ValueAr} {(string.IsNullOrEmpty(aq.Specialization) ? "" : " — " + aq.Specialization)}",
                                            $"{aq.Grade?.ValueAr} · {aq.UniversityOrFaculty} · {aq.CountryOrCity} · {aq.DateOfObtainingTheQualification:yyyy/MM/dd}");
                                    }
                                }

                                if (_cv.JobRanks?.Any() == true)
                                {
                                    DrawSectionTitle(col, "الدرجات الوظيفية");
                                    foreach (var jr in _cv.JobRanks)
                                    {
                                        DrawEntry(col, jr.JobRank?.ValueAr ?? "", jr.DateOfJobRank?.ToString("yyyy/MM/dd") ?? "");
                                    }
                                }
                            });

                            // --- Sidebar (32%) ---
                            row.RelativeItem(1).Background("#f0f4f8").ExtendVertical().Padding(20).Column(col =>
                            {
                                DrawSectionTitle(col, "بيانات الاتصال");
                                if (!string.IsNullOrEmpty(_cv.OfficialEmail)) DrawContactRow(col, "البريد: ", _cv.OfficialEmail);
                                if (!string.IsNullOrEmpty(_cv.MainPhoneNumber)) DrawContactRow(col, "الهاتف: ", _cv.MainPhoneNumber);
                                if (!string.IsNullOrEmpty(_cv.WorkPhoneNumber)) DrawContactRow(col, "العمل: ", _cv.WorkPhoneNumber);

                                if (_cv.Skills?.Any() == true)
                                {
                                    DrawSectionTitle(col, "المهارات");
                                    col.Item().PaddingTop(5).Row(r => {
                                        r.RelativeItem().PaddingTop(5).Column(c => {
                                            foreach (var s in _cv.Skills)
                                                c.Item().PaddingBottom(3).Background(Colors.White).PaddingHorizontal(10).PaddingVertical(2).Text(s).FontSize(8).FontColor("#19355a").Bold();
                                        });
                                    });
                                }
                            });
                        });
                    });
                });

                page.Footer().AlignCenter().PaddingBottom(10).Text(x => {
                    x.Span("صفحة ").FontSize(9).FontFamily("Cairo");
                    x.CurrentPageNumber().FontSize(9);
                });
            });
        }

        // Helpers لتحقيق نفس الـ Design
        private void DrawSectionTitle(ColumnDescriptor column, string title)
        {
            column.Item().PaddingTop(15).PaddingBottom(10).Row(row => {
                row.AutoItem().BorderRight(4).BorderColor("#b38e19").PaddingRight(10).Text(title).FontSize(11).Bold().FontColor("#19355a").FontFamily("Cairo");
            });
        }

        private void DrawEntry(ColumnDescriptor column, string title, string meta)
        {
            column.Item().BorderBottom(1).BorderColor("#e2e8f0").PaddingVertical(8).Column(c => {
                c.Item().Text(title).FontSize(10).Bold().FontColor("#19355a").FontFamily("Cairo");
                c.Item().Text(meta).FontSize(8).FontColor("#64748b").FontFamily("Cairo");
            });
        }

        private void DrawContactRow(ColumnDescriptor column, string label, string value)
        {
            column.Item().PaddingBottom(5).Text(t => {
                t.Span(label).FontSize(8).FontColor("#64748b").Bold();
                t.Span(value).FontSize(8).FontColor("#1e293b");
            });
        }
    }
}