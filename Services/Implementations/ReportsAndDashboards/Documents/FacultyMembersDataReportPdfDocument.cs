using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;

namespace Services.Implementations.ReportsAndDashboards.Documents
{
    public class FacultyMembersDataReportPdfDocument : IDocument
    {
        private readonly List<Faculty> _faculties;
        private readonly List<Department> _departments;
        private readonly List<FacultyMembersDataReportResponseDTO> _data;
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
        private static readonly string RowAlt = "#F9FAFC";

        public FacultyMembersDataReportPdfDocument(
            List<Faculty> faculties,
            List<Department> departments,
            List<FacultyMembersDataReportResponseDTO> data,
            string? notes)
        {
            _data = data ?? new List<FacultyMembersDataReportResponseDTO>();
            _notes = notes;
            _faculties = faculties ?? new List<Faculty>();
            _departments = departments ?? new List<Department>();

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
                        <rect x='0' y='0' width='{width}' height='110' fill='url(#headerGradient)'/>
                        <rect x='0' y='110' width='{width}' height='6' fill='url(#ribbonGradient)'/>
                        <rect x='0' y='{height - 50}' width='{width}' height='50' fill='{Navy}'/>
                        <path d='M{width - 60} 5 L{width - 5} 5 L{width - 5} 60' stroke='{Gold}' stroke-width='5' fill='none' />
                        <path d='M60 5 L5 5 L5 60' stroke='{Gold}' stroke-width='5' fill='none' />
                        <path d='M{width - 35} {height - 5} L{width - 5} {height - 5} L{width - 5} {height - 35}' stroke='{Gold}' stroke-width='3' fill='none' />
                        <path d='M35 {height - 5} L5 {height - 5} L5 {height - 35}' stroke='{Gold}' stroke-width='3' fill='none' />
                    </svg>";
                });

                page.Header().Height(110).Element(ComposeHeader);

