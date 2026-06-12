using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.ReportsAndDashboard.CVModule;

namespace Services.Implementations.ReportsAndDashboards.Documents
{
    public class CVReportPdfDocument : IDocument
    {
        private readonly List<CVReportResponseDTO> _data;
        private readonly string? _notes;

        private static readonly string Navy = "#1B3A6B";
        private static readonly string NavyDk = "#0F2547";
        private static readonly string Gold = "#B8952A";
        private static readonly string GoldLt = "#D4AF50";
        private static readonly string White = "#FFFFFF";
        private static readonly string Off = "#F5F6F9";
        private static readonly string Border = "#D0D8EA";
        private static readonly string Text = "#1A2035";
        private static readonly string Muted = "#5C6B8A";
        private static readonly string RowAlt = "#F9FAFC";

        public CVReportPdfDocument(List<CVReportResponseDTO> data, string? notes)
        {
            _data = data ?? new List<CVReportResponseDTO>();
            _notes = notes;

            FontManager.RegisterFont(File.OpenRead("./fonts/Cairo-Regular.ttf"));
            FontManager.RegisterFont(File.OpenRead("./fonts/Cairo-Bold.ttf"));
            FontManager.RegisterFont(File.OpenRead("./fonts/Amiri-Regular.ttf"));
            FontManager.RegisterFont(File.OpenRead("./fonts/Amiri-Bold.ttf"));
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);
                page.PageColor(Colors.White);
                page.ContentFromRightToLeft();
                page.DefaultTextStyle(x => x.FontFamily("Cairo").FontSize(10).FontColor(Text));

                page.Background().Svg(size => GetBackgroundSvg(size.Width, size.Height));
                page.Header().Height(95).Element(ComposeHeader);

                page.Content().PaddingTop(40).PaddingBottom(50).PaddingHorizontal(40).Column(col =>
                {
                    col.Spacing(0);
                    SectionTitle(col, "أولاً: ملخص إحصائيات السير الذاتية");
                    ComposeStatsRow(col);
                    SectionTitle(col, "ثانياً: تفاصيل السير الذاتية حسب الكليات والأقسام");
                    ComposeCVTable(col);
                    SectionTitle(col, "ثالثاً: التحليل التلقائي والتوصيات");
                    ComposeInsightsBox(col);
                    SectionTitle(col, "رابعاً: ملاحظات إضافية");
                    ComposeUserNotesBox(col);
                    ComposeSignatures(col);
                });

                page.Footer().Height(50).AlignBottom().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer c)
        {
            c.PaddingTop(15).PaddingHorizontal(35).Row(row =>
            {
                row.AutoItem().Row(logoRow => {
                    logoRow.AutoItem().Height(55).Svg(GetLogoSvg());
                    logoRow.AutoItem().PaddingRight(10).AlignMiddle().Column(uni => {
                        uni.Item().Text("جامعة العاصمة").FontSize(16).Bold().FontFamily("Cairo").FontColor(White);
                        uni.Item().PaddingTop(-2).Text("CAPITAL UNIVERSITY").FontSize(8).FontFamily("Cairo").FontColor(GoldLt);
                    });
                });

                row.RelativeItem().AlignCenter().PaddingRight(20).Column(titleCol => {
                    titleCol.Item().Text("احصائيات السيرة الذاتية لأعضاء هيئة التدريس").FontSize(16).Bold().FontFamily("Amiri").FontColor(White);
                    titleCol.Item().PaddingTop(5).Text($"بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontSize(10).FontFamily("Cairo").FontColor(White);
                });

                row.AutoItem().Width(110).AlignLeft().Column(meta => {
                    meta.Item().AlignLeft().Text(t => { t.Span("التاريخ: ").FontColor(White).FontSize(9); t.Span($"{DateTime.UtcNow:dd / MM / yyyy}").FontColor(GoldLt).Bold().FontSize(9); });
                    meta.Item().PaddingTop(4).AlignLeft().Text(t => { t.Span("رقم التقرير: ").FontColor(White).FontSize(9); t.Span($"#CU{DateTime.UtcNow:yyyyMMdd}").FontColor(GoldLt).Bold().FontSize(9); });
                });
            });
        }

        private void ComposeFooter(IContainer c)
        {
            c.PaddingVertical(12).PaddingHorizontal(40).Row(row =>
            {
                row.RelativeItem().AlignRight().Text($"جامعة العاصمة · بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontFamily("Cairo").FontColor(White).FontSize(10);
                row.AutoItem().AlignLeft().Text(t => { t.Span("الصفحة ").FontColor(White); t.CurrentPageNumber().FontColor(White).Bold(); t.Span(" من ").FontColor(White); t.TotalPages().FontColor(White).Bold(); });
            });
        }

        private void ComposeStatsRow(ColumnDescriptor col)
        {
            var totalCVs = _data.Sum(x => x.NoOfCVs);
            col.Item().PaddingTop(15).PaddingBottom(20).Row(row =>
            {
                StatCard(row.RelativeItem(), "إجمالي السير الذاتية", totalCVs.ToString(), Gold);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "عدد الكليات المشمولة", _data.Count.ToString(), Navy);
            });
        }

        private void ComposeCVTable(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(20).Border(1).BorderColor(Border).Table(table =>
            {
                table.ColumnsDefinition(c => { c.RelativeColumn(2); c.RelativeColumn(3); c.RelativeColumn(1); });
                table.Header(h => { HeaderCell(h.Cell(), "الكلية"); HeaderCell(h.Cell(), "القسم العلمي"); HeaderCell(h.Cell(), "عدد السير"); });

                bool even = false;
                foreach (var faculty in _data)
                {
                    foreach (var dept in faculty.DepartmentCVs)
                    {
                        var bg = even ? RowAlt : White;
                        BodyCell(table.Cell(), faculty.FacultyName, bg, false);
                        BodyCell(table.Cell(), dept.DepartmentName, bg, false);
                        BodyCell(table.Cell(), dept.NoOfCVs.ToString(), bg, true);
                        even = !even;
                    }
                }
            });
        }

        private void ComposeInsightsBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(8).PaddingBottom(10).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Gold);
                row.RelativeItem().Background(Off).Padding(15).Column(inner =>
                {
                    inner.Item().Text("تحليل النظام التلقائي:").Bold().FontFamily("Cairo").FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).Text($"يوضح هذا التقرير مدى تفعيل أعضاء هيئة التدريس لسيرهم الذاتية الرقمية، مما يعزز من شفافية البيانات الأكاديمية ضمن رؤية بوابة أعضاء هيئة التدريس لعام {DateTime.UtcNow.Year}.").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private void ComposeUserNotesBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(4).PaddingBottom(20).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(NavyDk);
                row.RelativeItem().Background(White).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ملاحظات المسؤول:").Bold().FontFamily("Cairo").FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).MinHeight(50).Text(_notes ?? "لا توجد ملاحظات إضافية.").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private static void ComposeSignatures(ColumnDescriptor col)
        {
            col.Item().PaddingTop(28).BorderTop(1).BorderColor(Border).PaddingTop(18).Row(row =>
            {
                SignatureBox(row.RelativeItem(), "إعداد: مركز تكنولوجيا المعلومات");
                row.ConstantItem(60);
                SignatureBox(row.RelativeItem(), "اعتماد: عميد الكلية");
            });
        }

        private static void SignatureBox(IContainer c, string label)
        {
            c.Column(col => { col.Item().Height(38).BorderBottom(1).BorderColor("#AAAAAA"); col.Item().PaddingTop(8).AlignCenter().Text(label).FontSize(11).FontFamily("Cairo").FontColor(Navy); });
        }

        private static void StatCard(IContainer c, string label, string value, string accentColor)
        {
            c.Border(2).BorderColor(Border).Column(col => {
                col.Item().Background(Off).Padding(14).AlignCenter().Column(inner => {
                    inner.Item().Text(value).Bold().FontSize(20).FontColor(Navy);
                    inner.Item().PaddingTop(5).Text(label).FontSize(12).FontFamily("Cairo").FontColor(Muted).Bold();
                });
                col.Item().Height(4).Background(accentColor);
            });
        }

        private static void SectionTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(22).PaddingBottom(10).BorderBottom(2).BorderColor(Border).Row(row => {
                row.ConstantItem(6).Height(24).Background(Gold);
                row.RelativeItem().PaddingRight(12).AlignMiddle().AlignRight().Text(title).Bold().FontFamily("Cairo").FontSize(14).FontColor(Navy);
            });
        }

