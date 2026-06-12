using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Entities.UniversityFacultiesAndDepartments;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;

namespace Services.Implementations.ReportsAndDashboards.Documents
{
    internal class ProjectsReportPdfDocument : IDocument
    {
        private readonly List<Faculty> _faculties;
        private readonly List<Department> _departments;
        private readonly List<ProjectsReportResponseDTO> _data;
        private readonly string? _notes;

        private static readonly string Navy = "#1B3A6B";
        private static readonly string NavyDk = "#0F2547";
        private static readonly string Gold = "#B8952A";
        private static readonly string GoldLt = "#D4AF50";
        private static readonly string Text = "#1A2035";
        private static readonly string Muted = "#5C6B8A";
        private static readonly string NoteYellow = "#FFFDF0";
        private static readonly string RowAlt = "#F9FAFC";
        private static readonly string Border = "#D0D8EA";

        public ProjectsReportPdfDocument(
            List<Faculty> faculties,
            List<Department> departments,
            List<ProjectsReportResponseDTO> data,
            string? notes)
        {
            _data = data ?? new List<ProjectsReportResponseDTO>();
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
                    SectionTitle(col, "أولاً: قائمة المشاريع البحثية");
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
                    logoRow.AutoItem().Width(40).AlignMiddle().Svg(@"<svg viewBox='0 0 90 110' fill='none' xmlns='http://www.w3.org/2000/svg'><path d='M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110' stroke='#B8952A' stroke-width='3.5' fill='none' stroke-linecap='round'/><circle cx='45' cy='45' r='10' fill='#B8952A'/><ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none'/><ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none' transform='rotate(60 45 45)'/><ellipse cx='45' cy='45' rx='27' ry='9' stroke='#D4AF50' stroke-width='1.8' fill='none' transform='rotate(120 45 45)'/></svg>");
                    logoRow.AutoItem().PaddingRight(10).AlignMiddle().Column(uniName =>
                    {
                        uniName.Item().Text("جامعة العاصمة").FontSize(16).Bold().FontColor(Colors.White).FontFamily("Cairo");
                        uniName.Item().PaddingTop(-4).Text("CAPITAL UNIVERSITY").FontSize(8).FontColor(GoldLt).FontFamily("Cairo");
                    });
                });

                row.RelativeItem().AlignMiddle().AlignCenter().PaddingRight(20).Column(titleCol =>
                {
                    titleCol.Item().AlignCenter().Text("تقرير عن المشاريع البحثية").FontSize(18).Bold().FontColor(Colors.White).FontFamily("Amiri");
                    titleCol.Item().PaddingTop(2).AlignCenter().Text($"بوابة أعضاء هيئة التدريس {DateTime.UtcNow.Year}").FontColor(Colors.White).FontSize(10).FontFamily("Cairo");
                });

