using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;

namespace Services.Implementations.ReportsAndDashboards.Documents
{
    internal class WritingsReportPdfDocument : IDocument
    {
        private readonly List<Faculty> _faculties;
        private readonly List<Department> _departments;
        private readonly List<WritingsReportResponseDTO> _data;
        private readonly string? _notes;

        private static readonly string Navy = "#1B3A6B";
        private static readonly string NavyDk = "#0F2547";
        private static readonly string Gold = "#B8952A";
        private static readonly string GoldLt = "#D4AF50";
        private static readonly string White = "#FFFFFF";
        private static readonly string Border = "#D0D8EA";
        private static readonly string Text = "#1A2035";
        private static readonly string Muted = "#5C6B8A";
        private static readonly string NoteYellow = "#FFFDF0";
        private static readonly string RowAlt = "#F9FAFC";

        public WritingsReportPdfDocument(
            List<Faculty> faculties,
            List<Department> departments,
            List<WritingsReportResponseDTO> data,
            string? notes)
        {
            _data = data ?? new List<WritingsReportResponseDTO>();
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

                    SectionTitle(col, "أولاً: قائمة مؤلفات أعضاء هيئة التدريس");
                    ComposeDataTable(col);

                    SectionTitle(col, "ثانياً: التحليل والتوصيات الإدارية");
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
                    titleCol.Item().AlignCenter().Text("إحصائية عن المؤلفات العلمية").FontSize(18).Bold().FontColor(Colors.White).FontFamily("Amiri");
                    titleCol.Item().PaddingTop(2).AlignCenter().Text($"لجنة النشر العلمي | قسم شؤون الموظفين - لعام {DateTime.UtcNow.Year}").FontColor(Colors.White).FontSize(10).FontFamily("Cairo");
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
                        t.Span("الرقم: ").FontColor(Colors.White).FontSize(9);
                        t.Span($"#BK-{DateTime.UtcNow:yyyy-MMdd}").FontColor(GoldLt).Bold().FontSize(9);
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
                    t.Span("هذا التقرير عام وشامل لجميع الكتب والمؤلفات العلمية المسجلة بكافة كليات وأقسام الجامعة حيث لم يتم تحديد كلية أو قسم معين.")
                     .FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                }
                else if (hasFaculties && !hasDepartments)
                {
                    var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));