        private static void HeaderCell(IContainer c, string text) => c.Background(Navy).BorderBottom(1).BorderColor(Gold).Padding(10).AlignCenter().AlignMiddle().Text(text).Bold().FontFamily("Cairo").FontSize(11).FontColor(White);

        private static void BodyCell(IContainer c, string text, string bg, bool center)
        {
            var cell = c.Background(bg).Border(0.5f).BorderColor(Border).Padding(8).AlignMiddle();
            if (center) cell.AlignCenter().Text(text).FontSize(10);
            else cell.AlignRight().PaddingRight(5).Text(text).FontSize(10);
        }

        private string GetLogoSvg() => @"<svg viewBox='0 0 90 110' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110' stroke='#B8952A' stroke-width='3.5' fill='none' stroke-linecap='round'/>
            <circle cx='45' cy='45' r='10' fill='#B8952A'/>
            <ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none'/>
            <ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none' transform='rotate(60 45 45)'/>
            <ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none' transform='rotate(120 45 45)'/>
        </svg>";

        private string GetBackgroundSvg(float width, float height) => $@"<svg width='{width}' height='{height}' xmlns='http://www.w3.org/2000/svg'>
            <defs>
                <linearGradient id='headerGradient' x1='0%' y1='0%' x2='100%' y2='0%'><stop offset='0%' stop-color='{NavyDk}' /><stop offset='100%' stop-color='{Navy}' /></linearGradient>
                <linearGradient id='ribbonGradient' x1='0%' y1='0%' x2='100%' y2='0%'><stop offset='0%' stop-color='{NavyDk}' /><stop offset='50%' stop-color='{Gold}' /><stop offset='100%' stop-color='{NavyDk}' /></linearGradient>
            </defs>
            <rect width='{width}' height='{height}' fill='white'/>
            <rect x='0' y='0' width='{width}' height='95' fill='url(#headerGradient)'/>
            <rect x='0' y='95' width='{width}' height='6' fill='url(#ribbonGradient)'/>
            <rect x='0' y='{height - 50}' width='{width}' height='50' fill='{Navy}'/>
            <path d='M{width - 60} 5 L{width - 5} 5 L{width - 5} 60' stroke='{Gold}' stroke-width='5' fill='none' />
            <path d='M60 5 L5 5 L5 60' stroke='{Gold}' stroke-width='5' fill='none' />
            <path d='M{width - 35} {height - 5} L{width - 5} {height - 5} L{width - 5} {height - 35}' stroke='{Gold}' stroke-width='3' fill='none' />
            <path d='M35 {height - 5} L5 {height - 5} L5 {height - 35}' stroke='{Gold}' stroke-width='3' fill='none' />
        </svg>";
    }
}