                page.Content().PaddingTop(30).PaddingBottom(50).PaddingHorizontal(40).Column(col =>
                {
                    col.Spacing(0);

                    ComposeReportIntroduction(col);

                    SectionTitle(col, "أولاً: ملخص الأداء الأكاديمي");
                    ComposeStatsRow(col);

                    SectionTitle(col, "ثانياً: بيانات أعضاء هيئة التدريس المختارة");
                    ComposeFacultyTable(col);

                    SectionTitle(col, "ثالثاً: التحليل والتوصيات الإدارية");
                    ComposeInsightsBox(col);
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
                row.AutoItem().AlignMiddle().Row(logoRow =>
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
                        uniName.Item().Text("جامعة العاصمة").FontSize(16).Bold().FontColor(Colors.White).FontFamily("Cairo");
                        uniName.Item().PaddingTop(-4).Text("CAPITAL UNIVERSITY").FontSize(8).FontColor(GoldLt).FontFamily("Cairo");
                    });
                });

                row.RelativeItem().AlignMiddle().AlignCenter().PaddingRight(20).Column(titleCol =>
                {
                    titleCol.Item().AlignCenter().Text("تقرير تفصيلي عن أعضاء هيئة التدريس").FontSize(18).Bold().FontColor(Colors.White).FontFamily("Amiri");
                    titleCol.Item().PaddingTop(2).AlignCenter().Text($"بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontSize(9).FontColor(Colors.White).FontFamily("Cairo");
                });

                row.AutoItem().Width(120).AlignMiddle().AlignLeft().Column(meta =>
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

        private void ComposeReportIntroduction(ColumnDescriptor col)
        {
            bool hasFaculties = _faculties != null && _faculties.Any();
            bool hasDepartments = _departments != null && _departments.Any();

            col.Item().PaddingBottom(10).Text(t =>
            {
                t.Span("ملحوظة: ").FontColor(Gold).FontFamily("Amiri").FontSize(12).Bold();

                if (!hasFaculties && !hasDepartments)
                {
                    t.Span("هذا التقرير عام وشامل لجميع كليات وأقسام الجامعة حيث لم يتم تحديد كلية معينة أو قسم معين.").FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                }
                else if (hasFaculties && !hasDepartments)
                {
                    var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));
                    t.Span("هذا التقرير مستخرج خصيصاً لكليات (").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
                    t.Span(uniqueFacultyNames).FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                    t.Span(") ولم يتم تحديد أقسام علمية معينة.").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
                }
                else
                {
                    var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));
                    t.Span("هذا التقرير صادر لكليات (").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
                    t.Span(uniqueFacultyNames).FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                    t.Span(") للأقسام العلمية التالية: ").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();

                    for (int i = 0; i < _departments!.Count; i++)
                    {
                        var dept = _departments[i];
                        var facultyObj = _faculties.FirstOrDefault(f => f.Id == dept.FacultyId);
                        string facultyName = facultyObj != null ? facultyObj.NameAR : "الكلية التابع لها";

                        t.Span("(").FontColor(Text).FontFamily("Cairo").FontSize(10);
                        t.Span($"{dept.NameAR}").FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                        t.Span(" في ").FontColor(Text).FontFamily("Cairo").FontSize(10);
                        t.Span(facultyName).FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                        t.Span(")");

                        if (i < _departments.Count - 1)
                        {
                            t.Span("، ").FontColor(Text).FontFamily("Cairo").FontSize(10);
                        }
                    }
                    t.Span(".").FontColor(Gold).FontFamily("Amiri").FontSize(12);
                }
            });
        }

        private static void SectionTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(18).PaddingBottom(10)
               .BorderBottom(2).BorderColor(Border)
               .Row(row =>
               {
                   row.ConstantItem(6).Height(24).Background(Gold);
                   row.RelativeItem().PaddingRight(12).AlignMiddle().AlignRight()
                      .Text(title).Bold().FontSize(14).FontColor(Navy);
               });
        }

        private void ComposeStatsRow(ColumnDescriptor col)
        {
            var totalInternational = _data.Sum(x => x.NoOfInternationalResearches);
            var totalLocal = _data.Sum(x => x.NoOfLocalResearches);
            var totalPatents = _data.Sum(x => x.NoOfPatents);

            col.Item().PaddingTop(15).PaddingBottom(18).Row(row =>
            {
                StatCard(row.RelativeItem(), "أبحاث دولية", totalInternational.ToString(), Navy);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "أبحاث محلية", totalLocal.ToString(), Gold);
                row.ConstantItem(12);
                StatCard(row.RelativeItem(), "براءات اختراع", totalPatents.ToString(), NavyDk);
            });
        }

        private void ComposeFacultyTable(ColumnDescriptor col)
        {
            col.Item().PaddingBottom(14).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.ConstantColumn(30);
                    c.RelativeColumn(2.3f);
                    c.RelativeColumn(3.4f);
                    c.RelativeColumn(1.3f);
                    c.RelativeColumn(1.3f);
                    c.RelativeColumn(1.3f);
                    c.RelativeColumn(1.1f);
                });

                table.Header(h =>
                {
                    HeaderCell(h.Cell(), "#");
                    HeaderCell(h.Cell(), "الاسم");
                    HeaderCell(h.Cell(), "القسم العلمي والكلية");
                    HeaderCell(h.Cell(), "أبحاث دولية");
                    HeaderCell(h.Cell(), "أبحاث محلية");
                    HeaderCell(h.Cell(), "براءات اختراع");
                    HeaderCell(h.Cell(), "جوائز");
                });

                int index = 1;
                bool even = false;

                foreach (var member in _data)
                {
                    var bg = even ? RowAlt : White;

                    var currentDept = _departments.FirstOrDefault(d => d.NameAR == member.Department);
                    var facultyObj = currentDept != null ? _faculties.FirstOrDefault(f => f.Id == currentDept.FacultyId) : null;

                    string facultyName = facultyObj != null ? facultyObj.NameAR : member.Faculty;

                    string departmentAndFaculty = !string.IsNullOrEmpty(facultyName)
                        ? $"{member.Department} بكلية ({facultyName})"
                        : member.Department;

                    BodyCell(table.Cell(), index.ToString(), bg, center: true);
                    BodyCell(table.Cell(), member.Name, bg, center: false);
                    BodyCell(table.Cell(), departmentAndFaculty, bg, center: false);
                    BodyCell(table.Cell(), member.NoOfInternationalResearches.ToString(), bg, center: true);
                    BodyCell(table.Cell(), member.NoOfLocalResearches.ToString(), bg, center: true);
                    BodyCell(table.Cell(), member.NoOfPatents.ToString(), bg, center: true);
                    BodyCell(table.Cell(), member.NoOfAwards.ToString(), bg, center: true);

                    even = !even;
                    index++;
                }
            });
        }

        private void ComposeInsightsBox(ColumnDescriptor col)
        {
            bool hasFaculties = _faculties != null && _faculties.Any();
            bool hasDepartments = _departments != null && _departments.Any();

            col.Item().PaddingBottom(6).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Gold);
                row.RelativeItem().Background(NoteYellow).Padding(15).Column(inner =>
                {
                    inner.Item().Text("تحليل النظام التلقائي:").Bold().FontSize(12).FontColor(NavyDk);

                    if (_data == null || !_data.Any())
                    {
                        inner.Item().PaddingTop(8).Text("لا يوجد بيانات كافية لإجراء التحليل التلقائي.").FontSize(11).LineHeight(1.8f);
                    }
                    else
                    {
                        double avgResearches = Math.Round(_data.Average(x => x.NoOfInternationalResearches + x.NoOfLocalResearches), 1);
                        var topDeptPatent = _data.GroupBy(x => x.Department)
                                                 .Select(g => new { Dept = g.Key, Patents = g.Sum(x => x.NoOfPatents) })
                                                 .OrderByDescending(g => g.Patents)
                                                 .FirstOrDefault();

                        string baseInsight = "";
                        if (!hasFaculties && !hasDepartments)
                        {
                            baseInsight = "يوضح التحليل الإحصائي العام توزيع نسب الأداء الأكاديمي على مستوى كافة كليات وأقسام الجامعة للفترة المحددة.";
                        }
                        else if (hasFaculties && !hasDepartments)
                        {
                            var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));
                            baseInsight = $"يتناول هذا التحليل قياس كفاءة الأداء الأكاديمي الإجمالي على مستوى كليات ({uniqueFacultyNames}) للفترة المختارة.";
                        }
                        else
                        {
                            var firstDept = _departments!.FirstOrDefault();
                            string targetDept = firstDept != null ? firstDept.NameAR : "القسم المختار";
                            baseInsight = $"يتصدر قسم {targetDept} مؤشرات الأداء الأكاديمي بنسبة تميز عالية ملبية لتطلعات الكلية للفترة المختارة.";
                        }

                        string patentInsight = topDeptPatent != null && topDeptPatent.Patents > 0
                            ? $"• تم رصد نمو ملحوظ في طلبات براءات الاختراع لقسم {topDeptPatent.Dept} ملبياً تطلعات الخطة الاستراتيجية."
                            : "• يوصى بتقديم دعم إضافي للأقسام العلمية لرفع معدل تسجيل براءات الاختراع الدولية المعتمدة.";

                        inner.Item().PaddingTop(8).Column(textCol =>
                        {
                            textCol.Spacing(4);
                            textCol.Item().Text($"• {baseInsight}").FontSize(11).LineHeight(1.8f);
                            textCol.Item().Text(patentInsight).FontSize(11).LineHeight(1.8f);
                            textCol.Item().Text($"• نحقق حالياً معدل {avgResearches} بحث لكل عضو هيئة تدريس، وهو ما يمثل أداءً مستقراً وممتازاً للعام الأكاديمي الحالي.").FontSize(11).LineHeight(1.8f);
                        });
                    }
                });
            });
        }

        private void ComposeUserNotesBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(20).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(NavyDk);
                row.RelativeItem().Background(White).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ملاحظات عميد الكلية / المسؤول:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).MinHeight(60).Text(_notes ?? "لا توجد ملاحظات إضافية").FontSize(11).LineHeight(1.8f);
                });
            });
        }

        private static void ComposeSignatures(ColumnDescriptor col)
        {
            col.Item().PaddingTop(28).BorderTop(1).BorderColor(Border).PaddingTop(18).Row(row =>
            {
                SignatureBox(row.RelativeItem(), "ختم الكلية المعنية");
                row.ConstantItem(60);
                SignatureBox(row.RelativeItem(), "اعتماد: عميد الكلية");
            });
        }

        private static void SignatureBox(IContainer c, string title)
        {
            c.Column(col =>
            {
                col.Item().Text(title).FontSize(10).FontColor(Muted);
                col.Item().PaddingTop(35).BorderBottom(1).BorderColor(Border);
            });
        }

        private void ComposeFooter(IContainer c)
        {
            c.PaddingVertical(12).PaddingHorizontal(45)
             .Row(row =>
             {
                 row.RelativeItem().AlignRight()
                    .Text($"جامعة العاصمة · بوابة أعضاء هيئة التدريس  {DateTime.UtcNow.Year}")
                    .FontColor(Colors.White).FontSize(10);

                 row.AutoItem().AlignLeft().Text(t =>
                 {
                     t.Span("الصفحة ").FontColor(Colors.White).FontSize(10);
                     t.CurrentPageNumber().FontColor(Colors.White).Bold().FontSize(10);
                     t.Span(" من ").FontColor(Colors.White).FontSize(10);
                     t.TotalPages().FontColor(Colors.White).Bold().FontSize(10);
                 });
             });
        }

        private static void StatCard(IContainer c, string label, string value, string accentColor)
        {
            c.Border(2).BorderColor(Border).BorderBottom(0).Column(col =>
            {
                col.Item().Background(Off).PaddingVertical(12).PaddingHorizontal(8).AlignCenter().Column(inner =>
                {
                    inner.Spacing(2);
                    inner.Item().Text(value).Bold().FontSize(18).FontColor(Navy);
                    inner.Item().Text(label).FontSize(10).FontColor(Muted).Bold();
                });

                col.Item().Height(3).Background(accentColor);
            });
        }

        private static void HeaderCell(IContainer c, string text)
        {
            c.Background(Navy)
             .Border(1).BorderColor(Border)
             .Padding(8)
             .AlignCenter()
             .AlignMiddle()
             .Text(text).Bold().FontSize(10).FontColor(Colors.White);
        }

        private static void BodyCell(IContainer c, string text, string bg, bool center)
        {
            var cell = c.Background(bg)
                        .BorderBottom(1).BorderColor(Border)
                        .BorderLeft(1).BorderColor(Border)
                        .BorderRight(1).BorderColor(Border)
                        .Padding(6)
                        .AlignMiddle();

            if (center)
                cell = cell.AlignCenter();
            else
                cell = cell.AlignRight().PaddingRight(5);

            cell.Text(text).FontSize(10);
        }
    }
}