                    t.Span("هذا التقرير مستخرج خصيصاً لرصد الكتب وحصر مؤلفات كليات (").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
                    t.Span(uniqueFacultyNames).FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
                    t.Span(") ولم يتم تحديد أقسام علمية معينة.").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
                }
                else
                {
                    var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));

                    t.Span("هذا التقرير صادر لرصد حصر الإنتاج الفكري والكتب الخاصة بكليات (").FontColor(Gold).FontFamily("Amiri").FontSize(12).Italic();
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
                   row.ConstantItem(5).Height(18).Background(Gold);
                   row.RelativeItem().PaddingRight(10).AlignMiddle().AlignRight()
                      .Text(title).Bold().FontSize(13).FontColor(Navy);
               });
        }

        private void ComposeDataTable(ColumnDescriptor col)
        {
            col.Item().PaddingTop(5).PaddingBottom(14).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.ConstantColumn(35);       
                    c.RelativeColumn(4f);     
                    c.RelativeColumn(2f);      
                    c.RelativeColumn(1.5f);     
                });

                table.Header(h =>
                {
                    HeaderCell(h.Cell(), "#");
                    HeaderCell(h.Cell(), "اسم عضو هيئة التدريس");
                    HeaderCell(h.Cell(), "الدور العلمى");
                    HeaderCell(h.Cell(), "عدد المؤلفات");
                });

                int index = 1;
                bool even = false;
                var rows = _data
                    .SelectMany(x => x.Writings.Select(w => new
                    {
                        x.FacultyMemberName,
                        w.AuthorRole,
                        w.NoOfWritings
                    }))
                    .ToList();

                foreach (var item in rows)
                {
                    var bg = even ? RowAlt : White;

                    BodyCell(table.Cell(), index.ToString(), bg, center: true);
                    BodyCell(table.Cell(), item.FacultyMemberName, bg, center: false);
                    BodyCell(table.Cell(), item.AuthorRole, bg, center: true);
                    BodyCell(table.Cell(), item.NoOfWritings.ToString(), bg, center: true);

                    even = !even;
                    index++;
                }
            });
        }

        private void ComposeInsightsBox(ColumnDescriptor col)
        {
            bool hasFaculties = _faculties != null && _faculties.Any();
            bool hasDepartments = _departments != null && _departments.Any();

            col.Item().PaddingBottom(10).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Gold);
                row.RelativeItem().Background(NoteYellow).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ℹ️ تحليل النظام التلقائي للمؤلفات:").Bold().FontSize(12).FontColor(NavyDk);

                    if (_data == null || !_data.Any())
                    {
                        inner.Item().PaddingTop(8).Text("لا توجد بيانات مؤلفات كافية لإجراء التحليل التلقائي.").FontSize(11).LineHeight(1.8f);
                    }
                    else
                    {
                        string insightsText = "";

                        if (!hasFaculties && !hasDepartments)
                        {
                            insightsText = $"يُظهر التقرير تنوعاً ملحوظاً في الأدوار العلمية المسجلة لعدد ({_data.Count}) من أعضاء هيئة التدريس، مع ارتفاع ملحوظ في حركة التأليف والإنتاج المعرفي الإجمالي على مستوى الكليات والأقسام العلمية للجامعة.";
                        }
                        else if (hasFaculties && !hasDepartments)
                        {
                            var uniqueFacultyNames = string.Join("، ", _faculties!.Select(f => f.NameAR));
                            insightsText = $"تم رصد حركة إنتاج فكري ونشاط تأليف نشط لعدد ({_data.Count}) من أعضاء هيئة التدريس بكليات ({uniqueFacultyNames})، مما يعكس تميز الكلية المستهدفة بمخرجات النشر العلمي الفردي والمشترك.";
                        }
                        else
                        {
                            var firstDept = _departments!.FirstOrDefault();
                            string targetDept = firstDept != null ? firstDept.NameAR : "القسم المختار";
                            insightsText = $"بناءً على الفلترة، أظهرت البيانات كفاءة بحثية وإنتاجية عالية لعدد ({_data.Count}) من السادة الأكاديميين بقسم {targetDept}، متمثلة في صياغة الكتب الأكاديمية والمؤلفات المرجعية المدخلة بالنظام.";
                        }

                        inner.Item().PaddingTop(8).Text(insightsText).FontSize(11).LineHeight(1.5f).Bold();
                    }
                });
            });
        }

        private void ComposeUserNotesBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(20).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Navy);
                row.RelativeItem().Background(White).Padding(15).Column(inner =>
                {
                    inner.Item().Text("📝 ملاحظات المسؤول / لجنة النشر العلمى:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).MinHeight(60).Text(_notes ?? "... يرجى كتابة أي ملاحظات إضافية هنا ...")
                                 .FontSize(11).LineHeight(1.5f).FontColor(Muted);
                });
            });
        }

        private static void ComposeSignatures(ColumnDescriptor col)
        {
            col.Item().PaddingTop(25).Row(row =>
            {
                row.RelativeItem().AlignCenter().Column(c =>
                {
                    c.Item().Width(180).BorderBottom(1).BorderColor(Border).Height(30);
                    c.Item().PaddingTop(5).AlignCenter().Text("الختم الرسمى").FontSize(11).FontColor(Text);
                });

                row.ConstantItem(100);

                row.RelativeItem().AlignCenter().Column(c =>
                {
                    c.Item().Width(180).BorderBottom(1).BorderColor(Border).Height(30);
                    c.Item().PaddingTop(5).AlignCenter().Text("توقيع مدير القسم / اللجنة").FontSize(11).FontColor(Text);
                });
            });
        }

        private void ComposeFooter(IContainer c)
        {
            c.PaddingVertical(10).PaddingHorizontal(65).Row(row =>
            {
                row.RelativeItem().AlignRight().AlignMiddle()
                   .Text($"جامعة العاصمة · وحدة تقنية المعلومات · {DateTime.UtcNow.Year}")
                   .FontColor(Colors.White).FontSize(10);

                row.AutoItem().AlignLeft().AlignMiddle().Text(t =>
                {
                    t.Span("صفحة ").FontColor(Colors.White).FontSize(10);
                    t.CurrentPageNumber().FontColor(Colors.White).Bold().FontSize(10);
                    t.Span(" من ").FontColor(Colors.White).FontSize(10);
                    t.TotalPages().FontColor(Colors.White).Bold().FontSize(10);
                });
            });
        }

        private static void HeaderCell(IContainer c, string text)
        {
            c.Background(NavyDk)
             .Border(1)
             .BorderColor(Border)
             .Padding(10)
             .AlignCenter()
             .AlignMiddle()
             .Text(text).Bold().FontSize(12).FontColor(Colors.White);
        }

        private static void BodyCell(IContainer c, string text, string bg, bool center)
        {
            var cell = c.Background(bg)
                        .BorderBottom(1).BorderColor(Border)
                        .BorderLeft(1).BorderColor(Border)
                        .Padding(9)
                        .AlignMiddle();

            if (center)
                cell = cell.AlignCenter();
            else
                cell = cell.AlignRight().PaddingRight(8);

            cell.Text(text).FontSize(12);
        }
    }
}