                row.AutoItem().Width(120).AlignMiddle().AlignLeft().Column(meta =>
                {
                    meta.Item().Text(t => { t.Span("تاريخ التقرير: ").FontColor(Colors.White).FontSize(9); t.Span($"{DateTime.UtcNow:dd/MM/yyyy}").FontColor(GoldLt).Bold().FontSize(9); });
                    meta.Item().PaddingTop(4).Text(t => { t.Span("رقم التقرير: ").FontColor(Colors.White).FontSize(9); t.Span($"#CU{DateTime.UtcNow:yyyyMMdd}").FontColor(GoldLt).Bold().FontSize(9); });
                });
            });
        }

        private void ComposeReportIntroduction(ColumnDescriptor col)
        {
            bool hasFaculties = _faculties?.Any() ?? false;
            col.Item().PaddingBottom(10).Text(t =>
            {
                t.Span("ملحوظة: ").FontColor(Gold).FontFamily("Amiri").FontSize(12).Bold();
                t.Span(hasFaculties ? "هذا التقرير مخصص لرصد المشاريع البحثية المسجلة للكليات والأقسام المحددة." : "هذا التقرير عام وشامل لجميع المشاريع البحثية بكافة كليات وأقسام الجامعة.")
                 .FontColor(Navy).FontFamily("Cairo").FontSize(10).Bold();
            });
        }

        private static void SectionTitle(ColumnDescriptor col, string title)
        {
            col.Item().PaddingTop(18).PaddingBottom(10).BorderBottom(2).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(5).Height(18).Background(Gold);
                row.RelativeItem().PaddingRight(10).AlignMiddle().Text(title).Bold().FontSize(13).FontColor(Navy);
            });
        }

        private void ComposeDataTable(ColumnDescriptor col)
        {
            col.Item().PaddingTop(5).PaddingBottom(14).Table(table =>
            {
                table.ColumnsDefinition(c => { c.ConstantColumn(40); c.RelativeColumn(2); c.RelativeColumn(2); c.RelativeColumn(1); });
                table.Header(h =>
                {
                    HeaderCell(h.Cell(), "#");
                    HeaderCell(h.Cell(), "اسم عضو هيئة التدريس");
                    HeaderCell(h.Cell(), "نوع المشروع");
                    HeaderCell(h.Cell(), "العدد");
                });

                int index = 1;
                foreach (var item in _data)
                {
                    foreach (var proj in item.Projects)
                    {
                        string bg = (index % 2 == 0) ? RowAlt : Colors.White;
                        BodyCell(table.Cell(), index.ToString(), bg, true);
                        BodyCell(table.Cell(), item.FacultyMemberName, bg, false);
                        BodyCell(table.Cell(), proj.ProjectType, bg, true);
                        BodyCell(table.Cell(), proj.NoOfProjects.ToString(), bg, true);
                        index++;
                    }
                }
            });
        }

        private void ComposeInsightsBox(ColumnDescriptor col)
        {
            col.Item().PaddingBottom(10).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Gold);
                row.RelativeItem().Background(NoteYellow).Padding(15).Column(inner =>
                {
                    inner.Item().Text("تحليل النظام التلقائي:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).Text(_data.Any() ? $"تم رصد ({_data.Count}) من أعضاء هيئة التدريس المشاركين في مشاريع بحثية متنوعة، مما يساهم في تعزيز الإنتاج العلمي للجامعة." : "لا توجد بيانات مشاريع متاحة للتحليل.").FontSize(11).Bold();
                });
            });
        }

        private void ComposeUserNotesBox(ColumnDescriptor col)
        {
            col.Item().PaddingTop(12).PaddingBottom(20).Border(1).BorderColor(Border).Row(row =>
            {
                row.ConstantItem(6).Background(Navy);
                row.RelativeItem().Background(Colors.White).Padding(15).Column(inner =>
                {
                    inner.Item().Text("ملاحظات المسؤول:").Bold().FontSize(12).FontColor(NavyDk);
                    inner.Item().PaddingTop(8).MinHeight(60).Text(_notes ?? "... لا توجد ملاحظات إضافية ...").FontSize(11).FontColor(Muted);
                });
            });
        }

        private static void ComposeSignatures(ColumnDescriptor col)
        {
            col.Item().PaddingTop(25).Row(row =>
            {
                row.RelativeItem().AlignCenter().Column(c => { c.Item().Width(180).BorderBottom(1).BorderColor(Border).Height(30); c.Item().PaddingTop(5).Text("معد التقرير").FontSize(11); });
                row.ConstantItem(100);
                row.RelativeItem().AlignCenter().Column(c => { c.Item().Width(180).BorderBottom(1).BorderColor(Border).Height(30); c.Item().PaddingTop(5).Text("اعتماد عميد الكلية").FontSize(11); });
            });
        }

        private void ComposeFooter(IContainer c)
        {
            c.PaddingVertical(12).PaddingHorizontal(40).Row(row =>
            {
                row.RelativeItem().AlignRight().Text($"جامعة العاصمة · بوابة أعضاء هيئة التدريس · {DateTime.UtcNow.Year}").FontColor(Colors.White).FontSize(10);
                row.AutoItem().Text(t => { t.Span("الصفحة ").FontColor(Colors.White); t.CurrentPageNumber().FontColor(Colors.White); t.Span(" من ").FontColor(Colors.White); t.TotalPages().FontColor(Colors.White); });
            });
        }

        private static void HeaderCell(IContainer c, string text) => c.Background(NavyDk).Border(1).BorderColor(Border).Padding(10).AlignCenter().Text(text).Bold().FontSize(12).FontColor(Colors.White);

        private static void BodyCell(IContainer c, string text, string bg, bool center)
        {
            var cell = c.Background(bg).BorderBottom(1).BorderLeft(1).BorderColor(Border).Padding(9).AlignMiddle();
            if (center) cell = cell.AlignCenter(); else cell = cell.AlignRight().PaddingRight(8);
            cell.Text(text).FontSize(12);
        }
    }
}