using Domain.Entities.UniversityFacultiesAndDepartments;
using Services.Abstraction.Contracts.ReportsAndDashboard;
using Services.ReportsAndDashboard.Helpers;
using System.Text;

namespace Services.Implementations.ReportsAndDashboards
{
    public class ReportsPreviewingService(IDashboardService _dashboardService , IUnitOfWork _unitOfWork) : IReportsPreviewingService
    {
        public async Task<string> PreviewFacultyResearchesAndResearchersReportAsync(int facultyId , string? notes)
        {

            var facultyRepo = _unitOfWork.GetRepository<Faculty, int>();
            var faculty = await facultyRepo.GetByIdAsync(facultyId) ?? throw new NotFoundException("Faculty Not Found");

            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareFacultyResearchReportDataAsync(facultyId, _dashboardService);

            var report = new StringBuilder();
            report.Append($@" <!DOCTYPE html>
                    <html lang=""ar"" dir=""rtl"">
                    <head>
                    <meta charset=""UTF-8""/>
                    <meta name=""viewport"" content=""width=device-width,initial-scale=1""/>
                    <title>تقرير الإنتاج البحثي حسب الكلية – جامعة العاصمة</title>
                    <link href=""https://fonts.googleapis.com/css2?family=Amiri:wght@400;700&family=Cairo:wght@400;600;700;900&display=swap"" rel=""stylesheet""/>
                    <style>
                    /* ─────────── A4 PAGE CORE ─────────── */
                    :root{{
                      --navy:      #1B3A6B;
                      --navy-dk:   #0F2547;
                      --gold:      #B8952A;
                      --gold-lt:   #D4AF50;
                      --white:     #FFFFFF;
                      --off:       #F5F6F9;
                      --border:    #D0D8EA;
                      --text:      #1a2035;
                      --muted:     #5C6B8A;
                    }}

                    *{{box-sizing:border-box; margin:0; padding:0;}}
                    body {{background: #dde2ec; padding: 20px 0; display: flex; justify-content: center; font-family: 'Cairo', sans-serif; color: var(--text); }}

                    .page {{
                      background: var(--white);
                      width: 210mm;
                      min-height: 297mm;
                      position: relative;
                      display: flex;
                      flex-direction: column;
                      box-shadow: 0 0 20px rgba(0,0,0,0.15);
                      margin: 0 auto;
                    }}

                    /* Decorative corner brackets */
                    .page::before,.page::after {{content:''; position:absolute; width:56px; height:56px; z-index:9;}}
                    .page::before {{top:0; right:0; border-top:5px solid var(--gold); border-right:5px solid var(--gold);}}
                    .page::after  {{top:0; left:0;  border-top:5px solid var(--gold); border-left:5px solid var(--gold);}}
                    .bot-bracket-r, .bot-bracket-l {{position:absolute; width:56px; height:56px; bottom:0; z-index:9;}}
                    .bot-bracket-r {{right:0; border-bottom:5px solid var(--gold); border-right:5px solid var(--gold);}}
                    .bot-bracket-l {{left:0;  border-bottom:5px solid var(--gold); border-left:5px solid var(--gold);}}

                    /* ─────────── HEADER ─────────── */
                    .hd {{
                      background: linear-gradient(135deg, var(--navy-dk) 0%, var(--navy) 55%);
                      padding: 25px 45px;
                      display: grid;
                      grid-template-columns: auto 1fr auto;
                      align-items: center;
                      gap: 20px;
                      color: white;
                    }}
                    .logo {{ display: flex; align-items: center; gap: 15px; }}
                    .logo svg {{ width: 55px; height: 65px; }}
                    .logo-txt .ar {{ font-weight: 900; font-size: 20px; line-height: 1.2; }}
                    .logo-txt .en {{ font-size: 11px; color: var(--gold-lt); letter-spacing: 1px; text-transform: uppercase; }}

                    .hd-title {{ text-align: center; }}
                    .hd-title h1 {{ font-family: 'Amiri', serif; font-size: 24px; margin-bottom: 5px; }}
                    .hd-title p {{ font-size: 12px; opacity: 0.9; }}    
                    .hd-meta {{ text-align: left; font-size: 11px; line-height: 1.6; }}
                    .meta-val {{ color: var(--gold-lt); font-weight: 700; margin-right: 5px; }}

                    .ribbon {{ height: 6px; background: linear-gradient(90deg, var(--navy-dk), var(--gold), var(--navy-dk)); }}

                    /* ─────────── BODY ─────────── */
                    .body {{ padding: 30px 45px; flex: 1; }}

                    .sec-head {{     
                      display: flex; 
                      align-items: center; 
                      gap: 12px; 
                      margin: 25px 0 15px; 
                      border-bottom: 2px solid var(--border); 
                      padding-bottom: 10px; 
                    }}
                    .sec-bar {{ width: 6px; background: var(--gold); height: 24px; border-radius: 2px; }}
                    .sec-head h2 {{ font-size: 18px; color: var(--navy); font-weight: 800; }}

                    /* Stats Grid */
                    .stats-grid {{ display: flex; gap: 15px; margin-bottom: 25px; }}
                    .stat-card {{ 
                      flex: 1; border: 2px solid var(--border); padding: 15px 10px; 
                      text-align: center; border-radius: 8px; background: var(--off);
                    }}
                    .stat-card .val {{ font-size: 20px; font-weight: 900; color: var(--navy); display: block; margin-bottom: 5px; }}
                    .stat-card .lab {{ font-size: 13px; color: var(--muted); font-weight: 700; }}

                    /* Table Styling */
                    .table-container {{ margin-bottom: 20px; border: 1px solid var(--border); border-radius: 8px; overflow: hidden; }}
                    .data-table {{ width: 100%; border-collapse: collapse; font-size: 14px; }}
                    .data-table th {{ background: var(--navy); color: white; padding: 14px; text-align: right; }}
                    .data-table td {{ padding: 14px; border-bottom: 1px solid var(--border); border-left: 1px solid var(--border); }}
                    .data-table td:last-child {{ border-left: none; }}
                    .data-table tr:nth-child(even) {{ background: #f9fafc; }}

                    /* Content Boxes */
                    .note-box, .user-notes-box {{ 
                      padding: 18px; 
                      font-size: 13.5px; 
                      margin-bottom: 15px; 
                      line-height: 1.8;
                      border-radius: 0 6px 6px 0;
                    }}
                    .note-box {{ background: #fffdf0; border-right: 6px solid var(--gold); }}
                    .user-notes-box {{ background: #fefefe; border: 1px solid var(--border); border-right: 6px solid var(--navy-dk); }}

                    .box-title {{ font-weight: 900; color: var(--navy-dk); margin-bottom: 8px; display: flex; align-items: center; gap: 8px; }}

                    /* Signatures */
                    .sig-area {{     
                      margin-top: 30px; display: flex; justify-content: space-between; 
                      padding: 20px 0; border-top: 1px solid var(--border);
                    }}
                    .sig-box {{ text-align: center; width: 220px; font-size: 12px; color: var(--navy); }}
                    .sig-line {{ border-bottom: 1.5px solid #aaa; height: 35px; margin-bottom: 10px; }}

                    .ft {{ background: var(--navy); padding: 15px 45px; display: flex; justify-content: space-between; color: white; font-size: 11px; margin-top: auto; }}

                    @media print {{ body {{ background: none; padding: 0; }} .page {{ box-shadow: none; margin: 0; }} }}
                    </style>
                    </head>
                    <script>
                        window.addEventListener('load', function() {{
                          var mmToPx = 96 / 25.4;
                          var A4_H = Math.round(297 * mmToPx);
                          var page = document.querySelector('.page');
                          var hd   = page.querySelector('.hd');
                          var rib  = page.querySelector('.ribbon');
                          var ft   = page.querySelector('.ft');
                          var body = page.querySelector('main.body');

                          var fixedH  = hd.offsetHeight + rib.offsetHeight + ft.offsetHeight;
                          var availH  = A4_H - fixedH - 60; /* 60 = padding-top + padding-bottom للـ .body */

                          var children = Array.from(body.children);
                          var groups = [[]], usedH = 0;

                          children.forEach(function(el) {{
                            var st = getComputedStyle(el);
                            var elH = el.offsetHeight
                                    + parseFloat(st.marginTop)
                                    + parseFloat(st.marginBottom);
                            if (usedH + elH > availH && groups[groups.length - 1].length > 0) {{
                              groups.push([]);
                              usedH = 0;
                            }}
                            groups[groups.length - 1].push(el);
                            usedH += elH;
                          }});

                          if (groups.length <= 1) return; 

                          var total = groups.length;
                          ft.querySelector('div:last-child').textContent = 'الصفحة 1 من ' + total;

                          var last = page;
                          groups.slice(1).forEach(function(group, i) {{
                            var pn = i + 2;
                            var np = document.createElement('div');
                            np.className = 'page';

                            var br = document.createElement('div'); br.className = 'bot-bracket-r'; np.appendChild(br);
                            var bl = document.createElement('div'); bl.className = 'bot-bracket-l'; np.appendChild(bl);

                            np.appendChild(hd.cloneNode(true));
                            np.appendChild(rib.cloneNode(true));

                            var nb = document.createElement('main');
                            nb.className = 'body';
                            group.forEach(function(el) {{ nb.appendChild(el); }});
                            np.appendChild(nb);

                            var nf = ft.cloneNode(true);
                            nf.querySelector('div:last-child').textContent = 'الصفحة ' + pn + ' من ' + total;
                            np.appendChild(nf);

                            last.parentNode.insertBefore(np, last.nextSibling);
                            last = np;
                          }});

                          document.body.style.flexDirection = 'column';
                          document.body.style.alignItems    = 'center';
                          document.body.style.gap           = '20px';
                        }});
                        </script>
                    
                    <body>
                    <div class=""page"">
                      <div class=""bot-bracket-r""></div><div class=""bot-bracket-l""></div>
  
                      <header class=""hd"">
                        <div class=""logo"">
                          <svg viewBox=""0 0 90 110"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110"" stroke=""#B8952A"" stroke-width=""3.5"" fill=""none"" stroke-linecap=""round""/>
                            <circle cx=""45"" cy=""45"" r=""10"" fill=""#B8952A""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(60 45 45)""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(120 45 45)""/>
                          </svg>
                          <div class=""logo-txt"">
                            <div class=""ar"">جامعة العاصمة</div>
                            <div class=""en"">Capital University</div>
                          </div>
                        </div>
                        <div class=""hd-title"">
                          <h1>تقرير توزيع الإنتاج البحثي على الأقسام العلمية</h1>
                          <p>{faculty!.NameAR} | إحصائيات {DateTime.UtcNow.Year}</p>
                        </div>
                        <div class=""hd-meta"">
                          <div>التاريخ:<span class=""meta-val"">{DateTime.UtcNow.Day} / {DateTime.UtcNow.Month} / {DateTime.UtcNow.Year}</span></div>
                          <div>رقم التقرير:<span class=""meta-val"">#CU{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}{DateTime.UtcNow.Hour}{DateTime.UtcNow.Second}</span></div>
                        </div>
                      </header>
                      <div class=""ribbon""></div>

                     <main class=""body"">
                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>أولاً: ملخص الأداء البحثي للكلية</h2></div>
                          <div class=""stats-grid"">
                            <div class=""stat-card"" style=""border-bottom: 4px solid var(--navy);""><span class=""val"">{data.DepartmentResearches.Count}</span><span class=""lab"">عدد الأقسام</span></div>
                            <div class=""stat-card"" style=""border-bottom: 4px solid var(--gold);""><span class=""val"">{data.DepartmentResearches.Sum(d => d.ResearchesNo)}</span><span class=""lab"">إجمالي الأبحاث</span></div>
                            <div class=""stat-card"" style=""border-bottom: 4px solid var(--navy-dk);""><span class=""val"">{data.DepartmentResearchers.Sum(d => d.ResearchesNo)}</span><span class=""lab"">إجمالي الباحثين</span></div>
                          </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>ثانياً: إحصائيات الأقسام التفصيلية</h2></div>
                          <div class=""table-container"">
                            <table class=""data-table"">
                              <thead>
                                <tr>
                                  <th style=""width: 40%;"">القسم العلمي</th>
                                  <th style=""width: 30%;"">عدد الباحثين</th>
                                  <th style=""width: 30%;"">عدد الأبحاث المنشورة</th>
                                </tr>
                              </thead>
                              <tbody>
                                {data.DepartmentsTableRows}
                              </tbody>
                            </table>
                          </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>ثالثاً: قائمة أفضل 5 باحثين بالكلية</h2></div>
                          <div class=""table-container"">
                            <table class=""data-table"">
                              <thead>
                                <tr>
                                  <th style=""width: 10%;"">التصنيف</th>
                                  <th style=""width: 40%;"">اسم الباحث</th>
                                  <th style=""width: 30%;"">القسم العلمي</th>
                                  <th style=""width: 20%;"">عدد الأبحاث</th>
                                </tr>
                              </thead>
                              <tbody>
                                {data.TopResearchersRows}
                              </tbody>
                            </table>
                          </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>رابعاً: التحليل والتوصيات الإدارية</h2></div>
      
                          <div class=""note-box"">
                            <div class=""box-title"">
                              <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""currentColor""><path d=""M13 10h-2V8h2v2zm0 6h-2v-4h2v4zm-1-14C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z""/></svg>
                              تحليل النظام التلقائي:
                            </div>
                         {data.InsightsHtml}
                         </div>

                          <div class=""user-notes-box"">
                            <div class=""box-title"">
                              <svg width=""18"" height=""18"" viewBox=""0 0 24 24"" fill=""currentColor"" style=""opacity: 0.8;""><path d=""M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04c.39-.39.39-1.02 0-1.41l-2.34-2.34c-.39-.39-1.02-.39-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z""/></svg>
                              ملاحظات عميد الكلية / المسؤول:
                            </div>
                            <div style=""white-space: pre-line; color: var(--text); min-height: 80px;"">
                                {notes ?? "لا يوجد"}
                            </div>
                          </div>
                        </section>

                        <div class=""sig-area"">
                          <div class=""sig-box""><div class=""sig-line""></div><p>إعداد: مركز الاتصالات وتكنولوجيا المعلومات</p></div>
                          <div class=""sig-box""><div class=""sig-line""></div><p>اعتماد: عميد الكلية</p></div>
                        </div>
                      </main>
                      <footer class=""ft"">
                        <div>جامعة العاصمة · بوابة اعضاء هيئة التدريس {DateTime.UtcNow.Year}</div>
                        <div style=""font-weight:700;"">الصفحة 1 من 1</div>
                      </footer>
                    </div>

                    </body>
                    </html>");

            return report.ToString();
        }

        public async Task<string> PreviewGeneralSystemInfoReportAsync(string? notes)
        {
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareGeneralSystemReportDataAsync(_dashboardService);

            var report = new StringBuilder();
            report.Append($@"<!DOCTYPE html>
                    <html lang=""ar"" dir=""rtl"">
                    <head>
                    <meta charset=""UTF-8""/>
                    <meta name=""viewport"" content=""width=device-width,initial-scale=1""/>
                    <title>جامعة العاصمة - التقرير التفصيلي 2026</title>
                    <link href=""https://fonts.googleapis.com/css2?family=Amiri:wght@400;700&family=Cairo:wght@400;600;700;900&display=swap"" rel=""stylesheet""/>
                    <style>
                    /* ─────────── A4 PAGE CORE ─────────── */
                    :root{{
                      --navy:      #1B3A6B;
                      --navy-dk:   #0F2547;
                      --gold:      #B8952A;
                      --gold-lt:   #D4AF50;
                      --white:     #FFFFFF;
                      --off:       #F5F6F9;
                      --border:    #D0D8EA;
                      --text:      #1a2035;
                      --muted:     #5C6B8A;
                    }}

                    *{{box-sizing:border-box; margin:0; padding:0;}}
                    body {{ background: #dde2ec; padding: 20px 0; display: flex; justify-content: center; }}

                    .page {{
                      background: var(--white);
                      width: 210mm;
                      min-height: 297mm;
                      position: relative;
                      display: flex;
                      flex-direction: column;
                      box-shadow: 0 0 20px rgba(0,0,0,0.15);
                      margin: 0 auto;
                    }}

                    /* Decorative corner brackets */
                    .page::before,.page::after {{content:''; position:absolute; width:56px; height:56px; z-index:9;}}
                    .page::before {{top:0; right:0; border-top:5px solid var(--gold); border-right:5px solid var(--gold);}}
                    .page::after  {{top:0; left:0;  border-top:5px solid var(--gold); border-left:5px solid var(--gold);}}
                    .bot-bracket-r, .bot-bracket-l {{position:absolute; width:56px; height:56px; bottom:0; z-index:9;}}
                    .bot-bracket-r {{right:0; border-bottom:5px solid var(--gold); border-right:5px solid var(--gold);}}
                    .bot-bracket-l {{left:0;  border-bottom:5px solid var(--gold); border-left:5px solid var(--gold);}}

                    /* ─────────── HEADER ─────────── */
                    .hd {{
                      background: linear-gradient(135deg, var(--navy-dk) 0%, var(--navy) 55%);
                      padding: 25px 45px;
                      display: grid;
                      grid-template-columns: auto 1fr auto;
                      align-items: center;
                      gap: 20px;
                      color: white;
                    }}
                    .logo {{ display: flex; align-items: center; gap: 15px; }}
                    .logo svg {{ width: 55px; height: 65px; }}
                    .logo-txt .ar {{ font-weight: 900; font-size: 20px; line-height: 1.2; }}
                    .logo-txt .en {{ font-size: 11px; color: var(--gold-lt); letter-spacing: 1px; text-transform: uppercase; }}

                    .hd-title {{ text-align: center; }}
                    .hd-title h1 {{ font-family: 'Amiri', serif; font-size: 26px; margin-bottom: 5px; }}
                    .hd-title p {{ font-size: 12px; opacity: 0.9; font-family: 'Cairo'; }}

                    .hd-meta {{ text-align: left; font-size: 11px; line-height: 1.6; }}
                    .meta-val {{ color: var(--gold-lt); font-weight: 700; margin-right: 5px; }}

                    .ribbon {{ height: 6px; background: linear-gradient(90deg, var(--navy-dk), var(--gold), var(--navy-dk)); }}

                    /* ─────────── BODY ─────────── */
                    .body {{ padding: 30px 45px; flex: 1; }}

                    .sec-head {{ 
                      display: flex; 
                      align-items: center; 
                      gap: 12px; 
                      margin: 25px 0 15px; 
                      border-bottom: 2px solid var(--border); 
                      padding-bottom: 10px; 
                    }}
                    .sec-bar {{ width: 6px; background: var(--gold); height: 24px; border-radius: 2px; }}
                    .sec-head h2 {{ font-size: 19px; color: var(--navy); font-weight: 800; }}

                    /* Stats Grid */
                    .stats-grid {{ 
                      display: flex; 
                      gap: 15px; 
                      margin-bottom: 25px; 
                    }}
                    .stat-card {{ 
                      flex: 1;
                      border: 2px solid var(--border); 
                      padding: 15px 10px; 
                      text-align: center; 
                      border-radius: 8px; 
                      background: var(--off);
                      min-width: 0;
                    }}
                    .stat-card .val {{ 
                      font-size: 20px; 
                      font-weight: 900; 
                      color: var(--navy); 
                      display: block;
                      margin-bottom: 5px;
                      word-wrap: break-word;
                    }}
                    .stat-card .lab {{ font-size: 13px; color: var(--muted); font-weight: 700; }}

                    /* Table Styling */
                    .table-container {{ margin-bottom: 20px; border: 1px solid var(--border); border-radius: 8px; overflow: hidden; }}
                    .data-table {{ width: 100%; border-collapse: collapse; font-size: 14px; table-layout: fixed; }}
                    .data-table th {{ background: var(--navy); color: white; padding: 14px; text-align: right; font-weight: 700; }}
                    .data-table td {{ padding: 14px; border-bottom: 1px solid var(--border); border-left: 1px solid var(--border); color: var(--text); }}
                    .data-table td:last-child {{ border-left: none; }}
                    .data-table tr:nth-child(even) {{ background: #f9fafc; }}

                    /* Content Boxes */
                    .note-box, .recom-box, .user-notes-box {{ 
                      padding: 18px; 
                      font-size: 13.5px; 
                      margin-bottom: 15px; 
                      line-height: 1.8;
                      border-radius: 0 6px 6px 0;
                    }}
                    .note-box {{ background: #fffdf0; border-right: 6px solid var(--gold); }}
                    .recom-box {{ background: #f4f7fa; border-right: 6px solid var(--navy); }}
                    .user-notes-box {{ background: #fefefe; border: 1px solid var(--border); border-right: 6px solid var(--navy-dk); }}

                    .box-title {{ 
                      font-weight: 900; 
                      color: var(--navy-dk); 
                      margin-bottom: 8px; 
                      display: flex; 
                      align-items: center; 
                      gap: 8px; 
                      font-size: 14.5px; 
                    }}

                    /* ─────────── SIGNATURES ─────────── */
                    .sig-area {{ 
                      margin-top: 30px; 
                      display: flex; 
                      justify-content: space-between; 
                      padding: 20px 0; 
                      border-top: 1px solid var(--border);
                    }}
                    .sig-box {{ text-align: center; width: 200px; font-size: 13px; color: var(--navy); }}
                    .sig-line {{ border-bottom: 1.5px solid #aaa; height: 35px; margin-bottom: 12px; }}

                    /* ─────────── FOOTER ─────────── */
                    .ft {{ 
                      background: var(--navy); 
                      padding: 15px 45px; 
                      display: flex; 
                      justify-content: space-between; 
                      color: white; 
                      font-size: 12px; 
                      margin-top: auto;
                    }}

                    @media print {{
                      body {{ background: none; padding: 0; }}
                      .page {{ box-shadow: none; margin: 0; }}
                    }}
                    </style>
                    </head>
                    <script>
                    window.addEventListener('load', function() {{
                        var mmToPx = 96 / 25.4;
                        var A4_H = Math.round(297 * mmToPx);
                        var firstPage = document.querySelector('.page');
                        var hd = firstPage.querySelector('.hd');
                        var rib = firstPage.querySelector('.ribbon');
                        var ft = firstPage.querySelector('.ft');
                        var mainBody = firstPage.querySelector('main.body');

                        var fixedH = hd.offsetHeight + rib.offsetHeight + ft.offsetHeight;
                        var paddingH = 60; 
                        var availH = A4_H - fixedH - paddingH;

                        var children = Array.from(mainBody.children);
                        var groups = [[]], usedH = 0;

                        children.forEach(function(el) {{
                            var st = window.getComputedStyle(el);
                            var elH = el.offsetHeight + parseFloat(st.marginTop) + parseFloat(st.marginBottom);

                            if (usedH + elH > availH && groups[groups.length - 1].length > 0) {{
                                groups.push([]);
                                usedH = 0;
                            }}
                            groups[groups.length - 1].push(el);
                            usedH += elH;
                        }});

                        if (groups.length <= 1) return;

                        var total = groups.length;
                        firstPage.querySelector('.ft div:last-child').textContent = 'الصفحة 1 من ' + total;

                        mainBody.innerHTML = '';
                        groups[0].forEach(function(el) {{ mainBody.appendChild(el); }});

                        var lastPage = firstPage;
    
                        groups.slice(1).forEach(function(group, i) {{
                            var pn = i + 2;
                            var np = document.createElement('div');
                            np.className = 'page';
        
                            var htmlContent = '<div class=""bot-bracket-r""></div><div class=""bot-bracket-l""></div>';
                            htmlContent += hd.outerHTML;
                            htmlContent += rib.outerHTML;
                            htmlContent += '<main class=""body""></main>';
                            htmlContent += ft.outerHTML;
        
                            np.innerHTML = htmlContent;
        
                            var nb = np.querySelector('.body');
                            group.forEach(function(el) {{ nb.appendChild(el); }});
        
                            np.querySelector('.ft div:last-child').textContent = 'الصفحة ' + pn + ' من ' + total;
        
                            lastPage.parentNode.insertBefore(np, lastPage.nextSibling);
                            lastPage = np;
                        }});

                        document.body.style.flexDirection = 'column';
                        document.body.style.alignItems = 'center';
                        document.body.style.gap = '20px';
                    }});
                    </script>
                    <body>
                    <div class=""page"">
                      <div class=""bot-bracket-r""></div><div class=""bot-bracket-l""></div>

                      <header class=""hd"">
                        <div class=""logo"">
                          <svg viewBox=""0 0 90 110"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                            <path d=""M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110"" stroke=""#B8952A"" stroke-width=""3.5"" fill=""none"" stroke-linecap=""round""/>
                            <circle cx=""45"" cy=""45"" r=""10"" fill=""#B8952A""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(60 45 45)""/>
                            <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(120 45 45)""/>
                          </svg>
                          <div class=""logo-txt"">
                            <div class=""ar"">جامعة العاصمة</div>
                            <div class=""en"">Capital University</div>
                          </div>
                        </div>
                        <div class=""hd-title"">
                          <h1>التقرير الإحصائي الفني الدوري</h1>
                          <p>بوابة أعضاء هيئة التدريس {DateTime.Now.Year}</p>
                        </div>
                        <div class=""hd-meta"">
                          <div>التاريخ:<span class=""meta-val"">{DateTime.UtcNow.Day} / {DateTime.UtcNow.Month} / {DateTime.UtcNow.Year}</span></div>
                          <div>رقم التقرير:<span class=""meta-val"">#CU{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}{DateTime.UtcNow.Hour}{DateTime.UtcNow.Second}</span></div>
                        </div>
                      </header>
                      <div class=""ribbon""></div>

                      <main class=""body"">
                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>أولاً: توزيع القوى البشرية والمستخدمين</h2></div>
                          <div class=""stats-grid"">
                            <div class=""stat-card""><span class=""val"">{data.Stats.TotalUsersNumber}</span><span class=""lab"">إجمالي المستخدمين</span></div>
                            <div class=""stat-card""><span class=""val"">{data.Stats.TotalFacultyMembersNumber}</span><span class=""lab"">أعضاء هيئة التدريس</span></div>
                            <div class=""stat-card""><span class=""val"">{data.Stats.TotalSystemManagersNumber}</span><span class=""lab"">مديرو النظام</span></div>
                          </div>
      
                          <div class=""table-container"">
                            <table class=""data-table"">
                              <thead>
                                <tr>
                                  <th style=""width: 40%;"">الكلية / الإدارة</th>
                                  <th style=""width: 30%;"">عدد المستخدمين</th>
                                </tr>
                              </thead>
                              <tbody>
                                 {data.UsersPerFacultyRows}
                               </tbody>
                            </table>
                          </div>
                          <div class=""note-box"">
                            <span class=""box-title"">ملاحظات تشغيلية:</span>
                            {data.OperationalAnalysis} 
                            </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>ثانياً: تحليل الإنتاج البحثي ومعدلات النمو</h2></div>
                          <div class=""table-container"">
                            <table class=""data-table"">
                              <thead>
                                <tr>
                                  <th style=""width: 40%;"">توزيع الأبحاث حسب الكلية</th>
                                  <th style=""width: 30%;"">عدد الأبحاث</th>
                                </tr>
                              </thead>
                              <tbody>
                                {data.ResearchesPerFacultyRows}      
                              </tbody>
                            </table>
                          </div>
                          <div class=""recom-box"">
                            <span class=""box-title"">توصيات الإنتاج العلمي:</span>
                            {data.ScientificAnalysis}  
                        </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>ثالثاً: مؤشرات الدعم الفني </h2></div>
                          <div class=""stats-grid"">
                            <div class=""stat-card""><span class=""val"">{data.Stats.TicketsStats.ClosedTicketsNo}</span><span class=""lab"">مشكلات محلولة</span></div>
                            <div class=""stat-card"" style=""border-color: var(--gold);""><span class=""val"">{data.Stats.TicketsStats.OpenedTicketsNo}</span><span class=""lab"">مشكلات قيد العمل</span></div>
                            <div class=""stat-card""><span class=""val"">{(data.Stats.TicketsStats.ClosedTicketsNo / (double)(data.Stats.TicketsStats.ClosedTicketsNo + data.Stats.TicketsStats.OpenedTicketsNo) * 100):F2}%</span><span class=""lab"">معدل الحل</span></div>
                          </div>
                        </section>

                        <section>
                          <div class=""sec-head""><div class=""sec-bar""></div><h2>رابعاً: ملاحظات إضافية واعتماد الإدارة</h2></div>
                          <div class=""user-notes-box"">
                            <span class=""box-title"">
                              <svg width=""18"" height=""18"" viewBox=""0 0 24 24"" fill=""currentColor"" style=""opacity: 0.8;""><path d=""M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04c.39-.39.39-1.02 0-1.41l-2.34-2.34c-.39-.39-1.02-.39-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z""/></svg>
                              ملاحظات المسؤول:
                            </span>
                            <div style=""white-space: pre-line; color: var(--text); min-height: 60px;"">
                                {notes ?? "لا يوجد"}
                            </div>
                          </div>
                        </section>

                        <div class=""sig-area"">
                          <div class=""sig-box""><div class=""sig-line""></div><p>إعداد: بوابة اعضاء هيئة التدريس</p></div>
                          <div class=""sig-box""><div class=""sig-line""></div><p>اعتماد: رئيس مركز الاتصالات وتكنولوجيا المعلومات</p></div>
                        </div>
                      </main>

                      <footer class=""ft"">
                        <div>جامعة العاصمة - بوابة اعضاء هيئة التدريس {DateTime.Now.Year}</div>
                        <div style=""font-weight:700;"">الصفحة 1 من 1</div>
                      </footer>
                    </div>

                    </body>
                    </html>");

            return report.ToString();
        }

        public async Task<string> PreviewResearchesReportAsync(string? notes)
        {
            
            var data = await GetRequiredDataForDashboardReportsHelpers.PrepareResearchDashboardReportDataAsync(_dashboardService);

            var report = new StringBuilder();
            report.Append($@"<!DOCTYPE html>
                <html lang=""ar"" dir=""rtl"">
                <head>
                    <meta charset=""UTF-8""/>
                    <meta name=""viewport"" content=""width=device-width,initial-scale=1""/>
                    <title>تقرير أداء الباحثين – جامعة العاصمة</title>
                    <link href=""https://fonts.googleapis.com/css2?family=Amiri:wght@400;700&family=Cairo:wght@400;600;700;900&display=swap"" rel=""stylesheet""/>
                    <style>
                        /* ─────────── A4 PAGE CORE ─────────── */
                        :root {{
                          --navy:      #1B3A6B;
                          --navy-dk:   #0F2547;
                          --gold:      #B8952A;
                          --gold-lt:   #D4AF50;
                          --white:     #FFFFFF;
                          --off:       #F5F6F9;
                          --border:    #D0D8EA;
                          --text:      #1a2035;
                          --muted:     #5C6B8A;
                        }}

                        * {{ box-sizing: border-box; margin: 0; padding: 0; }}
                        body {{ background: #dde2ec; padding: 20px 0; display: flex; flex-direction: column; align-items: center; gap: 20px; font-family: 'Cairo', sans-serif; color: var(--text); }}

                        .page {{
                          background: var(--white);
                          width: 210mm;
                          min-height: 297mm;
                          position: relative;
                          display: flex;
                          flex-direction: column;
                          box-shadow: 0 0 20px rgba(0,0,0,0.15);
                        }}

                        .page::before,.page::after {{ content:''; position:absolute; width:56px; height:56px; z-index:9; }}
                        .page::before {{ top:0; right:0; border-top:5px solid var(--gold); border-right:5px solid var(--gold); }}
                        .page::after  {{ top:0; left:0;  border-top:5px solid var(--gold); border-left:5px solid var(--gold); }}
                        .bot-bracket-r, .bot-bracket-l {{ position:absolute; width:56px; height:56px; bottom:0; z-index:9; }}
                        .bot-bracket-r {{ right:0; border-bottom:5px solid var(--gold); border-right:5px solid var(--gold); }}
                        .bot-bracket-l {{ left:0;  border-bottom:5px solid var(--gold); border-left:5px solid var(--gold); }}

                        .hd {{
                          background: linear-gradient(135deg, var(--navy-dk) 0%, var(--navy) 55%);
                          padding: 25px 45px;
                          display: grid;
                          grid-template-columns: auto 1fr auto;
                          align-items: center;
                          gap: 20px;
                          color: white;
                        }}
                        .logo {{ display: flex; align-items: center; gap: 15px; }}
                        .logo svg {{ width: 55px; height: 65px; }}
                        .logo-txt .ar {{ font-weight: 900; font-size: 20px; line-height: 1.2; }}
                        .logo-txt .en {{ font-size: 11px; color: var(--gold-lt); letter-spacing: 1px; text-transform: uppercase; }}

                        .hd-title {{ text-align: center; }}
                        .hd-title h1 {{ font-family: 'Amiri', serif; font-size: 26px; margin-bottom: 5px; }}
                        .hd-title p {{ font-size: 12px; opacity: 0.9; }}

                        .hd-meta {{ text-align: left; font-size: 11px; line-height: 1.6; }}
                        .meta-val {{ color: var(--gold-lt); font-weight: 700; margin-right: 5px; }}

                        .ribbon {{ height: 6px; background: linear-gradient(90deg, var(--navy-dk), var(--gold), var(--navy-dk)); }}
                        .body {{ padding: 30px 45px; flex: 1; }}

                        .sec-head {{ display: flex; align-items: center; gap: 12px; margin: 25px 0 15px; border-bottom: 2px solid var(--border); padding-bottom: 10px; }}
                        .sec-bar {{ width: 6px; background: var(--gold); height: 24px; border-radius: 2px; }}
                        .sec-head h2 {{ font-size: 19px; color: var(--navy); font-weight: 800; }}

                        .stats-grid {{ display: flex; gap: 15px; margin-bottom: 25px; }}
                        .stat-card {{ flex: 1; border: 2px solid var(--border); padding: 15px 10px; text-align: center; border-radius: 8px; background: var(--off); }}
                        .stat-card .val {{ font-size: 20px; font-weight: 900; color: var(--navy); display: block; margin-bottom: 5px; }}
                        .stat-card .lab {{ font-size: 13px; color: var(--muted); font-weight: 700; }}

                        .table-container {{ margin-bottom: 20px; border: 1px solid var(--border); border-radius: 8px; overflow: hidden; }}
                        .data-table {{ width: 100%; border-collapse: collapse; font-size: 14px; }}
                        .data-table th {{ background: var(--navy); color: white; padding: 14px; text-align: right; }}
                        .data-table td {{ padding: 14px; border-bottom: 1px solid var(--border); border-left: 1px solid var(--border); }}
                        .data-table td:last-child {{ border-left: none; }}
                        .data-table tr:nth-child(even) {{ background: #f9fafc; }}

                        .note-box, .user-notes-box {{ padding: 18px; font-size: 13.5px; margin-bottom: 15px; line-height: 1.8; border-radius: 0 6px 6px 0; }}
                        .note-box {{ background: #fffdf0; border-right: 6px solid var(--gold); }}
                        .user-notes-box {{ background: #fefefe; border: 1px solid var(--border); border-right: 6px solid var(--navy-dk); }}
                        .box-title {{ font-weight: 900; color: var(--navy-dk); margin-bottom: 8px; display: flex; align-items: center; gap: 8px; }}

                        .sig-area {{ margin-top: 30px; display: flex; justify-content: space-between; padding: 20px 0; border-top: 1px solid var(--border); }}
                        .sig-box {{ text-align: center; width: 220px; font-size: 12px; color: var(--navy); }}
                        .sig-line {{ border-bottom: 1.5px solid #aaa; height: 35px; margin-bottom: 10px; }}

                        .ft {{ background: var(--navy); padding: 15px 45px; display: flex; justify-content: space-between; color: white; font-size: 11px; margin-top: auto; }}

                        @media print {{ 
                            body {{ background: none; padding: 0; gap: 0; }} 
                            .page {{ box-shadow: none; margin: 0; page-break-after: always; }} 
                        }}
                    </style>
                </head>
                <body>

                <div class=""page"">
                  <div class=""bot-bracket-r""></div><div class=""bot-bracket-l""></div>
  
                  <header class=""hd"">
                    <div class=""logo"">
                      <svg viewBox=""0 0 90 110"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
                        <path d=""M10 110 L10 42 Q10 10 45 10 Q80 10 80 42 L80 110"" stroke=""#B8952A"" stroke-width=""3.5"" fill=""none"" stroke-linecap=""round""/>
                        <circle cx=""45"" cy=""45"" r=""10"" fill=""#B8952A""/>
                        <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none""/>
                        <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(60 45 45)""/>
                        <ellipse cx=""45"" cy=""45"" rx=""27"" ry=""9"" stroke=""#D4AF50"" stroke-width=""1.8"" fill=""none"" transform=""rotate(120 45 45)""/>
                      </svg>
                      <div class=""logo-txt"">
                        <div class=""ar"">جامعة العاصمة</div>
                        <div class=""en"">Capital University</div>
                      </div>
                    </div>
                    <div class=""hd-title"">
                      <h1>تقرير مفصل عن إحصائيات الأبحاث العلمية</h1>
                      <p>بوابة أعضاء هيئة التدريس {DateTime.Now.Year}</p>
                    </div>
                    <div class=""hd-meta"">
                      <div>التاريخ:<span class=""meta-val"">{DateTime.Now.Day} / {DateTime.Now.Month} / {DateTime.Now.Year}</span></div>
                      <div>رقم التقرير:<span class=""meta-val"">#CU{DateTime.UtcNow.Year}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}{DateTime.UtcNow.Hour}{DateTime.UtcNow.Second}</span></div>
                    </div>
                  </header>
                  <div class=""ribbon""></div>

                  <main class=""body"">
                    <section>
                      <div class=""sec-head""><div class=""sec-bar""></div><h2>أولاً: مؤشرات الإنتاج العلمي العام</h2></div>
                      <div class=""stats-grid"">
                        <div class=""stat-card"" style=""border-bottom: 4px solid var(--navy);""><span class=""val"">{data.Stats.InternationalResearchesNo}</span><span class=""lab"">أبحاث دولية</span></div>
                        <div class=""stat-card"" style=""border-bottom: 4px solid var(--gold);""><span class=""val"">{data.Stats.LocalResearchesNo}</span><span class=""lab"">أبحاث محلية</span></div>
                        <div class=""stat-card"" style=""border-bottom: 4px solid var(--navy-dk);""><span class=""val"">{data.Stats.CitationsStats.FirstOrDefault()?.TotalCitationsNo?? 0}</span><span class=""lab"">إجمالي الاقتباسات</span></div>
                      </div>

                      <div class=""table-container"">
                        <table class=""data-table"">
                          <thead>
                            <tr>
                              <th style=""width: 40%;"">أفضل 5 باحثين على مستوى الجامعة</th>
                              <th style=""width: 30%;"">الكلية</th>
                              <th style=""width: 15%;"">عدد الابحاث</th>
                              <th style=""width: 15%;"">النقاط</th>
                            </tr>
                          </thead>
                          <tbody>
                            {data.BestResearchersRows}   
                          </tbody>
                        </table>
                      </div>
                    </section>

                    <section>
                      <div class=""sec-head""><div class=""sec-bar""></div><h2>ثانياً: تحليل المواضيع البحثية والنمو</h2></div>
                      <div class=""table-container"">
                        <table class=""data-table"">
                          <thead>
                            <tr>
                              <th style=""width: 60%;"">أكثر 5 مواضيع بحثية</th>
                              <th style=""width: 40%;"">عدد الباحثين</th>
                            </tr>
                          </thead>
                          <tbody>
                             {data.InterestsRows}  
                          </tbody>
                        </table>
                      </div>

                      <div class=""table-container"">
                        <table class=""data-table"">
                          <thead>
                            <tr>
                              <th style=""width: 50%;"">السنة الإحصائية</th>
                              <th style=""width: 50%;"">إجمالي الاقتباسات السنوية</th>
                            </tr>
                          </thead>
                          <tbody>
                             {data.CitationsRows}
                          </tbody>
                        </table>
                      </div>
                    </section>

                    <section>
                      <div class=""sec-head""><div class=""sec-bar""></div><h2>ثالثاً: التوصيات والملاحظات الإدارية</h2></div>
      
                      <div class=""note-box"">
                        <div class=""box-title"">
                          <svg width=""16"" height=""16"" viewBox=""0 0 24 24"" fill=""currentColor""><path d=""M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z""/></svg>
                          تحليل النظام:
                        </div>
                     {data.SystemAnalysis}
                </div>
                      <div class=""user-notes-box"">
                        <div class=""box-title"">
                          <svg width=""18"" height=""18"" viewBox=""0 0 24 24"" fill=""currentColor"" style=""opacity: 0.8;""><path d=""M3 17.25V21h3.75L17.81 9.94l-3.75-3.75L3 17.25zM20.71 7.04c.39-.39.39-1.02 0-1.41l-2.34-2.34c-.39-.39-1.02-.39-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z""/></svg>
                          ملاحظات المسؤول والاعتماد:
                        </div>
                        <div style=""white-space: pre-line; color: var(--text); min-height: 60px;"">
                          {notes}
                        </div>
                      </div>
                    </section>

                    <div class=""sig-area"">
                      <div class=""sig-box""><div class=""sig-line""></div><p>إعداد: بوابة اعضاء هيئة التدريس</p></div>
                      <div class=""sig-box""><div class=""sig-line""></div><p>اعتماد: رئيس مركز الاتصالات وتكنولوجيا المعلومات</p></div>
                    </div>
                  </main>

                  <footer class=""ft"">
                    <div>جامعة العاصمة · بوابة اعضاء هيئة التدريس {DateTime.Now.Year}</div>
                    <div style=""font-weight:700;"">الصفحة 1 من 1</div>
                  </footer>
                </div>

                <script>
                window.addEventListener('load', function() {{
                  var mmToPx = 96 / 25.4;
                  var A4_H = Math.round(297 * mmToPx);
                  var page = document.querySelector('.page');
                  var hd   = page.querySelector('.hd');
                  var rib  = page.querySelector('.ribbon');
                  var ft   = page.querySelector('.ft');
                  var bodyContent = page.querySelector('main.body');
  
                  var fixedH  = hd.offsetHeight + rib.offsetHeight + ft.offsetHeight;
                  var availH  = A4_H - fixedH - 60; 
  
                  var children = Array.from(bodyContent.children);
                  var groups = [[]], usedH = 0;

                  children.forEach(function(el) {{
                    var st = window.getComputedStyle(el);
                    var elH = el.offsetHeight + parseFloat(st.marginTop) + parseFloat(st.marginBottom);
    
                    if (usedH + elH > availH && groups[groups.length - 1].length > 0) {{
                      groups.push([]);
                      usedH = 0;
                    }}
                    groups[groups.length - 1].push(el);
                    usedH += elH;
                  }});

                  if (groups.length <= 1) return;

                  var total = groups.length;
                  ft.querySelector('div:last-child').textContent = 'الصفحة 1 من ' + total;

                  var lastPage = page;
                  groups.slice(1).forEach(function(group, i) {{
                    var pn = i + 2;
                    var np = document.createElement('div');
                    np.className = 'page';
    
                    np.innerHTML = `
                      <div class=""bot-bracket-r""></div><div class=""bot-bracket-l""></div>
                      ${{hd.outerHTML}}
                      ${{rib.outerHTML}}
                      <main class=""body""></main>
                      ${{ft.outerHTML}}
                    `;
    
                    var nb = np.querySelector('.body');
                    group.forEach(function(el) {{ nb.appendChild(el); }});
    
                    np.querySelector('.ft div:last-child').textContent = 'الصفحة ' + pn + ' من ' + total;
    
                    lastPage.parentNode.insertBefore(np, lastPage.nextSibling);
                    lastPage = np;
                  }});
                }});
                </script>
                </body>
                </html>");

            return report.ToString();
        }
    }
}
