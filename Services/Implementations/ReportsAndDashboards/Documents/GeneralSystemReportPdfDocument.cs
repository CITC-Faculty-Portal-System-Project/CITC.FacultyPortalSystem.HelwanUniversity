using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.Implementations.ReportsAndDashboards.Documents
{
    public class GeneralSystemReportPdfDocument : IDocument
    {
        private readonly GeneralSystemReportDTO _data;
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
        private static readonly string NoteYellow = "#FFFDF0";
        private static readonly string NoteBlue = "#F4F7FA";
        private static readonly string RowAlt = "#F9FAFC";

        public GeneralSystemReportPdfDocument(GeneralSystemReportDTO data, string? notes)
        {
            _data = data;
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

                page.Background().Svg(size =>
                {
                    var width = size.Width;
                    var height = size.Height;

                    return $@"
                    <svg width='{width}' height='{height}' xmlns='http://www.w3.org/2000/svg'>
                        <defs>
                            <linearGradient id='headerGradient' x1='0%' y1='0%' x2='100%' y2='0%'>
                                <stop offset='0%' stop-color='{NavyDk}' />
                                <stop offset='100%' stop-color='{Navy}' />
                            </linearGradient>
                            <linearGradient id='ribbonGradient' x1='0%' y1='0%' x2='100%' y2='0%'>
                                <stop offset='0%' stop-color='{NavyDk}' />
                                <stop offset='50%' stop-color='{Gold}' />
                                <stop offset='100%' stop-color='{NavyDk}' />
                            </linearGradient>
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
                });

                page.Header().Height(95).Element(ComposeHeader);

                page.Content().PaddingTop(40).PaddingBottom(50).PaddingHorizontal(40).Column(col =>
                {
                    col.Spacing(0);

                    SectionTitle(col, "أولاً: توزيع القوى البشرية والمستخدمين");
                    ComposeUsersStats(col);
                    ComposeUsersTable(col);
                    ComposeOperationalBox(col);

                    SectionTitle(col, "ثانياً: تحليل الإنتاج البحثي ومعدلات النمو");
                    ComposeResearchesTable(col);
                    ComposeScientificBox(col);

                    SectionTitle(col, "ثالثاً: مؤشرات الدعم الفني");
                    ComposeTicketsStats(col);

                    SectionTitle(col, "رابعاً: ملاحظات إضافية واعتماد الإدارة");
                    ComposeNotesBox(col);

                    ComposeSignatures(col);
                });

                page.Footer().Height(50).AlignBottom().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer c)
        {
            c.PaddingTop(15).PaddingHorizontal(35).Row(row =>
            {
                row.AutoItem().Row(logoRow =>
                {
                    logoRow.AutoItem().Width(40).AlignMiddle().Svg($@"
                <svg viewBox='0 0 90 110' fill='none' xmlns='http://www.w3.org/2000/svg'>
                    <path d='M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110' stroke='{Gold}' stroke-width='3.5' fill='none' stroke-linecap='round'/>
                    <circle cx='45' cy='45' r='10' fill='{Gold}'/>
                    <ellipse cx='45' cy='45' rx='27' ry='9' stroke='{GoldLt}' stroke-width='1.8' fill='none'/>
                    <ellipse cx='45' cy='45' rx='27' ry='9' stroke='{GoldLt}' stroke-width='1.8' fill='none' transform='rotate(60 45 45)'/>
                    <ellipse cx='45' cy='45' rx='27' ry='9' stroke='{GoldLt}' stroke-width='1.8' fill='none' transform='rotate(120 45 45)'/>
                </svg>");

                    logoRow.AutoItem().PaddingRight(10).AlignMiddle().Column(uniName =>
                    {
                        uniName.Item().Text("جامعة العاصمة").FontSize(16).Bold().FontColor(Colors.White);
                        uniName.Item().PaddingTop(-2).Text("CAPITAL UNIVERSITY").FontSize(8).FontColor(GoldLt);
                    });
                });

                row.RelativeItem().AlignCenter().PaddingRight(20).Column(title =>
                {
                    title.Item().Text("التقرير الإحصائي الفني الدوري").FontSize(18).Bold().FontColor(Colors.White).FontFamily("Amiri");
                    title.Item().PaddingTop(5).AlignCenter().Text($"بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontSize(10).FontColor(Colors.White);
                });

                row.AutoItem().Width(110).AlignLeft().Column(meta =>
                {
                    meta.Item().AlignLeft().Text(t =>
                    {
                        t.Span("التاريخ: ").FontColor(Colors.White).FontSize(9);
                        t.Span($"{DateTime.UtcNow:dd / MM / yyyy}").FontColor(GoldLt).Bold().FontSize(9);
                    });
                    meta.Item().PaddingTop(4).AlignLeft().Text(t =>
                    {
                        t.Span("رقم التقرير: ").FontColor(Colors.White).FontSize(9);
                        t.Span($"#CU{DateTime.UtcNow:yyyyMMdd}").FontColor(GoldLt).Bold().FontSize(9);
                    });
                });
            });
        }

        private static void SectionTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(22).PaddingBottom(10)
               .BorderBottom(2).BorderColor(Border)
               .Row(row =>
               {
                   row.ConstantItem(6).Height(24).Background(Gold);
                   row.RelativeItem().PaddingRight(12).AlignMiddle().AlignRight()
                      .Text(title).Bold().FontSize(14).FontColor(Navy);
               });
        }

        private void ComposeUsersStats(ColumnDescriptor col)
        {
            col.Item().PaddingTop(15).PaddingBottom(18).Row(row =>
            {
                StatCard(row.RelativeItem(), "إجمالي المستخدمين", _data.Stats.TotalUsersNumber.ToString(), Navy);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "أعضاء هيئة التدريس", _data.Stats.TotalFacultyMembersNumber.ToString(), Gold);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "مديرو النظام", _data.Stats.TotalSystemManagersNumber.ToString(), NavyDk);
            });
        }

        private void ComposeUsersTable(ColumnDescriptor col)
        {
            col.Item().PaddingBottom(14).Border(1).BorderColor(Border).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(4);
                    c.RelativeColumn(2);
                });

                table.Header(h =>
                {
                    HeaderCell(h.Cell(), "الكلية / الإدارة");
                    HeaderCell(h.Cell(), "عدد المستخدمين");
                });

                bool even = false;
                foreach (var item in _data.Stats.UsersPerFaculty)
                {
                    var bg = even ? RowAlt : White;
                    BodyCell(table.Cell(), item.FacultyNameAR, bg);
                    BodyCell(table.Cell(), item.TotalNumberOfUsers.ToString(), bg, center: true);
                    even = !even;
                }
            });
        }

        private void ComposeOperationalBox(ColumnDescriptor col)
        {
            col.Item().PaddingBottom(6).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Gold);
                row.RelativeItem().Background(NoteYellow).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ملاحظات تشغيلية:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).Text(_data.OperationalAnalysis ?? "لا يوجد").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private void ComposeResearchesTable(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(14).Border(1).BorderColor(Border).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(4);
                    c.RelativeColumn(2);
                });

                table.Header(h =>
                {
                    HeaderCell(h.Cell(), "توزيع الأبحاث حسب الكلية");
                    HeaderCell(h.Cell(), "عدد الأبحاث");
                });

                bool even = false;
                foreach (var item in _data.Stats.ResearchesPerFaculty)
                {
                    var bg = even ? RowAlt : White;
                    BodyCell(table.Cell(), item.FacultyNameAR, bg);
                    BodyCell(table.Cell(), item.TotalNumberOfResearches.ToString(), bg, center: true);
                    even = !even;
                }
            });
        }

        private void ComposeScientificBox(ColumnDescriptor col)
        {
            col.Item().PaddingBottom(6).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Navy);
                row.RelativeItem().Background(NoteBlue).Padding(15).Column(inner =>
                {
                    inner.Item().Text("توصيات الإنتاج العلمي:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).Text(_data.ScientificAnalysis ?? "لا يوجد").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private void ComposeTicketsStats(ColumnDescriptor col)
        {
            var closed = _data.Stats.TicketsStats.ClosedTicketsNo;
            var opened = _data.Stats.TicketsStats.OpenedTicketsNo;
            double rate = (closed + opened) > 0 ? closed / (double)(closed + opened) * 100 : 0;

            col.Item().PaddingTop(15).PaddingBottom(6).Row(row =>
            {
                StatCard(row.RelativeItem(), "مشكلات محلولة", closed.ToString(), Navy);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "مشكلات قيد العمل", opened.ToString(), Gold);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "معدل الحل", $"{rate:F2}%", NavyDk);
            });
        }

        private void ComposeNotesBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(20).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(NavyDk);
                row.RelativeItem().Background(White).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ملاحظات المسؤول:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).MinHeight(60).Text(_notes ?? "لا يوجد").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private static void ComposeSignatures(ColumnDescriptor col)
        {
            col.Item().PaddingTop(28).BorderTop(1).BorderColor(Border).PaddingTop(18).Row(row =>
            {
                SignatureBox(row.RelativeItem(), "إعداد: بوابة أعضاء هيئة التدريس");
                row.ConstantItem(60);
                SignatureBox(row.RelativeItem(), "اعتماد: رئيس مركز الاتصالات وتكنولوجيا المعلومات");
            });
        }

        private static void SignatureBox(IContainer c, string label)
        {
            c.Column(col =>
            {
                col.Item().Height(38).BorderBottom(1).BorderColor("#AAAAAA");
                col.Item().PaddingTop(8).AlignCenter().Text(label).FontSize(11).FontColor(Navy);
            });
        }

        private void ComposeFooter(IContainer c)
        {
            c.PaddingVertical(12).PaddingHorizontal(40).Row(row =>
            {
                row.RelativeItem().AlignRight().Text($"جامعة العاصمة · بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontColor(Colors.White).FontSize(10);
                row.AutoItem().AlignLeft().Text(t =>
                {
                    t.Span("الصفحة ").FontColor(Colors.White);
                    t.CurrentPageNumber().FontColor(Colors.White).Bold();
                    t.Span(" من ").FontColor(Colors.White);
                    t.TotalPages().FontColor(Colors.White).Bold();
                });
            });
        }

        private static void StatCard(IContainer c, string label, string value, string accentColor)
        {
            c.Border(2).BorderColor(Border).Column(col =>
            {
                col.Item().Background(Off).Padding(14).AlignCenter().Column(inner =>
                {
                    inner.Item().Text(value).Bold().FontSize(20).FontColor(Navy);
                    inner.Item().PaddingTop(5).Text(label).FontSize(12).FontColor(Muted).Bold();
                });
                col.Item().Height(4).Background(accentColor);
            });
        }

        private static void HeaderCell(IContainer c, string text)
        {
            c.Background(Navy).BorderBottom(1).BorderColor(Gold).Padding(10).AlignCenter().AlignMiddle()
             .Text(text).Bold().FontSize(11).FontColor(Colors.White);
        }

        private static void BodyCell(IContainer c, string text, string bg, bool center = false)
        {
            var cell = c.Background(bg).Border(0.5f).BorderColor(Border).Padding(8);
            if (center) cell.AlignCenter().Text(text).FontSize(11);
            else cell.PaddingRight(5).Text(text).FontSize(11);
        }
    }
}